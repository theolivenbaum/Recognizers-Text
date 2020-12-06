﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Chinese;

namespace Microsoft.Recognizers.Text.DateTime.Chinese
{
    public class ChineseTimePeriodExtractorChsConfiguration : ChineseBaseDateTimeExtractorConfiguration<PeriodType>
    {
        public const string TimePeriodConnectWords = DateTimeDefinitions.TimePeriodTimePeriodConnectWords;

        // 五点十分四十八秒
        public static readonly string ChineseTimeRegex = ChineseTimeExtractorConfiguration.ChineseTimeRegex;

        // 六点 到 九点 | 六 到 九点
        public static readonly string LeftChsTimeRegex = DateTimeDefinitions.TimePeriodLeftChsTimeRegex;

        public static readonly string RightChsTimeRegex = DateTimeDefinitions.TimePeriodRightChsTimeRegex;

        // 2:45
        public static readonly string DigitTimeRegex = ChineseTimeExtractorConfiguration.DigitTimeRegex;

        public static readonly string LeftDigitTimeRegex = DateTimeDefinitions.TimePeriodLeftDigitTimeRegex;

        public static readonly string RightDigitTimeRegex = DateTimeDefinitions.TimePeriodRightDigitTimeRegex;

        public static readonly string ShortLeftChsTimeRegex = DateTimeDefinitions.TimePeriodShortLeftChsTimeRegex;

        public static readonly string ShortLeftDigitTimeRegex = DateTimeDefinitions.TimePeriodShortLeftDigitTimeRegex;

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public ChineseTimePeriodExtractorChsConfiguration()
        {
            var regexes = new Dictionary<Regex, PeriodType>
            {
                {
                    RegexCache.Get(DateTimeDefinitions.TimePeriodRegexes1, RegexFlags),
                    PeriodType.FullTime
                },
                {
                    RegexCache.Get(DateTimeDefinitions.TimePeriodRegexes2, RegexFlags),
                    PeriodType.ShortTime
                },
                {
                    RegexCache.Get(DateTimeDefinitions.TimeOfDayRegex, RegexFlags),
                    PeriodType.ShortTime
                },
            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, PeriodType> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_DATETIME_TIMEPERIOD;
    }
}