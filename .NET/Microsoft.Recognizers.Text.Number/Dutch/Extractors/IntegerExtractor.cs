﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Dutch;

namespace Microsoft.Recognizers.Text.Number.Dutch
{
    public class IntegerExtractor : CachedNumberExtractor
    {
        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly ConcurrentDictionary<string, IntegerExtractor> Instances = new ConcurrentDictionary<string, IntegerExtractor>();

        private readonly string keyPrefix;

        private IntegerExtractor(BaseNumberOptionsConfiguration config)
            : base(config.Options)
        {

            keyPrefix = string.Intern(ExtractType + "_" + config.Options + "_" + config.Placeholder + "_" + config.Culture);

            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    RegexCache.Get(NumbersDefinitions.NumbersWithPlaceHolder(config.Placeholder), RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.NumbersWithSuffix, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.RoundNumberIntegerRegexWithLocks, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.NumbersWithDozenSuffix, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.AllIntRegexWithLocks, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.DUTCH)
                },
                {
                    RegexCache.Get(NumbersDefinitions.AllIntRegexWithDozenSuffixLocks, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.DUTCH)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.IntegerNumComma, config.Placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.IntegerNumBlank, config.Placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.IntegerNumNoBreakSpace, config.Placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.IntegerNumDot, config.Placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_INTEGER; // "Integer";

        public static IntegerExtractor GetInstance(BaseNumberOptionsConfiguration config)
        {

            var extractorKey = config.Placeholder;

            if (!Instances.ContainsKey(extractorKey))
            {
                var instance = new IntegerExtractor(config);
                Instances.TryAdd(extractorKey, instance);
            }

            return Instances[extractorKey];
        }

        protected override object GenKey(string input)
        {
            return (keyPrefix, input);
        }

    }
}