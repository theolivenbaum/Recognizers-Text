﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Japanese;

namespace Microsoft.Recognizers.Text.Number.Japanese
{
    public class JapaneseNumberParserConfiguration : BaseNumberParserConfiguration, ICJKNumberParserConfiguration
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public JapaneseNumberParserConfiguration(INumberOptionsConfiguration config)
        {

            this.Config = config;
            this.LanguageMarker = NumbersDefinitions.LangMarker;
            this.CultureInfo = new CultureInfo(config.Culture);

            this.IsCompoundNumberLanguage = NumbersDefinitions.CompoundNumberLanguage;
            this.IsMultiDecimalSeparatorCulture = NumbersDefinitions.MultiDecimalSeparatorCulture;

            this.DecimalSeparatorChar = NumbersDefinitions.DecimalSeparatorChar;
            this.FractionMarkerToken = NumbersDefinitions.FractionMarkerToken;
            this.NonDecimalSeparatorChar = NumbersDefinitions.NonDecimalSeparatorChar;
            this.HalfADozenText = NumbersDefinitions.HalfADozenText;
            this.WordSeparatorToken = NumbersDefinitions.WordSeparatorToken;
            this.ZeroChar = NumbersDefinitions.ZeroChar;
            this.PairChar = NumbersDefinitions.PairChar;

            this.WrittenDecimalSeparatorTexts = Enumerable.Empty<string>();
            this.WrittenGroupSeparatorTexts = Enumerable.Empty<string>();
            this.WrittenIntegerSeparatorTexts = Enumerable.Empty<string>();
            this.WrittenFractionSeparatorTexts = Enumerable.Empty<string>();

            this.CardinalNumberMap = new Dictionary<string, long>().ToImmutableDictionary();
            this.OrdinalNumberMap = new Dictionary<string, long>().ToImmutableDictionary();
            this.RelativeReferenceOffsetMap = NumbersDefinitions.RelativeReferenceOffsetMap.ToImmutableDictionary();
            this.RelativeReferenceRelativeToMap = NumbersDefinitions.RelativeReferenceRelativeToMap.ToImmutableDictionary();
            this.RoundNumberMap = NumbersDefinitions.RoundNumberMap.ToImmutableDictionary();
            this.ZeroToNineMap = NumbersDefinitions.ZeroToNineMap.ToImmutableDictionary();
            this.FullToHalfMap = NumbersDefinitions.FullToHalfMap.ToImmutableDictionary();
            this.RoundNumberMapChar = NumbersDefinitions.RoundNumberMapChar.ToImmutableDictionary();
            this.UnitMap = NumbersDefinitions.UnitMap.ToImmutableDictionary();
            this.RoundDirectList = NumbersDefinitions.RoundDirectList.ToImmutableList();
            this.TenChars = NumbersDefinitions.TenChars.ToImmutableList();

            this.HalfADozenRegex = null;

            // @TODO Change init to follow design in other languages
            this.DigitalNumberRegex = RegexCache.Get(NumbersDefinitions.DigitalNumberRegex, RegexFlags);
            this.DozenRegex = RegexCache.Get(NumbersDefinitions.DozenRegex, RegexFlags);
            this.PointRegex = RegexCache.Get(NumbersDefinitions.PointRegex, RegexFlags);
            this.DigitNumRegex = RegexCache.Get(NumbersDefinitions.DigitNumRegex, RegexFlags);
            this.DoubleAndRoundRegex = RegexCache.Get(NumbersDefinitions.DoubleAndRoundRegex, RegexFlags);
            this.FracSplitRegex = RegexCache.Get(NumbersDefinitions.FracSplitRegex, RegexFlags);
            this.NegativeNumberSignRegex = RegexCache.Get(NumbersDefinitions.NegativeNumberSignRegex, RegexFlags);
            this.SpeGetNumberRegex = RegexCache.Get(NumbersDefinitions.SpeGetNumberRegex, RegexFlags);
            this.PercentageRegex = RegexCache.Get(NumbersDefinitions.PercentageRegex, RegexFlags);
            this.PairRegex = RegexCache.Get(NumbersDefinitions.PairRegex, RegexFlags);
            this.RoundNumberIntegerRegex = RegexCache.Get(NumbersDefinitions.RoundNumberIntegerRegex, RegexFlags);
            this.PercentageNumRegex = null;
        }

        public string NonDecimalSeparatorText { get; private set; }

        public Regex DigitNumRegex { get; private set; }

        public Regex DozenRegex { get; private set; }

        public Regex PercentageRegex { get; private set; }

        public Regex PercentageNumRegex { get; private set; }

        public Regex DoubleAndRoundRegex { get; private set; }

        public Regex FracSplitRegex { get; private set; }

        public Regex PointRegex { get; private set; }

        public Regex SpeGetNumberRegex { get; private set; }

        public Regex PairRegex { get; private set; }

        public Regex RoundNumberIntegerRegex { get; private set; }

        public char ZeroChar { get; private set; }

        public char PairChar { get; private set; }

        public ImmutableDictionary<char, double> ZeroToNineMap { get; private set; }

        public ImmutableDictionary<char, long> RoundNumberMapChar { get; private set; }

        public ImmutableDictionary<char, char> FullToHalfMap { get; private set; }

        public ImmutableDictionary<string, string> UnitMap { get; private set; }

        public ImmutableDictionary<char, char> TratoSimMap { get; private set; }

        public ImmutableList<char> RoundDirectList { get; private set; }

        public ImmutableList<char> TenChars { get; private set; }

        public override IEnumerable<string> NormalizeTokenSet(IEnumerable<string> tokens, ParseResult context)
        {
            return tokens;
        }

        public override long ResolveCompositeNumber(string numberStr)
        {
            return 0;
        }
    }
}