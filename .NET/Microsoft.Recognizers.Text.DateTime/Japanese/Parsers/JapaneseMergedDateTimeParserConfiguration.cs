﻿using System;
using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.Japanese;
using DateObject = System.DateTime;

namespace Microsoft.Recognizers.Text.DateTime.Japanese
{
    public class JapaneseMergedDateTimeParserConfiguration : BaseMergedDateTimeParser
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex BeforeRegex = new Regex(DateTimeDefinitions.MergedBeforeRegex, RegexFlags);

        private static readonly Regex AfterRegex = new Regex(DateTimeDefinitions.MergedAfterRegex, RegexFlags);

        // TODO implement SinceRegex
        private static readonly Regex SinceRegex = new Regex(DateTimeDefinitions.MergedAfterRegex, RegexFlags);

        public JapaneseMergedDateTimeParserConfiguration(IMergedParserConfiguration configuration)
            : base(configuration)
        {
        }

        public new ParseResult Parse(ExtractResult er)
        {
            return Parse(er, DateObject.Now);
        }

        public new ParseResult Parse(ExtractResult er, DateObject refTime)
        {
            var referenceTime = refTime;
            DateTimeParseResult pr;

            // push, save teh MOD string
            bool hasBefore = false, hasAfter = false, hasSince = false;
            if (BeforeRegex.IsMatch(er.Text))
            {
                hasBefore = true;
            }
            else if (AfterRegex.IsMatch(er.Text))
            {
                hasAfter = true;
            }
            else if (SinceRegex.IsMatch(er.Text))
            {
                hasSince = true;
            }

            if (er.Type.Equals(Constants.SYS_DATETIME_DATE, StringComparison.Ordinal))
            {
                pr = this.Config.DateParser.Parse(er, referenceTime);
            }
            else if (er.Type.Equals(Constants.SYS_DATETIME_TIME, StringComparison.Ordinal))
            {
                pr = this.Config.TimeParser.Parse(er, referenceTime);
            }
            else if (er.Type.Equals(Constants.SYS_DATETIME_DATETIME, StringComparison.Ordinal))
            {
                pr = this.Config.DateTimeParser.Parse(er, referenceTime);
            }
            else if (er.Type.Equals(Constants.SYS_DATETIME_DATEPERIOD, StringComparison.Ordinal))
            {
                pr = this.Config.DatePeriodParser.Parse(er, referenceTime);
            }
            else if (er.Type.Equals(Constants.SYS_DATETIME_TIMEPERIOD, StringComparison.Ordinal))
            {
                pr = this.Config.TimePeriodParser.Parse(er, referenceTime);
            }
            else if (er.Type.Equals(Constants.SYS_DATETIME_DATETIMEPERIOD, StringComparison.Ordinal))
            {
                pr = this.Config.DateTimePeriodParser.Parse(er, referenceTime);
            }
            else if (er.Type.Equals(Constants.SYS_DATETIME_DURATION, StringComparison.Ordinal))
            {
                pr = this.Config.DurationParser.Parse(er, referenceTime);
            }
            else if (er.Type.Equals(Constants.SYS_DATETIME_SET, StringComparison.Ordinal))
            {
                pr = this.Config.SetParser.Parse(er, referenceTime);
            }
            else
            {
                return null;
            }

            // pop, restore the MOD string
            if (hasBefore)
            {
                var val = (DateTimeResolutionResult)pr.Value;
                if (val != null)
                {
                    val.Mod = Constants.BEFORE_MOD;
                }

                pr.Value = val;
            }

            if (hasAfter)
            {
                var val = (DateTimeResolutionResult)pr.Value;
                if (val != null)
                {
                    val.Mod = Constants.AFTER_MOD;
                }

                pr.Value = val;
            }

            if (hasSince)
            {
                var val = (DateTimeResolutionResult)pr.Value;
                if (val != null)
                {
                    val.Mod = Constants.SINCE_MOD;
                }

                pr.Value = val;
            }

            pr.Value = DateTimeResolution(pr);

            var hasModifier = hasBefore || hasAfter || hasSince;

            // change the type at last for the after or before mode
            pr.Type = $"{ParserTypeName}.{DetermineDateTimeType(er.Type, hasModifier)}";

            return pr;
        }
    }
}