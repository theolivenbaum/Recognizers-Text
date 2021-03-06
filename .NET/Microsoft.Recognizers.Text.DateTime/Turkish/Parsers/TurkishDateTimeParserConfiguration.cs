﻿using System.Collections.Immutable;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Turkish;
using Microsoft.Recognizers.Text.DateTime.Utilities;
using Microsoft.Recognizers.Text.Utilities;

namespace Microsoft.Recognizers.Text.DateTime.Turkish
{
    public class TurkishDateTimeParserConfiguration : BaseDateTimeOptionsConfiguration, IDateTimeParserConfiguration
    {
        public static readonly Regex AmTimeRegex =
             RegexCache.Get(DateTimeDefinitions.AMTimeRegex, RegexFlags);

        public static readonly Regex PmTimeRegex =
            RegexCache.Get(DateTimeDefinitions.PMTimeRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex NowTimeRegex =
            RegexCache.Get(DateTimeDefinitions.NowTimeRegex, RegexFlags);

        private static readonly Regex RecentlyTimeRegex =
            RegexCache.Get(DateTimeDefinitions.RecentlyTimeRegex, RegexFlags);

        private static readonly Regex AsapTimeRegex =
            RegexCache.Get(DateTimeDefinitions.AsapTimeRegex, RegexFlags);

        private static readonly Regex NextPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.NextPrefixRegex, RegexFlags);

        private static readonly Regex PreviousPrefixRegex =
            RegexCache.Get(DateTimeDefinitions.PreviousPrefixRegex, RegexFlags);

        public TurkishDateTimeParserConfiguration(ICommonDateTimeParserConfiguration config)
         : base(config)
        {
            TokenBeforeDate = DateTimeDefinitions.TokenBeforeDate;
            TokenBeforeTime = DateTimeDefinitions.TokenBeforeTime;

            DateExtractor = config.DateExtractor;
            TimeExtractor = config.TimeExtractor;
            DateParser = config.DateParser;
            TimeParser = config.TimeParser;

            NowRegex = TurkishDateTimeExtractorConfiguration.NowRegex;

            SimpleTimeOfTodayAfterRegex = TurkishDateTimeExtractorConfiguration.SimpleTimeOfTodayAfterRegex;
            SimpleTimeOfTodayBeforeRegex = TurkishDateTimeExtractorConfiguration.SimpleTimeOfTodayBeforeRegex;
            SpecificTimeOfDayRegex = TurkishDateTimeExtractorConfiguration.SpecificTimeOfDayRegex;
            SpecificEndOfRegex = TurkishDateTimeExtractorConfiguration.SpecificEndOfRegex;
            UnspecificEndOfRegex = TurkishDateTimeExtractorConfiguration.UnspecificEndOfRegex;
            UnitRegex = TurkishTimeExtractorConfiguration.TimeUnitRegex;
            DateNumberConnectorRegex = TurkishDateTimeExtractorConfiguration.DateNumberConnectorRegex;
            YearRegex = TurkishDateTimeExtractorConfiguration.YearRegex;

            Numbers = config.Numbers;
            CardinalExtractor = config.CardinalExtractor;
            IntegerExtractor = config.IntegerExtractor;
            NumberParser = config.NumberParser;
            DurationExtractor = config.DurationExtractor;
            DurationParser = config.DurationParser;
            UnitMap = config.UnitMap;
            UtilityConfiguration = config.UtilityConfiguration;
        }

        public string TokenBeforeDate { get; }

        public string TokenBeforeTime { get; }

        public IDateExtractor DateExtractor { get; }

        public IDateTimeExtractor TimeExtractor { get; }

        public IDateTimeParser DateParser { get; }

        public IDateTimeParser TimeParser { get; }

        public IExtractor CardinalExtractor { get; }

        public IExtractor IntegerExtractor { get; }

        public IParser NumberParser { get; }

        public IDateTimeExtractor DurationExtractor { get; }

        public IDateTimeParser DurationParser { get; }

        public IImmutableDictionary<string, string> UnitMap { get; }

        public Regex NowRegex { get; }

        public Regex AMTimeRegex => AmTimeRegex;

        public Regex PMTimeRegex => PmTimeRegex;

        public Regex SimpleTimeOfTodayAfterRegex { get; }

        public Regex SimpleTimeOfTodayBeforeRegex { get; }

        public Regex SpecificTimeOfDayRegex { get; }

        public Regex SpecificEndOfRegex { get; }

        public Regex UnspecificEndOfRegex { get; }

        public Regex UnitRegex { get; }

        public Regex DateNumberConnectorRegex { get; }

        public Regex PrepositionRegex { get; }

        public Regex ConnectorRegex { get; }

        public Regex YearRegex { get; }

        public IImmutableDictionary<string, int> Numbers { get; }

        public IDateTimeUtilityConfiguration UtilityConfiguration { get; }

        public int GetHour(string text, int hour)
        {
            int result = hour;

            var trimmedText = text.Trim();

            if (AMTimeRegex.MatchEnd(trimmedText, trim: true).Success && hour >= Constants.HalfDayHourCount)
            {
                result -= Constants.HalfDayHourCount;
            }
            else if (!AMTimeRegex.MatchEnd(trimmedText, trim: true).Success && hour < Constants.HalfDayHourCount)
            {
                result += Constants.HalfDayHourCount;
            }

            return result;
        }

        public bool GetMatchedNowTimex(string text, out string timex)
        {
            var trimmedText = text.Trim();

            if (NowTimeRegex.MatchEnd(trimmedText, trim: true).Success)
            {
                timex = "PRESENT_REF";
            }
            else if (RecentlyTimeRegex.IsExactMatch(trimmedText, trim: true))
            {
                timex = "PAST_REF";
            }
            else if (AsapTimeRegex.IsExactMatch(trimmedText, trim: true))
            {
                timex = "FUTURE_REF";
            }
            else
            {
                timex = null;
                return false;
            }

            return true;
        }

        public int GetSwiftDay(string text)
        {
            var trimmedText = text.Trim();

            var swift = 0;
            if (NextPrefixRegex.MatchBegin(trimmedText, trim: true).Success)
            {
                swift = 1;
            }
            else if (PreviousPrefixRegex.MatchBegin(trimmedText, trim: true).Success)
            {
                swift = -1;
            }

            return swift;
        }

        public bool ContainsAmbiguousToken(string text, string matchedText) => false;
    }
}
