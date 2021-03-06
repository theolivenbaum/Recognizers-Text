﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions.Korean;

namespace Microsoft.Recognizers.Text.Number.Korean
{
    public class OrdinalExtractor : BaseNumberExtractor
    {

        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        public OrdinalExtractor()
        {
            var regexes = new Dictionary<Regex, TypeTag>
            {
                {
                    RegexCache.Get(NumbersDefinitions.OrdinalKoreanRegex, RegexFlags),
                    RegexTagGenerator.GenerateRegexTag(Constants.ORDINAL_PREFIX, Constants.NUMBER_SUFFIX)
                },

            };

            Regexes = regexes.ToImmutableDictionary();
        }

        internal sealed override ImmutableDictionary<Regex, TypeTag> Regexes { get; }

        protected sealed override string ExtractType { get; } = Constants.SYS_NUM_ORDINAL;
    }
}