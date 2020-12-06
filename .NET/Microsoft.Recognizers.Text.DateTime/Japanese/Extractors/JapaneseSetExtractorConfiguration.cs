﻿using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Japanese;
using DateObject = System.DateTime;

namespace Microsoft.Recognizers.Text.DateTime.Japanese
{
    public class JapaneseSetExtractorConfiguration : IDateTimeExtractor
    {
        public static readonly string ExtractorName = Constants.SYS_DATETIME_SET;

        public static readonly Regex EachUnitRegex = RegexCache.Get(DateTimeDefinitions.SetEachUnitRegex, RegexFlags);

        public static readonly Regex EachPrefixRegex = RegexCache.Get(DateTimeDefinitions.SetEachPrefixRegex, RegexFlags);

        public static readonly Regex LastRegex = RegexCache.Get(DateTimeDefinitions.SetLastRegex, RegexFlags);

        public static readonly Regex EachDayRegex = RegexCache.Get(DateTimeDefinitions.SetEachDayRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly JapaneseDurationExtractorConfiguration DurationExtractor = new JapaneseDurationExtractorConfiguration();

        private static readonly JapaneseTimeExtractorConfiguration TimeExtractor = new JapaneseTimeExtractorConfiguration();

        private static readonly JapaneseDateExtractorConfiguration DateExtractor = new JapaneseDateExtractorConfiguration();

        private static readonly JapaneseDateTimeExtractorConfiguration DateTimeExtractor = new JapaneseDateTimeExtractorConfiguration();

        public static List<Token> MatchEachDuration(string text, DateObject referenceTime)
        {
            var ret = new List<Token>();

            var ers = DurationExtractor.Extract(text, referenceTime);
            foreach (var er in ers)
            {
                // "each last summer" doesn't make sense
                if (LastRegex.IsMatch(er.Text))
                {
                    continue;
                }

                var beforeStr = text.Substring(0, er.Start ?? 0);
                var match = EachPrefixRegex.Match(beforeStr);
                if (match.Success)
                {
                    ret.Add(new Token(match.Index, er.Start + er.Length ?? 0));
                }
            }

            return ret;
        }

        public static List<Token> MatchEachUnit(string text)
        {
            var ret = new List<Token>();

            // handle "each month"
            var matches = EachUnitRegex.Matches(text);
            foreach (Match match in matches)
            {
                ret.Add(new Token(match.Index, match.Index + match.Length));
            }

            return ret;
        }

        public static List<Token> TimeEveryday(string text, DateObject referenceTime)
        {
            var ret = new List<Token>();
            var ers = TimeExtractor.Extract(text, referenceTime);
            foreach (var er in ers)
            {
                var beforeStr = text.Substring(0, er.Start ?? 0);
                var match = EachDayRegex.Match(beforeStr);
                if (match.Success)
                {
                    ret.Add(new Token(match.Index, match.Index + match.Length + (er.Length ?? 0)));
                }
            }

            return ret;
        }

        public static List<Token> MatchEachDate(string text, DateObject referenceTime)
        {
            var ret = new List<Token>();
            var ers = DateExtractor.Extract(text, referenceTime);
            foreach (var er in ers)
            {
                var beforeStr = text.Substring(0, er.Start ?? 0);
                var match = EachPrefixRegex.Match(beforeStr);
                if (match.Success)
                {
                    ret.Add(new Token(match.Index, match.Index + match.Length + (er.Length ?? 0)));
                }
            }

            return ret;
        }

        public static List<Token> MatchEachDateTime(string text, DateObject referenceTime)
        {
            var ret = new List<Token>();
            var ers = DateTimeExtractor.Extract(text, referenceTime);
            foreach (var er in ers)
            {
                var beforeStr = text.Substring(0, er.Start ?? 0);
                var match = EachPrefixRegex.Match(beforeStr);
                if (match.Success)
                {
                    ret.Add(new Token(match.Index, match.Index + match.Length + (er.Length ?? 0)));
                }
            }

            return ret;
        }

        public List<ExtractResult> Extract(string text)
        {
            return Extract(text, DateObject.Now);
        }

        public List<ExtractResult> Extract(string text, DateObject referenceTime)
        {
            var tokens = new List<Token>();
            tokens.AddRange(MatchEachUnit(text));
            tokens.AddRange(MatchEachDuration(text, referenceTime));
            tokens.AddRange(TimeEveryday(text, referenceTime));
            tokens.AddRange(MatchEachDate(text, referenceTime));
            tokens.AddRange(MatchEachDateTime(text, referenceTime));

            return Token.MergeAllTokens(tokens, text, ExtractorName);
        }
    }
}