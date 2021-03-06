﻿using System.Collections.Generic;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Hindi;

namespace Microsoft.Recognizers.Text.DateTime.Hindi
{
    public class HindiHolidayExtractorConfiguration : BaseDateTimeOptionsConfiguration, IHolidayExtractorConfiguration
    {
        public static readonly Regex YearRegex =
            RegexCache.Get(DateTimeDefinitions.YearRegex, RegexFlags);

        public static readonly Regex H1 =
            RegexCache.Get(DateTimeDefinitions.HolidayRegex1, RegexFlags);

        public static readonly Regex H2 =
            RegexCache.Get(DateTimeDefinitions.HolidayRegex2, RegexFlags);

        public static readonly Regex H3 =
            RegexCache.Get(DateTimeDefinitions.HolidayRegex3, RegexFlags);

        public static readonly Regex[] HolidayRegexList =
        {
            H1,
            H2,
            H3,
        };

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public HindiHolidayExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {
        }

        public IEnumerable<Regex> HolidayRegexes => HolidayRegexList;
    }
}
