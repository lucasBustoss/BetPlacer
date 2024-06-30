using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetPlacer.Core.Helpers.Database
{
    public static class SnakeCaseNamingConvention
    {
        public static string ToSnakeCase(this string input)
        {
            return string.Concat(input.Select((x, i) =>
                i > 0 && char.IsUpper(x) ? "_" + x.ToString() : x.ToString())).ToLower();
        }
    }
}
