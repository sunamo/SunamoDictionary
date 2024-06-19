using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class CA
{
    public static List<string> PostfixIfNotEnding(string pre, List<string> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = pre + l[i];
        }
        return l;
    }


    public static List<string> Prepend(string v, List<string> toReplace)
    {
        for (int i = 0; i < toReplace.Count; i++)
        {
            if (!toReplace[i].StartsWith(v))
            {
                toReplace[i] = v + toReplace[i];
            }
        }
        return toReplace;
    }
}

