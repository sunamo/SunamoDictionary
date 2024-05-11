using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunamoDictionary;
internal partial class DictionaryHelper
{
    public static void AppendLineOrCreate<T>(Dictionary<T, StringBuilder> sb, T n, string item)
    {
        if (sb.ContainsKey(n))
        {
            sb[n].AppendLine(item);
        }
        else
        {
            var sb2 = new StringBuilder();
            sb2.AppendLine(item);
            sb.Add(n, sb2);
        }
    }
}
