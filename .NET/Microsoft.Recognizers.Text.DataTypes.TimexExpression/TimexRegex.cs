// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Recognizers.Text.DataTypes.TimexExpression
{
    public static class TimexRegex
    {
        private const string DateTimeCollectionName = "datetime";
        private const string DateCollectionName = "date";
        private const string TimeCollectionName = "time";
        private const string PeriodCollectionName = "period";

        private static IDictionary<string, Regex[]> timexRegex = new Dictionary<string, Regex[]>
        {
            {
                DateCollectionName, new Regex[]
                {
                    // date
                    RegexCache.Get(@"^(?<year>\d\d\d\d)-(?<month>\d\d)-(?<dayOfMonth>\d\d)"),
                    RegexCache.Get(@"^XXXX-WXX-(?<dayOfWeek>\d)"),
                    RegexCache.Get(@"^XXXX-(?<month>\d\d)-(?<dayOfMonth>\d\d)"),

                    // daterange
                    RegexCache.Get(@"^(?<year>\d\d\d\d)"),
                    RegexCache.Get(@"^(?<year>\d\d\d\d)-(?<month>\d\d)"),
                    RegexCache.Get(@"^(?<season>SP|SU|FA|WI)"),
                    RegexCache.Get(@"^(?<year>\d\d\d\d)-(?<season>SP|SU|FA|WI)"),
                    RegexCache.Get(@"^(?<year>\d\d\d\d)-W(?<weekOfYear>\d\d)"),
                    RegexCache.Get(@"^(?<year>\d\d\d\d)-W(?<weekOfYear>\d\d)-(?<weekend>WE)"),
                    RegexCache.Get(@"^XXXX-(?<month>\d\d)"),
                    RegexCache.Get(@"^XXXX-(?<month>\d\d)-W(?<weekOfMonth>\d\d)"),
                    RegexCache.Get(@"^XXXX-(?<month>\d\d)-WXX-(?<weekOfMonth>\d{1,2})"),
                    RegexCache.Get(@"^XXXX-(?<month>\d\d)-WXX-(?<weekOfMonth>\d)-(?<dayOfWeek>\d)"),
                }
            },
            {
                TimeCollectionName, new Regex[]
                {
                    // time
                    RegexCache.Get(@"T(?<hour>\d\d)Z?$"),
                    RegexCache.Get(@"T(?<hour>\d\d):(?<minute>\d\d)Z?$"),
                    RegexCache.Get(@"T(?<hour>\d\d):(?<minute>\d\d):(?<second>\d\d)Z?$"),

                    // timerange
                    RegexCache.Get(@"^T(?<partOfDay>DT|NI|MO|AF|EV)$"),
                }
            },
            {
                PeriodCollectionName, new Regex[]
                {
                    RegexCache.Get(@"^P(?<amount>\d*\.?\d+)(?<dateUnit>Y|M|W|D)$"),
                    RegexCache.Get(@"^PT(?<amount>\d*\.?\d+)(?<timeUnit>H|M|S)$"),
                }
            },
        };

        public static bool Extract(string name, string timex, IDictionary<string, string> result)
        {
            var lowerName = name.ToLower();
            var nameGroup = new string[lowerName == DateTimeCollectionName ? 2 : 1];

            if (lowerName == DateTimeCollectionName)
            {
                nameGroup[0] = DateCollectionName;
                nameGroup[1] = TimeCollectionName;
            }
            else
            {
                nameGroup[0] = lowerName;
            }

            var anyTrue = false;
            foreach (var nameItem in nameGroup)
            {
                foreach (var entry in timexRegex[nameItem])
                {
                    if (TryExtract(entry, timex, result))
                    {
                        anyTrue = true;
                    }
                }
            }

            return anyTrue;
        }

        private static bool TryExtract(Regex regex, string timex, IDictionary<string, string> result)
        {
            var regexResult = regex.Match(timex);
            if (!regexResult.Success)
            {
                return false;
            }

            foreach (var groupName in regex.GetGroupNames())
            {
                result[groupName] = regexResult.Groups[groupName].Value;
            }

            return true;
        }
    }
}
