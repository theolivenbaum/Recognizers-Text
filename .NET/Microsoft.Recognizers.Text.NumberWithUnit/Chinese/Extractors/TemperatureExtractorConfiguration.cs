﻿using System.Collections.Immutable;
using System.Globalization;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions;
using Microsoft.Recognizers.Definitions.Chinese;

namespace Microsoft.Recognizers.Text.NumberWithUnit.Chinese
{
    public class TemperatureExtractorConfiguration : ChineseNumberWithUnitExtractorConfiguration
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex AmbiguousUnitMultiplierRegex =
            RegexCache.Get(BaseUnits.AmbiguousUnitNumberMultiplierRegex, RegexFlags);

        public TemperatureExtractorConfiguration()
            : this(new CultureInfo(Culture.Chinese))
        {
        }

        public TemperatureExtractorConfiguration(CultureInfo ci)
            : base(ci)
        {
        }

        public override ImmutableDictionary<string, string> SuffixList =>
            NumbersWithUnitDefinitions.TemperatureSuffixList.ToImmutableDictionary();

        public override ImmutableDictionary<string, string> PrefixList =>
            NumbersWithUnitDefinitions.TemperaturePrefixList.ToImmutableDictionary();

        public override ImmutableList<string> AmbiguousUnitList =>
            NumbersWithUnitDefinitions.TemperatureAmbiguousValues.ToImmutableList();

        public override string ExtractType => Constants.SYS_UNIT_TEMPERATURE;

        public override Regex AmbiguousUnitNumberMultiplierRegex => AmbiguousUnitMultiplierRegex;
    }
}