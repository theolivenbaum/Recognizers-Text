﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Japanese;

namespace Microsoft.Recognizers.Text.Number.Japanese
{
    public class FractionExtractor : BaseNumberExtractor
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public FractionExtractor()
        {
            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    // -4 5/2,       ４ ６／３
                    RegexCache.Get(NumbersDefinitions.FractionNotationSpecialsCharsRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 8/3
                    RegexCache.Get(NumbersDefinitions.FractionNotationRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 五分の二   七分の三
                    RegexCache.Get(NumbersDefinitions.AllFractionNumber, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.FRACTION_PREFIX, Constants.JAPANESE)
                },
            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_FRACTION;
    }
}
