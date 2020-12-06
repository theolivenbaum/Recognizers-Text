﻿using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Portuguese;

namespace Microsoft.Recognizers.Text.Number.Portuguese
{
    public class FractionExtractor : BaseNumberExtractor
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly ConcurrentDictionary<(NumberMode, NumberOptions), FractionExtractor> Instances =
            new ConcurrentDictionary<(NumberMode, NumberOptions), FractionExtractor>();

        private FractionExtractor(BaseNumberOptionsConfiguration config)
            : base(config.Options)
        {

            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    RegexCache.Get(NumbersDefinitions.FractionNotationWithSpacesRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.FractionNotationRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.FractionNounRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.PORTUGUESE)
                },
                {
                    RegexCache.Get(NumbersDefinitions.FractionNounWithArticleRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.PORTUGUESE)
                },
            };

            // Not add FractionPrepositionRegex when the mode is Unit to avoid wrong recognize cases like "$1000 over 3"
            if (config.Mode != NumberMode.Unit)
            {
                regexes.Add(
                    RegexCache.Get(NumbersDefinitions.FractionPrepositionRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.PORTUGUESE));
            }

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_FRACTION; // "Fraction";

        public static FractionExtractor GetInstance(BaseNumberOptionsConfiguration config)
        {
            var extractorKey = (config.Mode, config.Options);

            if (!Instances.ContainsKey(extractorKey))
            {
                var instance = new FractionExtractor(config);
                Instances.TryAdd(extractorKey, instance);
            }

            return Instances[extractorKey];
        }
    }
}