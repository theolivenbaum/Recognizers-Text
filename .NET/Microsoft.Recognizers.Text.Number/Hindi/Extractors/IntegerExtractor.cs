﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Hindi;

namespace Microsoft.Recognizers.Text.Number.Hindi
{
    public class IntegerExtractor : BaseNumberExtractor
    {
        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly ConcurrentDictionary<string, IntegerExtractor> Instances =
            new ConcurrentDictionary<string, IntegerExtractor>();

        private IntegerExtractor(string placeholder = NumbersDefinitions.PlaceHolderDefault)
        {
            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    RegexCache.Get(NumbersDefinitions.NumbersWithPlaceHolder(placeholder), RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.IndianNumberingSystemRegex, RegexFlags),
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
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.HINDI)
                },
                {
                    RegexCache.Get(NumbersDefinitions.AllIntRegexWithDozenSuffixLocks, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.HINDI)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.IntegerNumComma, placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.IntegerNumBlank, placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    GenerateLongFormatNumberRegexes(LongFormatType.IntegerNumNoBreakSpace, placeholder, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.NegativeHinglishRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.HINDI)
                },
                {
                    RegexCache.Get(NumbersDefinitions.CompoundEnglishNumberRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.HINDI)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DecimalUnitsWithRoundNumberRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.HINDI)
                },
            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_INTEGER; // "Integer";

        public static IntegerExtractor GetInstance(string placeholder = NumbersDefinitions.PlaceHolderDefault)
        {
            if (!Instances.ContainsKey(placeholder))
            {
                var instance = new IntegerExtractor(placeholder);
                Instances.TryAdd(placeholder, instance);
            }

            return Instances[placeholder];
        }
    }
}