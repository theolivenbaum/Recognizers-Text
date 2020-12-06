﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Japanese;
using Microsoft.Recognizers.Text.Number.Config;

namespace Microsoft.Recognizers.Text.Number.Japanese
{
    public class IntegerExtractor : BaseNumberExtractor
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public IntegerExtractor(CJKNumberExtractorMode mode = CJKNumberExtractorMode.Default)
        {
            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    // 123456,  －１２３４５６
                    RegexCache.Get(NumbersDefinitions.NumbersSpecialsChars, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 15k,  16 G
                    RegexCache.Get(NumbersDefinitions.NumbersSpecialsCharsWithSuffix, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 1,234,  ２，３３２，１１１
                    RegexCache.Get(NumbersDefinitions.DottedNumbersSpecialsChar, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.NUMBER_SUFFIX)
                },
                {
                    // 半百  半ダース
                    RegexCache.Get(NumbersDefinitions.NumbersWithHalfDozen, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.JAPANESE)
                },
                {
                    // 一ダース  五十ダース
                    RegexCache.Get(NumbersDefinitions.NumbersWithDozen, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.JAPANESE)
                },
            };

            switch (mode)
            {
                case CJKNumberExtractorMode.Default:
                    // 一百五十五, 负一亿三百二十二.
                    // Uses an allow list to avoid extracting "西九条" from "九"
                    regexes.Add(
                        RegexCache.Get(NumbersDefinitions.NumbersWithAllowListRegex, RegexFlags),
                        RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.JAPANESE));
                    break;

                case CJKNumberExtractorMode.ExtractAll:
                    // 一百五十五, 负一亿三百二十二, "西九条" from "九"
                    // Uses no allow lists and extracts all potential integers (useful in Units, for example).
                    regexes.Add(
                        RegexCache.Get(NumbersDefinitions.NumbersAggressiveRegex, RegexFlags),
                        RegexTagGenerator.GenerateRegexTag(Constants.INTEGER_PREFIX, Constants.JAPANESE));
                    break;
            }

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_INTEGER;
    }
}
