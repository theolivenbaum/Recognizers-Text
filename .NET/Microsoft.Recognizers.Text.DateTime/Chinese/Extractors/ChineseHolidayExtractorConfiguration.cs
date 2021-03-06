﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Chinese;

namespace Microsoft.Recognizers.Text.DateTime.Chinese
{
    public class ChineseHolidayExtractorConfiguration : BaseDateTimeOptionsConfiguration, IHolidayExtractorConfiguration
    {

        public static readonly Regex LunarHolidayRegex = RegexCache.Get(DateTimeDefinitions.LunarHolidayRegex, RegexFlags);

        public static readonly Regex[] HolidayRegexList =
        {
            RegexCache.Get(DateTimeDefinitions.HolidayRegexList1, RegexFlags),
            RegexCache.Get(DateTimeDefinitions.HolidayRegexList2, RegexFlags),
            LunarHolidayRegex,
        };

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public ChineseHolidayExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {
        }

        public IEnumerable<Regex> HolidayRegexes => HolidayRegexList;
    }
}