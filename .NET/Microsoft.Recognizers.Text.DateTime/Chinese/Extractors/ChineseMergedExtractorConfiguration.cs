﻿using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions;
using Microsoft.Recognizers.Definitions.Chinese;
using Microsoft.Recognizers.Definitions.Utilities;
using Microsoft.Recognizers.Text.Utilities;
using DateObject = System.DateTime;

namespace Microsoft.Recognizers.Text.DateTime.Chinese
{
    public class ChineseMergedExtractorConfiguration : IDateTimeExtractor
    {
        public static readonly Regex BeforeRegex = RegexCache.Get(DateTimeDefinitions.ParserConfigurationBefore, RegexFlags);
        public static readonly Regex AfterRegex = RegexCache.Get(DateTimeDefinitions.ParserConfigurationAfter, RegexFlags);
        public static readonly Regex UntilRegex = RegexCache.Get(DateTimeDefinitions.ParserConfigurationUntil, RegexFlags);
        public static readonly Regex SincePrefixRegex = RegexCache.Get(DateTimeDefinitions.ParserConfigurationSincePrefix, RegexFlags);
        public static readonly Regex SinceSuffixRegex = RegexCache.Get(DateTimeDefinitions.ParserConfigurationSinceSuffix, RegexFlags);
        public static readonly Regex EqualRegex = RegexCache.Get(BaseDateTime.EqualRegex, RegexFlags);
        public static readonly Regex PotentialAmbiguousRangeRegex = RegexCache.Get(DateTimeDefinitions.FromToRegex, RegexFlags);
        public static readonly Regex AmbiguousRangeModifierPrefix = RegexCache.Get(DateTimeDefinitions.AmbiguousRangeModifierPrefix, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly ChineseDateExtractorConfiguration DateExtractor = new ChineseDateExtractorConfiguration();
        private static readonly ChineseTimeExtractorConfiguration TimeExtractor = new ChineseTimeExtractorConfiguration();
        private static readonly ChineseDateTimeExtractorConfiguration DateTimeExtractor = new ChineseDateTimeExtractorConfiguration();
        private static readonly ChineseTimePeriodExtractorChsConfiguration TimePeriodExtractor = new ChineseTimePeriodExtractorChsConfiguration();
        private static readonly ChineseDurationExtractorConfiguration DurationExtractor = new ChineseDurationExtractorConfiguration();
        private static readonly ChineseSetExtractorConfiguration SetExtractor = new ChineseSetExtractorConfiguration();

        private readonly ChineseDateTimePeriodExtractorConfiguration dateTimePeriodExtractor;
        private readonly ChineseDatePeriodExtractorConfiguration datePeriodExtractor;

        private readonly IDateTimeOptionsConfiguration config;

        public ChineseMergedExtractorConfiguration(IDateTimeOptionsConfiguration config)
        {
            this.config = config;

            AmbiguityFiltersDict = DefinitionLoader.LoadAmbiguityFilters(DateTimeDefinitions.AmbiguityFiltersDict);
            HolidayExtractor = new BaseHolidayExtractor(new ChineseHolidayExtractorConfiguration(config));

            dateTimePeriodExtractor = new ChineseDateTimePeriodExtractorConfiguration(config);
            datePeriodExtractor = new ChineseDatePeriodExtractorConfiguration(config);
        }

        public Dictionary<Regex, Regex> AmbiguityFiltersDict { get; }

        private BaseHolidayExtractor HolidayExtractor { get; }

        public List<ExtractResult> Extract(string text)
        {
            return Extract(text, DateObject.Now);
        }

        public List<ExtractResult> Extract(string text, DateObject referenceTime)
        {
            var ret = DateExtractor.Extract(text, referenceTime);

            // the order is important, since there is a problem in merging
            AddTo(ret, TimeExtractor.Extract(text, referenceTime));
            AddTo(ret, DurationExtractor.Extract(text, referenceTime));
            AddTo(ret, datePeriodExtractor.Extract(text, referenceTime));
            AddTo(ret, DateTimeExtractor.Extract(text, referenceTime));
            AddTo(ret, TimePeriodExtractor.Extract(text, referenceTime));
            AddTo(ret, dateTimePeriodExtractor.Extract(text, referenceTime));
            AddTo(ret, SetExtractor.Extract(text, referenceTime));
            AddTo(ret, HolidayExtractor.Extract(text, referenceTime));

            ret = FilterAmbiguity(ret, text);

            AddMod(ret, text);

            ret = ret.OrderBy(p => p.Start).ToList();

            return ret;
        }

        private static List<ExtractResult> MoveOverlap(List<ExtractResult> dst, ExtractResult result)
        {
            var duplicate = new List<int>();
            for (var i = 0; i < dst.Count; ++i)
            {
                if (result.Text.Contains(dst[i].Text) &&
                    (result.Start == dst[i].Start || result.Start + result.Length == dst[i].Start + dst[i].Length))
                {
                    duplicate.Add(i);
                }
            }

            var tempDst = dst.Where((_, i) => !duplicate.Contains(i)).ToList();

            return tempDst;
        }

        // Filter some bad cases like "十二周岁" and "12号", etc.
        private List<ExtractResult> FilterAmbiguity(List<ExtractResult> extractResults, string text)
        {
            if (this.AmbiguityFiltersDict != null)
            {
                foreach (var regex in this.AmbiguityFiltersDict)
                {
                    foreach (var extractResult in extractResults)
                    {
                        if (regex.Key.IsMatch(extractResult.Text))
                        {
                            var matches = regex.Value.Matches(text).Cast<Match>();
                            extractResults = extractResults.Where(er => !matches.Any(m => m.Index < er.Start + er.Length && m.Index + m.Length > er.Start))
                                .ToList();
                        }
                    }
                }
            }

            return extractResults;
        }

        private void AddMod(List<ExtractResult> ers, string text)
        {
            var lastEnd = 0;
            foreach (var er in ers)
            {
                var beforeStr = text.Substring(lastEnd, er.Start ?? 0);
                var afterStr = text.Substring((er.Start ?? 0) + (er.Length ?? 0));

                var match = BeforeRegex.MatchBegin(afterStr, trim: true);

                if (match.Success)
                {
                    var modLength = match.Index + match.Length;
                    er.Length += modLength;
                    er.Text = text.Substring(er.Start ?? 0, er.Length ?? 0);

                    er.Metadata = AssignModMetadata(er.Metadata);
                }

                match = AfterRegex.MatchBegin(afterStr, trim: true);

                if (match.Success)
                {
                    var modLength = match.Index + match.Length;
                    er.Length += modLength;
                    er.Text = text.Substring(er.Start ?? 0, er.Length ?? 0);

                    er.Metadata = AssignModMetadata(er.Metadata);
                }

                match = UntilRegex.MatchEnd(beforeStr, trim: true);

                if (match.Success)
                {
                    var modLength = beforeStr.Length - match.Index;
                    er.Length += modLength;
                    er.Start -= modLength;
                    er.Text = text.Substring(er.Start ?? 0, er.Length ?? 0);

                    er.Metadata = AssignModMetadata(er.Metadata);
                }

                match = SincePrefixRegex.MatchEnd(beforeStr, trim: true);

                if (match.Success && AmbiguousRangeChecker(beforeStr, text, er))
                {
                    var modLength = beforeStr.Length - match.Index;
                    er.Length += modLength;
                    er.Start -= modLength;
                    er.Text = text.Substring(er.Start ?? 0, er.Length ?? 0);

                    er.Metadata = AssignModMetadata(er.Metadata);
                }

                match = SinceSuffixRegex.MatchBegin(afterStr, trim: true);
                if (match.Success)
                {
                    var modLength = match.Index + match.Length;
                    er.Length += modLength;
                    er.Text = text.Substring(er.Start ?? 0, er.Length ?? 0);

                    er.Metadata = AssignModMetadata(er.Metadata);
                }

                match = EqualRegex.MatchBegin(beforeStr, trim: true);
                if (match.Success)
                {
                    var modLength = beforeStr.Length - match.Index;
                    er.Length += modLength;
                    er.Start -= modLength;
                    er.Text = text.Substring(er.Start ?? 0, er.Length ?? 0);

                    er.Metadata = AssignModMetadata(er.Metadata);
                }
            }
        }

        private void AddTo(List<ExtractResult> dst, List<ExtractResult> src)
        {
            foreach (var result in src)
            {
                var isFound = false;
                int indexRm = -1, lengthRm = 1;
                for (var i = 0; i < dst.Count; i++)
                {
                    if (dst[i].IsOverlap(result))
                    {
                        isFound = true;
                        if (result.Length > dst[i].Length)
                        {
                            indexRm = i;
                            var j = i + 1;
                            while (j < dst.Count && dst[j].IsOverlap(result))
                            {
                                lengthRm++;
                                j++;
                            }
                        }

                        break;
                    }
                }

                if (!isFound)
                {
                    dst.Add(result);
                }
                else if (indexRm >= 0)
                {
                    dst.RemoveRange(indexRm, lengthRm);
                    var tmpDst = MoveOverlap(dst, result);
                    dst.Clear();
                    dst.AddRange(tmpDst);
                    dst.Insert(indexRm, result);
                }
            }
        }

        // Avoid adding mod for ambiguity cases, such as "从" in "从 ... 到 ..." should not add mod
        // TODO: Revise PotentialAmbiguousRangeRegex to support cases like "从2015年起，哪所大学需要的分数在80到90之间"
        private bool AmbiguousRangeChecker(string beforeStr, string text, ExtractResult er)
        {
            if (AmbiguousRangeModifierPrefix.MatchEnd(beforeStr, true).Success)
            {
                var matches = PotentialAmbiguousRangeRegex.Matches(text).Cast<Match>();
                if (matches.Any(m => m.Index < er.Start + er.Length && m.Index + m.Length > er.Start))
                {
                    return false;
                }
            }

            return true;
        }

        private Metadata AssignModMetadata(Metadata metadata)
        {
            if (metadata == null)
            {
                metadata = new Metadata { HasMod = true };
            }
            else
            {
                metadata.HasMod = true;
            }

            return metadata;
        }
    }
}