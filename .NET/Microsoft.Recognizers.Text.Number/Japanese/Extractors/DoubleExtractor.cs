﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Japanese;

namespace Microsoft.Recognizers.Text.Number.Japanese
{
    public class DoubleExtractor : BaseNumberExtractor
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public DoubleExtractor()
        {
            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    RegexCache.Get(NumbersDefinitions.DoubleSpecialsChars, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // (-)2.5, can avoid cases like ip address xx.xx.xx.xx
                    RegexCache.Get(NumbersDefinitions.DoubleSpecialsCharsWithNegatives, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // (-).2
                    RegexCache.Get(NumbersDefinitions.SimpleDoubleSpecialsChars, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // えは九・二三二一三一二
                    RegexCache.Get(NumbersDefinitions.DoubleRoundNumberSpecialsChars, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 1.0 K
                    RegexCache.Get(NumbersDefinitions.DoubleWithMultiplierRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // １５.２万
                    RegexCache.Get(NumbersDefinitions.DoubleWithThousandsRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.JAPANESE)
                },
                {
                    // 2e6, 21.2e0
                    RegexCache.Get(NumbersDefinitions.DoubleExponentialNotationRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.POWER_SUFFIX)
                },
                {
                    RegexCache.Get(NumbersDefinitions.DoubleExponentialNotationKanjiRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.POWER_SUFFIX)
                },
                {
                    // 2^5
                    RegexCache.Get(NumbersDefinitions.DoubleScientificNotationRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.POWER_SUFFIX)
                },
                {
                    // １　２３４　５６７．８９
                    GenerateLongFormatNumberRegexes(LongFormatType.DoubleNumFullWidthBlankDot),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 1 234 567.89
                    GenerateLongFormatNumberRegexes(LongFormatType.DoubleNumBlankDot),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_DOUBLE;
    }
}
