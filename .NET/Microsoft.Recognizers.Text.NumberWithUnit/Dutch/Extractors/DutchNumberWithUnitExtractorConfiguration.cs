﻿using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.Recognizers.Definitions;
using Microsoft.Recognizers.Definitions.Dutch;
using Microsoft.Recognizers.Definitions.Utilities;
using Microsoft.Recognizers.Text.Number;
using Microsoft.Recognizers.Text.Number.Dutch;

namespace Microsoft.Recognizers.Text.NumberWithUnit.Dutch
{
    public abstract class DutchNumberWithUnitExtractorConfiguration : INumberWithUnitExtractorConfiguration
    {
        private const RegexOptions RegexFlags = RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture;

        private static readonly Regex CompoundUnitConnRegex =
            RegexCache.Get(NumbersWithUnitDefinitions.CompoundUnitConnectorRegex, RegexFlags);

        private static readonly Regex NonUnitsRegex =
            RegexCache.Get(BaseUnits.PmNonUnitRegex, RegexFlags);

        protected DutchNumberWithUnitExtractorConfiguration(CultureInfo ci)
        {
            this.CultureInfo = ci;

            var unitNumConfig = new BaseNumberOptionsConfiguration(ci.Name, NumberOptions.None, NumberMode.Unit);
            this.UnitNumExtractor = NumberExtractor.GetInstance(unitNumConfig);

            this.BuildPrefix = NumbersWithUnitDefinitions.BuildPrefix;
            this.BuildSuffix = NumbersWithUnitDefinitions.BuildSuffix;
            this.ConnectorToken = string.Empty;

            AmbiguityFiltersDict = DefinitionLoader.LoadAmbiguityFilters(NumbersWithUnitDefinitions.AmbiguityFiltersDict);
        }

        public abstract string ExtractType { get; }

        public CultureInfo CultureInfo { get; }

        public IExtractor UnitNumExtractor { get; }

        public string BuildPrefix { get; }

        public string BuildSuffix { get; }

        public string ConnectorToken { get; }

        public Regex CompoundUnitConnectorRegex => CompoundUnitConnRegex;

        public Regex NonUnitRegex => NonUnitsRegex;

        public virtual Regex AmbiguousUnitNumberMultiplierRegex => null;

        public Dictionary<Regex, Regex> AmbiguityFiltersDict { get; } = null;

        public abstract ImmutableDictionary<string, string> SuffixList { get; }

        public abstract ImmutableDictionary<string, string> PrefixList { get; }

        public abstract ImmutableList<string> AmbiguousUnitList { get; }

        public void ExpandHalfSuffix(string source, ref List<ExtractResult> result, IOrderedEnumerable<ExtractResult> numbers)
        {
        }
    }
}