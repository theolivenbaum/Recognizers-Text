﻿using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Italian;

namespace Microsoft.Recognizers.Text.DateTime.Italian
{
    public class ItalianDateTimePeriodParserConfiguration : BaseDateTimeOptionsConfiguration, IDateTimePeriodParserConfiguration
    {
        public static readonly Regex MorningStartEndRegex =
            RegexCache.Get(DateTimeDefinitions.MorningStartEndRegex, RegexFlags);

        public static readonly Regex AfternoonStartEndRegex =
            RegexCache.Get(DateTimeDefinitions.AfternoonStartEndRegex, RegexFlags);

        public static readonly Regex EveningStartEndRegex =
            RegexCache.Get(DateTimeDefinitions.EveningStartEndRegex, RegexFlags);

        public static readonly Regex NightStartEndRegex =
            RegexCache.Get(DateTimeDefinitions.NightStartEndRegex, RegexFlags);

        public static readonly Regex PastSuffixRegex =
            RegexCache.Get(DateTimeDefinitions.PastSuffixRegex, RegexFlags);

        public static readonly Regex NextSuffixRegex =
            RegexCache.Get(DateTimeDefinitions.NextSuffixRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public ItalianDateTimePeriodParserConfiguration(ICommonDateTimeParserConfiguration config)
            : base(config)
        {
            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;
            TokenBeforeTime = DateTimeDefinitions.TokenBeforeTime;

            DateExtractor = config.DateExtractor;
            TimeExtractor = config.TimeExtractor;
            DateTimeExtractor = config.DateTimeExtractor;
            TimePeriodExtractor = config.TimePeriodExtractor;
            CardinalExtractor = config.CardinalExtractor;
            DurationExtractor = config.DurationExtractor;
            NumberParser = config.NumberParser;
            DateParser = config.DateParser;
            TimeParser = config.TimeParser;
            TimePeriodParser = config.TimePeriodParser;
            DurationParser = config.DurationParser;
            DateTimeParser = config.DateTimeParser;
            TimeZoneParser = config.TimeZoneParser;

            PureNumberFromToRegex = ItalianTimePeriodExtractorConfiguration.PureNumFromTo;
            HyphenDateRegex = ItalianDateTimePeriodExtractorConfiguration.HyphenDateRegex;
            PureNumberBetweenAndRegex = ItalianTimePeriodExtractorConfiguration.PureNumBetweenAnd;
            SpecificTimeOfDayRegex = ItalianDateTimeExtractorConfiguration.SpecificTimeOfDayRegex;
            TimeOfDayRegex = ItalianDateTimeExtractorConfiguration.TimeOfDayRegex;
            PreviousPrefixRegex = ItalianDatePeriodExtractorConfiguration.PreviousPrefixRegex;
            FutureRegex = ItalianDatePeriodExtractorConfiguration.NextPrefixRegex;
            FutureSuffixRegex = ItalianDatePeriodExtractorConfiguration.FutureSuffixRegex;
            NumberCombinedWithUnitRegex = ItalianDateTimePeriodExtractorConfiguration.TimeNumberCombinedWithUnit;
            UnitRegex = ItalianTimePeriodExtractorConfiguration.TimeUnitRegex;
            PeriodTimeOfDayWithDateRegex = ItalianDateTimePeriodExtractorConfiguration.PeriodTimeOfDayWithDateRegex;
            RelativeTimeUnitRegex = ItalianDateTimePeriodExtractorConfiguration.RelativeTimeUnitRegex;
            RestOfDateTimeRegex = ItalianDateTimePeriodExtractorConfiguration.RestOfDateTimeRegex;
            AmDescRegex = ItalianDateTimePeriodExtractorConfiguration.AmDescRegex;
            PmDescRegex = ItalianDateTimePeriodExtractorConfiguration.PmDescRegex;
            WithinNextPrefixRegex = ItalianDateTimePeriodExtractorConfiguration.WithinNextPrefixRegex;
            PrefixDayRegex = ItalianDateTimePeriodExtractorConfiguration.PrefixDayRegex;
            BeforeRegex = ItalianDateTimePeriodExtractorConfiguration.BeforeRegex;
            AfterRegex = ItalianDateTimePeriodExtractorConfiguration.AfterRegex;

            UnitMap = config.UnitMap;
            Numbers = config.Numbers;
        }

        public string TokenBeforeDate { get; }

        public string TokenBeforeTime { get; }

        public IDateExtractor DateExtractor { get; }

        public IDateTimeExtractor TimeExtractor { get; }

        public IDateTimeExtractor DateTimeExtractor { get; }

        public IDateTimeExtractor TimePeriodExtractor { get; }

        public IExtractor CardinalExtractor { get; }

        public IDateTimeExtractor DurationExtractor { get; }

        public IParser NumberParser { get; }

        public IDateTimeParser DateParser { get; }

        public IDateTimeParser TimeParser { get; }

        public IDateTimeParser DateTimeParser { get; }

        public IDateTimeParser TimePeriodParser { get; }

        public IDateTimeParser DurationParser { get; }

        public IDateTimeParser TimeZoneParser { get; }

        public Regex PureNumberFromToRegex { get; }

        public Regex HyphenDateRegex { get; }

        public Regex PureNumberBetweenAndRegex { get; }

        public Regex SpecificTimeOfDayRegex { get; }

        public Regex TimeOfDayRegex { get; }

        public Regex PreviousPrefixRegex { get; }

        public Regex FutureRegex { get; }

        public Regex FutureSuffixRegex { get; }

        public Regex NumberCombinedWithUnitRegex { get; }

        public Regex UnitRegex { get; }

        public Regex PeriodTimeOfDayWithDateRegex { get; }

        public Regex RelativeTimeUnitRegex { get; }

        public Regex RestOfDateTimeRegex { get; }

        public Regex AmDescRegex { get; }

        public Regex PmDescRegex { get; }

        public Regex WithinNextPrefixRegex { get; }

        public Regex PrefixDayRegex { get; }

        public Regex BeforeRegex { get; }

        public Regex AfterRegex { get; }

        bool IDateTimePeriodParserConfiguration.CheckBothBeforeAfter => DateTimeDefinitions.CheckBothBeforeAfter;

        public IImmutableDictionary<string, string> UnitMap { get; }

        public IImmutableDictionary<string, int> Numbers { get; }

        public bool GetMatchedTimeRange(string text, out string timeStr, out int beginHour, out int endHour, out int endMin)
        {
            var trimmedText = text.Trim();
            beginHour = 0;
            endHour = 0;
            endMin = 0;
            if (MorningStartEndRegex.IsMatch(trimmedText))
            {
                timeStr = "TMO";
                beginHour = 8;
                endHour = 12;
            }
            else if (AfternoonStartEndRegex.IsMatch(trimmedText))
            {
                timeStr = "TAF";
                beginHour = 12;
                endHour = 16;
            }
            else if (EveningStartEndRegex.IsMatch(trimmedText))
            {
                timeStr = "TEV";
                beginHour = 16;
                endHour = 20;
            }
            else if (NightStartEndRegex.IsMatch(trimmedText))
            {
                timeStr = "TNI";
                beginHour = 20;
                endHour = 23;
                endMin = 59;
            }
            else
            {
                timeStr = null;
                return false;
            }

            return true;
        }

        // **NOTE: for certain cases, prochain/dernier (next, last) are suffix OR prefix
        public int GetSwiftPrefix(string text)
        {
            var trimmedText = text.Trim();
            var swift = 0;
            if (NextSuffixRegex.IsMatch(trimmedText))
            {
                swift = 1;
            }
            else if (PastSuffixRegex.IsMatch(trimmedText))
            {
                swift = -1;
            }

            return swift;
        }
    }
}
