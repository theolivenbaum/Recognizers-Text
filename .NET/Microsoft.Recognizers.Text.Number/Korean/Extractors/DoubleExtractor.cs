﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Korean;

namespace Microsoft.Recognizers.Text.Number.Korean
{
    public class DoubleExtractor : BaseNumberExtractor
    {
        internal sealed override ImmutableDictionary<Regex, string> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_DOUBLE;

        public DoubleExtractor()
        {
            var regexes = new Dictionary<Regex, string>()
            {
                {
                    new Regex(NumbersDefinitions.DoubleSpecialsChars, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // (-)2.5, can avoid cases like ip address xx.xx.xx.xx
                    new Regex(NumbersDefinitions.DoubleSpecialsCharsWithNegatives, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    //(-).2 
                    new Regex(NumbersDefinitions.SimpleDoubleSpecialsChars, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 1.0 K
                    new Regex(NumbersDefinitions.DoubleWithMultiplierRegex, RegexOptions.Singleline),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    //１５.２만
                    new Regex(NumbersDefinitions.DoubleWithThousandsRegex, RegexOptions.Singleline),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 2e6, 21.2e0
                    new Regex(NumbersDefinitions.DoubleExponentialNotationRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    //2^5
                    new Regex(NumbersDefinitions.DoubleScientificNotationRegex, RegexOptions.IgnoreCase | RegexOptions.Singleline),
                    RegexTagGenerator.GenerateRegexTag(Constants.DOUBLE_PREFIX, Constants.NUMBER_SUFFIX)
                }
            };

            Regexes = regexes.ToImmutableDictionary();
        }
    }
}
