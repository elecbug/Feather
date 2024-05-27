using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Feather.Extesions
{
    /// <summary>
    /// 문자열 확장
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// 두 문자열이 와일드 카드로 일치하는지 확인
        /// </summary>
        /// <param name="source"> 확인할 베이스 문자열 </param>
        /// <param name="like"> 와일드 카드가 적용된 문자열 </param>
        /// <returns></returns>
        public static bool ContainsLike(this string source, string like)
        {
            return Regex.IsMatch(source,
              "^" + Regex.Escape(like).Replace("_", ".").Replace("%", ".*") + "$");
        }
    }
}
