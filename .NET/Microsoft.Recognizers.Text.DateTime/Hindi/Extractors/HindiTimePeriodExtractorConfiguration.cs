﻿using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Hindi;
using Microsoft.Recognizers.Text.DateTime.Hindi.Utilities;
using Microsoft.Recognizers.Text.DateTime.Utilities;
using Microsoft.Recognizers.Text.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.Hindi
{
    public class HindiTimePeriodExtractorConfiguration : BaseDateTimeOptionsConfiguration, ITimePeriodExtractorConfiguration
    {
        public static readonly Regex TillRegex =
            RegexCache.Get(DateTimeDefinitions.TillRegex, RegexFlags);

        public static readonly Regex HourRegex =
            RegexCache.Get(DateTimeDefinitions.HourRegex, RegexFlags);

        public static readonly Regex PeriodHourNumRegex =
            RegexCache.Get(DateTimeDefinitions.PeriodHourNumRegex, RegexFlags);

        public static readonly Regex PeriodDescRegex =
            RegexCache.Get(DateTimeDefinitions.DescRegex, RegexFlags);

        public static readonly Regex PmRegex =
            RegexCache.Get(DateTimeDefinitions.PmRegex, RegexFlags);

        public static readonly Regex AmRegex =
            RegexCache.Get(DateTimeDefinitions.AmRegex, RegexFlags);

        public static readonly Regex PureNumFromTo =
            RegexCache.Get(DateTimeDefinitions.PureNumFromTo, RegexFlags);

        public static readonly Regex PureNumBetweenAnd =
            RegexCache.Get(DateTimeDefinitions.PureNumBetweenAnd, RegexFlags);

        public static readonly Regex SpecificTimeFromTo =
            RegexCache.Get(DateTimeDefinitions.SpecificTimeFromTo, RegexFlags);

        public static readonly Regex SpecificTimeBetweenAnd =
            RegexCache.Get(DateTimeDefinitions.SpecificTimeBetweenAnd, RegexFlags);

        public static readonly Regex PrepositionRegex =
            RegexCache.Get(DateTimeDefinitions.PrepositionRegex, RegexFlags);

        public static readonly Regex TimeOfDayRegex =
            RegexCache.Get(DateTimeDefinitions.TimeOfDayRegex, RegexFlags);

        public static readonly Regex SpecificTimeOfDayRegex =
            RegexCache.Get(DateTimeDefinitions.SpecificTimeOfDayRegex, RegexFlags);

        public static readonly Regex TimeUnitRegex =
            RegexCache.Get(DateTimeDefinitions.TimeUnitRegex, RegexFlags);

        public static readonly Regex TimeFollowedUnit =
            RegexCache.Get(DateTimeDefinitions.TimeFollowedUnit, RegexFlags);

        public static readonly Regex TimeNumberCombinedWithUnit =
            RegexCache.Get(DateTimeDefinitions.TimeNumberCombinedWithUnit, RegexFlags);

        public static readonly Regex GeneralEndingRegex =
            RegexCache.Get(DateTimeDefinitions.GeneralEndingRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex FromRegex =
            RegexCache.Get(DateTimeDefinitions.FromTokenRegex, RegexFlags);

        private static readonly Regex BetweenRegex =
            RegexCache.Get(DateTimeDefinitions.RangePrefixRegex, RegexFlags);

        private static readonly Regex RangeConnectorRegex =
            RegexCache.Get(DateTimeDefinitions.RangeConnectorRegex, RegexFlags);

        public HindiTimePeriodExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {
            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;
            SingleTimeExtractor = new BaseTimeExtractor(new HindiTimeExtractorConfiguration(this));
            UtilityConfiguration = new HindiDatetimeUtilityConfiguration();
            IntegerExtractor = Number.Hindi.IntegerExtractor.GetInstance();
        }

        public string TokenBeforeDate { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        public IDateTimeExtractor SingleTimeExtractor { get; }

        public IDateTimeExtractor TimeZoneExtractor { get; }

        public IExtractor IntegerExtractor { get; }

        public IEnumerable<Regex> SimpleCasesRegex => new[]
        {
            PureNumFromTo, PureNumBetweenAnd, SpecificTimeFromTo, SpecificTimeBetweenAnd,
        };

        public IEnumerable<Regex> PureNumberRegex => new[]
        {
            PureNumFromTo, PureNumBetweenAnd,
        };

        bool ITimePeriodExtractorConfiguration.CheckBothBeforeAfter => DateTimeDefinitions.CheckBothBeforeAfter;

        Regex ITimePeriodExtractorConfiguration.TillRegex => TillRegex;

        Regex ITimePeriodExtractorConfiguration.TimeOfDayRegex => TimeOfDayRegex;

        Regex ITimePeriodExtractorConfiguration.GeneralEndingRegex => GeneralEndingRegex;

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
            var betweenMatch = BetweenRegex.Match(text);
            if (betweenMatch.Success)
            {
                index = betweenMatch.Index + betweenMatch.Length;
            }

            return betweenMatch.Success;
        }

        public bool IsConnectorToken(string text)
        {
            return RangeConnectorRegex.IsExactMatch(text, trim: true);
        }

        public List<ExtractResult> ApplyPotentialPeriodAmbiguityHotfix(string text, List<ExtractResult> timePeriodErs) => TimePeriodFunctions.ApplyPotentialPeriodAmbiguityHotfix(text, timePeriodErs);
    }
}
