﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions;
using Microsoft.Recognizers.Definitions.French;
using Microsoft.Recognizers.Definitions.Utilities;
using Microsoft.Recognizers.Text.Matcher;

namespace Microsoft.Recognizers.Text.DateTime.French
{
    public class FrenchMergedExtractorConfiguration : BaseDateTimeOptionsConfiguration, IMergedExtractorConfiguration
    {
        // avant - 'before'
        public static readonly Regex BeforeRegex =
            RegexCache.Get(DateTimeDefinitions.BeforeRegex, RegexFlags);

        // ensuite/puis are for adverbs, i.e 'i ate and then i walked', so we'll use apres
        public static readonly Regex AfterRegex =
            RegexCache.Get(DateTimeDefinitions.AfterRegex, RegexFlags);

        public static readonly Regex SinceRegex =
            RegexCache.Get(DateTimeDefinitions.SinceRegex, RegexFlags);

        public static readonly Regex AroundRegex =
            RegexCache.Get(DateTimeDefinitions.AroundRegex, RegexFlags);

        public static readonly Regex EqualRegex =
            RegexCache.Get(BaseDateTime.EqualRegex, RegexFlags);

        // 'Je vais du lundi au mecredi' - I will go from monday to weds
        public static readonly Regex FromToRegex =
            RegexCache.Get(DateTimeDefinitions.FromToRegex, RegexFlags);

        public static readonly Regex SingleAmbiguousMonthRegex =
            RegexCache.Get(DateTimeDefinitions.SingleAmbiguousMonthRegex, RegexFlags);

        public static readonly Regex PrepositionSuffixRegex =
            RegexCache.Get(DateTimeDefinitions.PrepositionSuffixRegex, RegexFlags);

        public static readonly Regex AmbiguousRangeModifierPrefix =
            RegexCache.Get(DateTimeDefinitions.AmbiguousRangeModifierPrefix, RegexFlags);

        public static readonly Regex NumberEndingPattern =
            RegexCache.Get(DateTimeDefinitions.NumberEndingPattern, RegexFlags);

        public static readonly Regex SuffixAfterRegex =
            RegexCache.Get(DateTimeDefinitions.SuffixAfterRegex, RegexFlags);

        public static readonly Regex UnspecificDatePeriodRegex =
            RegexCache.Get(DateTimeDefinitions.UnspecificDatePeriodRegex, RegexFlags);

        public static readonly Regex[] TermFilterRegexes = System.Array.Empty<Regex>();

        public static readonly StringMatcher SuperfluousWordMatcher = new StringMatcher();

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public FrenchMergedExtractorConfiguration(IDateTimeOptionsConfiguration config)
            : base(config)
        {
            DateExtractor = new BaseDateExtractor(new FrenchDateExtractorConfiguration(this));
            TimeExtractor = new BaseTimeExtractor(new FrenchTimeExtractorConfiguration(this));
            DateTimeExtractor = new BaseDateTimeExtractor(new FrenchDateTimeExtractorConfiguration(this));
            DatePeriodExtractor = new BaseDatePeriodExtractor(new FrenchDatePeriodExtractorConfiguration(this));
            TimePeriodExtractor = new BaseTimePeriodExtractor(new FrenchTimePeriodExtractorConfiguration(this));
            DateTimePeriodExtractor = new BaseDateTimePeriodExtractor(new FrenchDateTimePeriodExtractorConfiguration(this));
            DurationExtractor = new BaseDurationExtractor(new FrenchDurationExtractorConfiguration(this));
            SetExtractor = new BaseSetExtractor(new FrenchSetExtractorConfiguration(this));
            HolidayExtractor = new BaseHolidayExtractor(new FrenchHolidayExtractorConfiguration(this));
            TimeZoneExtractor = new BaseTimeZoneExtractor(new FrenchTimeZoneExtractorConfiguration(this));
            DateTimeAltExtractor = new BaseDateTimeAltExtractor(new FrenchDateTimeAltExtractorConfiguration(this));
            IntegerExtractor = Number.French.IntegerExtractor.GetInstance();

            AmbiguityFiltersDict = DefinitionLoader.LoadAmbiguityFilters(DateTimeDefinitions.AmbiguityFiltersDict);
        }

        public IDateExtractor DateExtractor { get; }

        public IDateTimeExtractor TimeExtractor { get; }

        public IDateTimeExtractor DateTimeExtractor { get; }

        public IDateTimeExtractor DatePeriodExtractor { get; }

        public IDateTimeExtractor TimePeriodExtractor { get; }

        public IDateTimeExtractor DateTimePeriodExtractor { get; }

        public IDateTimeExtractor DurationExtractor { get; }

        public IDateTimeExtractor SetExtractor { get; }

        public IDateTimeExtractor HolidayExtractor { get; }

        public IDateTimeZoneExtractor TimeZoneExtractor { get; }

        public IDateTimeListExtractor DateTimeAltExtractor { get; }

        public IExtractor IntegerExtractor { get; }

        public Dictionary<Regex, Regex> AmbiguityFiltersDict { get; }

        Regex IMergedExtractorConfiguration.AfterRegex => AfterRegex;

        Regex IMergedExtractorConfiguration.BeforeRegex => BeforeRegex;

        Regex IMergedExtractorConfiguration.SinceRegex => SinceRegex;

        Regex IMergedExtractorConfiguration.AroundRegex => AroundRegex;

        Regex IMergedExtractorConfiguration.EqualRegex => EqualRegex;

        Regex IMergedExtractorConfiguration.FromToRegex => FromToRegex;

        Regex IMergedExtractorConfiguration.SingleAmbiguousMonthRegex => SingleAmbiguousMonthRegex;

        Regex IMergedExtractorConfiguration.PrepositionSuffixRegex => PrepositionSuffixRegex;

        Regex IMergedExtractorConfiguration.AmbiguousRangeModifierPrefix => null;

        Regex IMergedExtractorConfiguration.PotentialAmbiguousRangeRegex => null;

        Regex IMergedExtractorConfiguration.NumberEndingPattern => NumberEndingPattern;

        Regex IMergedExtractorConfiguration.SuffixAfterRegex => SuffixAfterRegex;

        Regex IMergedExtractorConfiguration.UnspecificDatePeriodRegex => UnspecificDatePeriodRegex;

        Regex IMergedExtractorConfiguration.UnspecificTimePeriodRegex => null;

        public Regex FailFastRegex { get; } = null;

        IEnumerable<Regex> IMergedExtractorConfiguration.TermFilterRegexes => TermFilterRegexes;

        StringMatcher IMergedExtractorConfiguration.SuperfluousWordMatcher => SuperfluousWordMatcher;

        bool IMergedExtractorConfiguration.CheckBothBeforeAfter => DateTimeDefinitions.CheckBothBeforeAfter;
    }
}
