﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Swedish;

namespace Microsoft.Recognizers.Text.Number.Swedish
{
    public class OrdinalExtractor : CachedNumberExtractor
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly ConcurrentDictionary<string, OrdinalExtractor> Instances =
            new ConcurrentDictionary<string, OrdinalExtractor>();

        private readonly string keyPrefix;

        private OrdinalExtractor(BaseNumberOptionsConfiguration config)
            : base(config.Options)
        {

            keyPrefix = string.Intern(ExtractType + "_" + config.Options.ToString() + "_" + config.Culture);

            AmbiguousFractionConnectorsRegex = RegexCache.Get(NumbersDefinitions.AmbiguousFractionConnectorsRegex, RegexFlags);

            RelativeReferenceRegex = RegexCache.Get(NumbersDefinitions.RelativeOrdinalRegex, RegexFlags);

            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    RegexCache.Get(NumbersDefinitions.OrdinalSuffixRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.ORDINAL_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.OrdinalNumericRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.ORDINAL_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.OrdinalSwedishRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.ORDINAL_PREFIX, Constants.SWEDISH)
                },
                {
                    RegexCache.Get(NumbersDefinitions.OrdinalRoundNumberRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.ORDINAL_PREFIX, Constants.SWEDISH)
                },
            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_ORDINAL; // "Ordinal";

        protected sealed override Regex AmbiguousFractionConnectorsRegex { get; }

        protected sealed override Regex RelativeReferenceRegex { get; }

        public static OrdinalExtractor GetInstance(BaseNumberOptionsConfiguration config)
        {
            var extractorKey = config.Options.ToString();

            if (!Instances.ContainsKey(extractorKey))
            {
                var instance = new OrdinalExtractor(config);
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