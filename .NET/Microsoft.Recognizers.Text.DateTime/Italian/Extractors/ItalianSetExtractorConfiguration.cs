﻿using System;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Italian;

namespace Microsoft.Recognizers.Text.DateTime.Italian
{
    public class ItalianSetExtractorConfiguration : BaseDateTimeOptionsConfiguration, ISetExtractorConfiguration
    {
        public static readonly string ExtractorName = Constants.SYS_DATETIME_SET;

        public static readonly Regex SetUnitRegex =
            RegexCache.Get(DateTimeDefinitions.DurationUnitRegex, RegexFlags);

        public static readonly Regex PeriodicRegex =
            RegexCache.Get(DateTimeDefinitions.PeriodicRegex, RegexFlags);

        public static readonly Regex EachUnitRegex =
            RegexCache.Get(DateTimeDefinitions.EachUnitRegex, RegexFlags);

        public static readonly Regex EachPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.EachPrefixRegex, RegexFlags);

        public static readonly Regex EachDayRegex =
            RegexCache.Get(DateTimeDefinitions.EachDayRegex, RegexFlags);

        public static readonly Regex SetLastRegex =
            RegexCache.Get(DateTimeDefinitions.SetLastRegex, RegexFlags);

        public static readonly Regex SetWeekDayRegex =
            RegexCache.Get(DateTimeDefinitions.SetWeekDayRegex, RegexFlags);

        public static readonly Regex SetEachRegex =
            RegexCache.Get(DateTimeDefinitions.SetEachRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public ItalianSetExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {
            DurationExtractor = new BaseDurationExtractor(new ItalianDurationExtractorConfiguration(this));
            TimeExtractor = new BaseTimeExtractor(new ItalianTimeExtractorConfiguration(this));
            DateExtractor = new BaseDateExtractor(new ItalianDateExtractorConfiguration(this));
            DateTimeExtractor = new BaseDateTimeExtractor(new ItalianDateTimeExtractorConfiguration(this));
            DatePeriodExtractor = new BaseDatePeriodExtractor(new ItalianDatePeriodExtractorConfiguration(this));
            TimePeriodExtractor = new BaseTimePeriodExtractor(new ItalianTimePeriodExtractorConfiguration(this));
            DateTimePeriodExtractor = new BaseDateTimePeriodExtractor(new ItalianDateTimePeriodExtractorConfiguration(this));
        }

        public IDateTimeExtractor DurationExtractor { get; }

        public IDateTimeExtractor TimeExtractor { get; }

        public IDateExtractor DateExtractor { get; }

        public IDateTimeExtractor DateTimeExtractor { get; }

        public IDateTimeExtractor DatePeriodExtractor { get; }

        public IDateTimeExtractor TimePeriodExtractor { get; }

        public IDateTimeExtractor DateTimePeriodExtractor { get; }

        bool ISetExtractorConfiguration.CheckBothBeforeAfter => DateTimeDefinitions.CheckBothBeforeAfter;

        Regex ISetExtractorConfiguration.LastRegex => SetLastRegex;

        Regex ISetExtractorConfiguration.EachPrefixRegex => EachPrefixRegex;

        Regex ISetExtractorConfiguration.PeriodicRegex => PeriodicRegex;

        Regex ISetExtractorConfiguration.EachUnitRegex => EachUnitRegex;

        Regex ISetExtractorConfiguration.EachDayRegex => EachDayRegex;

        Regex ISetExtractorConfiguration.BeforeEachDayRegex => null;

        Regex ISetExtractorConfiguration.SetWeekDayRegex => SetWeekDayRegex;

        Regex ISetExtractorConfiguration.SetEachRegex => SetEachRegex;

        public Tuple<string, int> WeekDayGroupMatchTuple(Match match)
        {

            string weekday = string.Empty;
            int del = 0;
            if (match.Groups["g0"].Length != 0)
            {
                weekday = match.Groups["g0"] + "a";
                del = 0;
            }
            else if (match.Groups["g1"].Length != 0)
            {
                weekday = match.Groups["g1"] + "io";
                del = -1;
            }
            else if (match.Groups["g2"].Length != 0)
            {
                weekday = match.Groups["g2"] + "e";
                del = 0;
            }
            else if (match.Groups["g3"].Length != 0)
            {
                weekday = match.Groups["g3"] + "ì";
                del = 0;
            }
            else if (match.Groups["g4"].Length != 0)
            {
                weekday = match.Groups["g4"] + "a";
                del = 1;
            }
            else if (match.Groups["g5"].Length != 0)
            {
                weekday = match.Groups["g5"] + "o";
                del = 0;
            }

            return Tuple.Create<string, int>(weekday, del);
        }
    }
}
