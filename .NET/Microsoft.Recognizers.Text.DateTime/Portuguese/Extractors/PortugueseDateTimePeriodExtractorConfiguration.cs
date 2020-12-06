﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions;
using Microsoft.Recognizers.Definitions.Portuguese;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.Portuguese
{
    public class PortugueseDateTimePeriodExtractorConfiguration : BaseDateTimeOptionsConfiguration, IDateTimePeriodExtractorConfiguration
    {
        public static readonly Regex NumberCombinedWithUnit =
            RegexCache.Get(DateTimeDefinitions.DateTimePeriodNumberCombinedWithUnit, RegexFlags);

        public static readonly Regex WeekDayRegex =
            RegexCache.Get(DateTimeDefinitions.WeekDayRegex, RegexFlags);

        public static readonly Regex RestOfDateTimeRegex =
            RegexCache.Get(DateTimeDefinitions.RestOfDateTimeRegex, RegexFlags);

        public static readonly Regex HyphenDateRegex =
            RegexCache.Get(BaseDateTime.HyphenDateRegex, RegexFlags);

        public static readonly Regex PeriodTimeOfDayWithDateRegex =
            RegexCache.Get(DateTimeDefinitions.PeriodTimeOfDayWithDateRegex, RegexFlags);

        public static readonly Regex RelativeTimeUnitRegex =
            RegexCache.Get(DateTimeDefinitions.RelativeTimeUnitRegex, RegexFlags);

        public static readonly Regex GeneralEndingRegex =
            RegexCache.Get(DateTimeDefinitions.GeneralEndingRegex, RegexFlags);

        public static readonly Regex MiddlePauseRegex =
            RegexCache.Get(DateTimeDefinitions.MiddlePauseRegex, RegexFlags);

        public static readonly Regex AmDescRegex =
            RegexCache.Get(DateTimeDefinitions.AmDescRegex, RegexFlags);

        public static readonly Regex PmDescRegex =
            RegexCache.Get(DateTimeDefinitions.PmDescRegex, RegexFlags);

        public static readonly Regex WithinNextPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.WithinNextPrefixRegex, RegexFlags);

        public static readonly Regex DateUnitRegex =
            RegexCache.Get(DateTimeDefinitions.DateUnitRegex, RegexFlags);

        public static readonly Regex PrefixDayRegex =
            RegexCache.Get(DateTimeDefinitions.PrefixDayRegex, RegexFlags | RegexOptions.RightToLeft);

        public static readonly Regex SuffixRegex =
            RegexCache.Get(DateTimeDefinitions.SuffixRegex, RegexFlags);

        public static readonly Regex BeforeRegex =
            RegexCache.Get(DateTimeDefinitions.BeforeRegex, RegexFlags);

        public static readonly Regex AfterRegex =
            RegexCache.Get(DateTimeDefinitions.AfterRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex FromRegex =
            RegexCache.Get(DateTimeDefinitions.FromRegex, RegexFlags);

        private static readonly Regex RangeConnectorRegex =
            RegexCache.Get(DateTimeDefinitions.RangeConnectorRegex, RegexFlags);

        private static readonly Regex BetweenRegex =
            RegexCache.Get(DateTimeDefinitions.BetweenRegex, RegexFlags);

        public PortugueseDateTimePeriodExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {
            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;

            var numOptions = NumberOptions.None;
            if ((config.Options & DateTimeOptions.NoProtoCache) != 0)
            {
                numOptions = NumberOptions.NoProtoCache;
            }

            var numConfig = new BaseNumberOptionsConfiguration(config.Culture, numOptions);

            CardinalExtractor = Number.Portuguese.CardinalExtractor.GetInstance(numConfig);

            SingleDateExtractor = new BaseDateExtractor(new PortugueseDateExtractorConfiguration(this));
            SingleTimeExtractor = new BaseTimeExtractor(new PortugueseTimeExtractorConfiguration(this));
            SingleDateTimeExtractor = new BaseDateTimeExtractor(new PortugueseDateTimeExtractorConfiguration(this));
            DurationExtractor = new BaseDurationExtractor(new PortugueseDurationExtractorConfiguration(this));
            TimePeriodExtractor = new BaseTimePeriodExtractor(new PortugueseTimePeriodExtractorConfiguration(this));
            TimeZoneExtractor = new BaseTimeZoneExtractor(new PortugueseTimeZoneExtractorConfiguration(this));
        }

        public string TokenBeforeDate { get; }

        public IExtractor CardinalExtractor { get; }

        public IDateTimeExtractor SingleDateExtractor { get; }

        public IDateTimeExtractor SingleTimeExtractor { get; }

        public IDateTimeExtractor SingleDateTimeExtractor { get; }

        public IDateTimeExtractor DurationExtractor { get; }

        public IDateTimeExtractor TimePeriodExtractor { get; }

        public IDateTimeExtractor TimeZoneExtractor { get; }

        public IEnumerable<Regex> SimpleCasesRegex => new[]
        {
            PortugueseTimePeriodExtractorConfiguration.PureNumFromTo,
            PortugueseTimePeriodExtractorConfiguration.PureNumBetweenAnd,
        };

        public Regex PrepositionRegex => PortugueseDateTimeExtractorConfiguration.PrepositionRegex;

        public Regex TillRegex => PortugueseDatePeriodExtractorConfiguration.TillRegex;

        public Regex SpecificTimeOfDayRegex => PortugueseDateTimeExtractorConfiguration.SpecificTimeOfDayRegex;

        public Regex TimeOfDayRegex => PortugueseDateTimeExtractorConfiguration.TimeOfDayRegex;

        public Regex FollowedUnit => PortugueseTimePeriodExtractorConfiguration.FollowedUnit;

        public Regex TimeUnitRegex => PortugueseTimePeriodExtractorConfiguration.UnitRegex;

        public Regex PreviousPrefixRegex => PortugueseDatePeriodExtractorConfiguration.PastRegex;

        public Regex NextPrefixRegex => PortugueseDatePeriodExtractorConfiguration.FutureRegex;

        public Regex FutureSuffixRegex => PortugueseDatePeriodExtractorConfiguration.FutureSuffixRegex;

        bool IDateTimePeriodExtractorConfiguration.CheckBothBeforeAfter => DateTimeDefinitions.CheckBothBeforeAfter;

        Regex IDateTimePeriodExtractorConfiguration.PrefixDayRegex => PrefixDayRegex;

        Regex IDateTimePeriodExtractorConfiguration.DateUnitRegex => DateUnitRegex;

        Regex IDateTimePeriodExtractorConfiguration.NumberCombinedWithUnit => NumberCombinedWithUnit;

        Regex IDateTimePeriodExtractorConfiguration.WeekDayRegex => WeekDayRegex;

        Regex IDateTimePeriodExtractorConfiguration.PeriodTimeOfDayWithDateRegex => PeriodTimeOfDayWithDateRegex;

        Regex IDateTimePeriodExtractorConfiguration.RelativeTimeUnitRegex => RelativeTimeUnitRegex;

        Regex IDateTimePeriodExtractorConfiguration.RestOfDateTimeRegex => RestOfDateTimeRegex;

        Regex IDateTimePeriodExtractorConfiguration.GeneralEndingRegex => GeneralEndingRegex;

        Regex IDateTimePeriodExtractorConfiguration.MiddlePauseRegex => MiddlePauseRegex;

        Regex IDateTimePeriodExtractorConfiguration.AmDescRegex => AmDescRegex;

        Regex IDateTimePeriodExtractorConfiguration.PmDescRegex => PmDescRegex;

        Regex IDateTimePeriodExtractorConfiguration.WithinNextPrefixRegex => WithinNextPrefixRegex;

        Regex IDateTimePeriodExtractorConfiguration.SuffixRegex => SuffixRegex;

        Regex IDateTimePeriodExtractorConfiguration.BeforeRegex => BeforeRegex;

        Regex IDateTimePeriodExtractorConfiguration.AfterRegex => AfterRegex;

        public bool GetFromTokenIndex(string text, out int index)
        {
            index = -1;
            var fromMatch = FromRegex.Match(text);
            if (fromMatch.Success)
            {
                index = fromMatch.Index;
            }

            return fromMatch.Success;
        }

        public bool GetBetweenTokenIndex(string text, out int index)
        {
            index = -1;

            var match = BetweenRegex.Match(text);
            if (match.Success)
            {
                index = match.Index;
            }

            return match.Success;
        }

        public bool HasConnectorToken(string text)
        {
            return RangeConnectorRegex.IsExactMatch(text, true);
        }
    }
}
