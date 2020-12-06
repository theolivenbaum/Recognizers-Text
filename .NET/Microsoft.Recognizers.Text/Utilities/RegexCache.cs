using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Microsoft.Recognizers.Text
{
    public static class RegexCache
    {
        private static ConcurrentDictionary<(string pattern, RegexOptions options), Regex> _cache = new ConcurrentDictionary<(string pattern, RegexOptions options), Regex>();

        public static Regex Get(string pattern, RegexOptions options)
        {
            return _cache.GetOrAdd((pattern, options), k => new Regex(k.pattern, k.options));
        }

        public static Regex Get(string pattern)
        {
            return _cache.GetOrAdd((pattern, default), k => new Regex(k.pattern));
        }
    }
}