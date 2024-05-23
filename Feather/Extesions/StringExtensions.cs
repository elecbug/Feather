using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Feather.Extesions
{
    public static class StringExtensions
    {
        public static bool ContainsLike(this string source, string like)
        {
            return Regex.IsMatch(source,
              "^" + Regex.Escape(like).Replace("_", ".").Replace("%", ".*") + "$");
        }
    }
}
