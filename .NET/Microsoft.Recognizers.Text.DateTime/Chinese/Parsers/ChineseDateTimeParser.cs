﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Chinese;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.Number.Chinese;
using Microsoft.Recognizers.Text.Utilities;

using DateObject = System.DateTime;

namespace Microsoft.Recognizers.Text.DateTime.Chinese
{
    public class ChineseDateTimeParser : IDateTimeParser
    {
        public static readonly string ParserName = Constants.SYS_DATETIME_DATETIME;

        public static readonly Regex SimpleAmRegex = RegexCache.Get(DateTimeDefinitions.DateTimeSimpleAmRegex, RegexFlags);

        public static readonly Regex SimplePmRegex = RegexCache.Get(DateTimeDefinitions.DateTimeSimplePmRegex, RegexFlags);

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly IDateTimeExtractor SingleDateExtractor = new ChineseDateExtractorConfiguration();

        private static readonly IDateTimeExtractor SingleTimeExtractor = new ChineseTimeExtractorConfiguration();

        private readonly IDateTimeExtractor durationExtractor = new ChineseDurationExtractorConfiguration();

        private readonly IExtractor integerExtractor;

        private readonly IParser numberParser;

        private readonly IFullDateTimeParserConfiguration config;

        public ChineseDateTimeParser(IFullDateTimeParserConfiguration configuration)
        {
            config = configuration;

            var numOptions = NumberOptions.None;
            if ((config.Options & DateTimeOptions.NoProtoCache) != 0)
            {
                numOptions = NumberOptions.NoProtoCache;
            }

            var numConfig = new BaseNumberOptionsConfiguration(config.Culture, numOptions);

            integerExtractor = new IntegerExtractor(numConfig);
            numberParser = new BaseCJKNumberParser(new ChineseNumberParserConfiguration(numConfig));
        }

        public ParseResult Parse(ExtractResult extResult)
        {
            return this.Parse(extResult, DateObject.Now);
        }

        public DateTimeParseResult Parse(ExtractResult er, DateObject refDate)
        {
            var referenceTime = refDate;

            object value = null;
            if (er.Type.Equals(ParserName, StringComparison.Ordinal))
            {
                var innerResult = MergeDateAndTime(er.Text, referenceTime);
                if (!innerResult.Success)
                {
                    innerResult = ParseBasicRegex(er.Text, referenceTime);
                }

                if (!innerResult.Success)
                {
                    innerResult = ParseTimeOfToday(er.Text, referenceTime);
                }

                if (!innerResult.Success)
                {
                    innerResult = ParserDurationWithAgoAndLater(er.Text, referenceTime);
                }

                if (innerResult.Success)
                {
                    innerResult.FutureResolution = new Dictionary<string, string>
                    {
                        { TimeTypeConstants.DATETIME, DateTimeFormatUtil.FormatDateTime((DateObject)innerResult.FutureValue) },
                    };

                    innerResult.PastResolution = new Dictionary<string, string>
                    {
                        { TimeTypeConstants.DATETIME, DateTimeFormatUtil.FormatDateTime((DateObject)innerResult.PastValue) },
                    };

                    innerResult.IsLunar = IsLunarCalendar(er.Text);

                    value = innerResult;
                }
            }

            var ret = new DateTimeParseResult
            {
                Text = er.Text,
                Start = er.Start,
                Length = er.Length,
                Type = er.Type,
                Data = er.Data,
                Value = value,
                TimexStr = value == null ? string.Empty : ((DateTimeResolutionResult)value).Timex,
                ResolutionStr = string.Empty,
            };
            return ret;
        }

        public List<DateTimeParseResult> FilterResults(string query, List<DateTimeParseResult> candidateResults)
        {
            return candidateResults;
        }

        private static DateTimeResolutionResult ParseBasicRegex(string text, DateObject referenceTime)
        {
            var ret = new DateTimeResolutionResult();
            var trimmedText = text.Trim();

            // handle "现在"
            var match = ChineseDateTimeExtractorConfiguration.NowRegex.MatchExact(trimmedText, trim: true);

            if (match.Success)
            {

                // @TODO move hardcoded values to resource files
                if (trimmedText.EndsWith("现在", StringComparison.Ordinal))
                {
                    ret.Timex = "PRESENT_REF";
                }
                else if (trimmedText.Equals("刚刚才", StringComparison.Ordinal) ||
                         trimmedText.Equals("刚刚", StringComparison.Ordinal) ||
                         trimmedText.Equals("刚才", StringComparison.Ordinal))
                {
                    ret.Timex = "PAST_REF";
                }
                else if (trimmedText.Equals("立刻", StringComparison.Ordinal) ||
                         trimmedText.Equals("马上", StringComparison.Ordinal))
                {
                    ret.Timex = "FUTURE_REF";
                }

                ret.FutureValue = ret.PastValue = referenceTime;
                ret.Success = true;

                return ret;
            }

            return ret;
        }

        // parse if lunar contains
        private bool IsLunarCalendar(string text)
        {
            var trimmedText = text.Trim();
            var match = ChineseDateExtractorConfiguration.LunarRegex.Match(trimmedText);
            if (match.Success)
            {
                return true;
            }

            return ChineseHolidayExtractorConfiguration.LunarHolidayRegex.IsMatch(trimmedText);
        }

        // merge a Date entity and a Time entity
        private DateTimeResolutionResult MergeDateAndTime(string text, DateObject referenceTime)
        {
            var ret = new DateTimeResolutionResult();

            var er1 = SingleDateExtractor.Extract(text, referenceTime);
            if (er1.Count == 0)
            {
                return ret;
            }

            var er2 = SingleTimeExtractor.Extract(text, referenceTime);
            if (er2.Count == 0)
            {
                return ret;
            }

            // TODO: Add reference time
            var pr1 = this.config.DateParser.Parse(er1[0], referenceTime.Date);
            var pr2 = this.config.TimeParser.Parse(er2[0], referenceTime);

            if (pr1.Value == null || pr2.Value == null)
            {
                return ret;
            }

            var futureDate = (DateObject)((DateTimeResolutionResult)pr1.Value).FutureValue;
            var pastDate = (DateObject)((DateTimeResolutionResult)pr1.Value).PastValue;
            var time = (DateObject)((DateTimeResolutionResult)pr2.Value).FutureValue;

            var hour = time.Hour;
            var min = time.Minute;
            var sec = time.Second;

            // handle morning, afternoon
            if (SimplePmRegex.IsMatch(text) && hour < Constants.HalfDayHourCount)
            {
                hour += Constants.HalfDayHourCount;
            }
            else if (SimpleAmRegex.IsMatch(text) && hour >= Constants.HalfDayHourCount)
            {
                hour -= Constants.HalfDayHourCount;
            }

            var timeStr = pr2.TimexStr;
            if (timeStr.EndsWith(Constants.Comment_AmPm, StringComparison.Ordinal))
            {
                timeStr = timeStr.Substring(0, timeStr.Length - 4);
            }

            timeStr = "T" + hour.ToString("D2", CultureInfo.InvariantCulture) + timeStr.Substring(3);
            ret.Timex = pr1.TimexStr + timeStr;

            var val = (DateTimeResolutionResult)pr2.Value;

            if (hour <= Constants.HalfDayHourCount && !SimplePmRegex.IsMatch(text) && !SimpleAmRegex.IsMatch(text) &&
                !string.IsNullOrEmpty(val.Comment))
            {
                // ret.Timex += "ampm";
                ret.Comment = Constants.Comment_AmPm;
            }

            ret.FutureValue = DateObject.MinValue.SafeCreateFromValue(futureDate.Year, futureDate.Month, futureDate.Day, hour, min, sec);
            ret.PastValue = DateObject.MinValue.SafeCreateFromValue(pastDate.Year, pastDate.Month, pastDate.Day, hour, min, sec);
            ret.Success = true;

            return ret;
        }

        private DateTimeResolutionResult ParseTimeOfToday(string text, DateObject referenceTime)
        {
            var ret = new DateTimeResolutionResult();
            var ers = SingleTimeExtractor.Extract(text, referenceTime);
            if (ers.Count != 1)
            {
                return ret;
            }

            // TODO: Add reference time
            var pr = this.config.TimeParser.Parse(ers[0], referenceTime);
            if (pr.Value == null)
            {
                return ret;
            }

            var time = (DateObject)((DateTimeResolutionResult)pr.Value).FutureValue;

            var hour = time.Hour;
            var min = time.Minute;
            var sec = time.Second;

            var match = ChineseDateTimeExtractorConfiguration.TimeOfTodayRegex.Match(text);

            if (match.Success)
            {
                var matchStr = match.Value;

                var swift = 0;

                // @TODO move hardcoded values to resources file

                switch (matchStr)
                {
                    case "今晚":
                        if (hour < Constants.HalfDayHourCount)
                        {
                            hour += Constants.HalfDayHourCount;
                        }

                        break;
                    case "今早":
                    case "今晨":
                        if (hour >= Constants.HalfDayHourCount)
                        {
                            hour -= Constants.HalfDayHourCount;
                        }

                        break;
                    case "明晚":
                        swift = 1;
                        if (hour < Constants.HalfDayHourCount)
                        {
                            hour += Constants.HalfDayHourCount;
                        }

                        break;
                    case "明早":
                    case "明晨":
                        swift = 1;
                        if (hour >= Constants.HalfDayHourCount)
                        {
                            hour -= Constants.HalfDayHourCount;
                        }

                        break;
                    case "昨晚":
                        swift = -1;
                        if (hour < Constants.HalfDayHourCount)
                        {
                            hour += Constants.HalfDayHourCount;
                        }

                        break;
                    default:
                        break;
                }

                var date = referenceTime.AddDays(swift).Date;

                // in this situation, luisStr cannot end up with "ampm", because we always have a "morning" or "night"
                var timeStr = pr.TimexStr;
                if (timeStr.EndsWith(Constants.Comment_AmPm, StringComparison.Ordinal))
                {
                    timeStr = timeStr.Substring(0, timeStr.Length - 4);
                }

                timeStr = "T" + hour.ToString("D2", CultureInfo.InvariantCulture) + timeStr.Substring(3);

                ret.Timex = DateTimeFormatUtil.FormatDate(date) + timeStr;
                ret.FutureValue = ret.PastValue = DateObject.MinValue.SafeCreateFromValue(date.Year, date.Month, date.Day, hour, min, sec);
                ret.Success = true;
                return ret;
            }

            return ret;
        }

        // handle cases like "5分钟前", "1小时以后"
        private DateTimeResolutionResult ParserDurationWithAgoAndLater(string text, DateObject referenceDate)
        {
            var ret = new DateTimeResolutionResult();
            var durationRes = durationExtractor.Extract(text, referenceDate);

            if (durationRes.Count > 0)
            {
                var match = ChineseDateTimeExtractorConfiguration.DateTimePeriodUnitRegex.Match(text);
                if (match.Success)
                {
                    var suffix = text.Substring((int)durationRes[0].Start + (int)durationRes[0].Length).Trim();
                    var srcUnit = match.Groups["unit"].Value;

                    var numberStr = text.Substring((int)durationRes[0].Start, match.Index - (int)durationRes[0].Start).Trim();
                    var number = ConvertChineseToNum(numberStr);

                    if (this.config.UnitMap.ContainsKey(srcUnit))
                    {
                        var unitStr = this.config.UnitMap[srcUnit];

                        var beforeMatch = ChineseDateTimeExtractorConfiguration.BeforeRegex.Match(suffix);
                        if (beforeMatch.Success && suffix.StartsWith(beforeMatch.Value, StringComparison.InvariantCulture))
                        {
                            DateObject date;
                            switch (unitStr)
                            {
                                case Constants.TimexHour:
                                    date = referenceDate.AddHours(-number);
                                    break;
                                case Constants.TimexMinute:
                                    date = referenceDate.AddMinutes(-number);
                                    break;
                                case Constants.TimexSecond:
                                    date = referenceDate.AddSeconds(-number);
                                    break;
                                default:
                                    return ret;
                            }

                            ret.Timex = $"{DateTimeFormatUtil.LuisDate(date)}";
                            ret.FutureValue = ret.PastValue = date;
                            ret.Success = true;
                            return ret;
                        }

                        var afterMatch = ChineseDateTimeExtractorConfiguration.AfterRegex.Match(suffix);
                        if (afterMatch.Success && suffix.StartsWith(afterMatch.Value, StringComparison.Ordinal))
                        {
                            DateObject date;
                            switch (unitStr)
                            {
                                case Constants.TimexHour:
                                    date = referenceDate.AddHours(number);
                                    break;
                                case Constants.TimexMinute:
                                    date = referenceDate.AddMinutes(number);
                                    break;
                                case Constants.TimexSecond:
                                    date = referenceDate.AddSeconds(number);
                                    break;
                                default:
                                    return ret;
                            }

                            ret.Timex = $"{DateTimeFormatUtil.LuisDate(date)}";
                            ret.FutureValue = ret.PastValue = date;
                            ret.Success = true;
                            return ret;
                        }
                    }
                }
            }

            return ret;
        }

        // convert Chinese Number to Integer
        private int ConvertChineseToNum(string numStr)
        {
            var num = -1;
            var er = integerExtractor.Extract(numStr);
            if (er.Count != 0)
            {
                if (er[0].Type.Equals(Number.Constants.SYS_NUM_INTEGER, StringComparison.Ordinal))
                {
                    num = Convert.ToInt32((double)(numberParser.Parse(er[0]).Value ?? 0));
                }
            }

            return num;
        }
    }
}