﻿using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Spanish;

namespace Microsoft.Recognizers.Text.Number.Spanish
{
    public class SpanishNumberRangeParserConfiguration : INumberRangeParserConfiguration
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public SpanishNumberRangeParserConfiguration(INumberOptionsConfiguration config)
        {

            CultureInfo = new CultureInfo(config.Culture);

            var numConfig = new BaseNumberOptionsConfiguration(config.Culture, config.Options);

            NumberExtractor = Spanish.NumberExtractor.GetInstance(numConfig);
            OrdinalExtractor = Spanish.OrdinalExtractor.GetInstance(numConfig);

            NumberParser = new BaseNumberParser(new SpanishNumberParserConfiguration(config));

            MoreOrEqual = RegexCache.Get(NumbersDefinitions.MoreOrEqual, RegexFlags);
            LessOrEqual = RegexCache.Get(NumbersDefinitions.LessOrEqual, RegexFlags);
            MoreOrEqualSuffix = RegexCache.Get(NumbersDefinitions.MoreOrEqualSuffix, RegexFlags);
            LessOrEqualSuffix = RegexCache.Get(NumbersDefinitions.LessOrEqualSuffix, RegexFlags);
            MoreOrEqualSeparate = RegexCache.Get(NumbersDefinitions.OneNumberRangeMoreSeparateRegex, RegexFlags);
            LessOrEqualSeparate = RegexCache.Get(NumbersDefinitions.OneNumberRangeLessSeparateRegex, RegexFlags);
        }

        public CultureInfo CultureInfo { get; private set; }

        public IExtractor NumberExtractor { get; private set; }

        public IExtractor OrdinalExtractor { get; private set; }

        public IParser NumberParser { get; private set; }

        public Regex MoreOrEqual { get; private set; }

        public Regex LessOrEqual { get; private set; }

        public Regex MoreOrEqualSuffix { get; private set; }

        public Regex LessOrEqualSuffix { get; private set; }

        public Regex MoreOrEqualSeparate { get; private set; }

        public Regex LessOrEqualSeparate { get; private set; }
    }
}
