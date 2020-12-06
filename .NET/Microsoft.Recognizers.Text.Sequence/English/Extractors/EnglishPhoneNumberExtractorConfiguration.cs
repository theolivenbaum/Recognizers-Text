﻿using System.Text.RegularExpressions;
using Microsoft.Recognizers.Definitions.English;

namespace Microsoft.Recognizers.Text.Sequence.English
{
    public class EnglishPhoneNumberExtractorConfiguration : BasePhoneNumberExtractorConfiguration
    {
        public EnglishPhoneNumberExtractorConfiguration(SequenceOptions options)
            : base(options)
        {
            FalsePositivePrefixRegex = RegexCache.Get(PhoneNumbersDefinitions.FalsePositivePrefixRegex);
        }
    }
}
