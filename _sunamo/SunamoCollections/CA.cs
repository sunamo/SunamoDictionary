namespace SunamoDictionary._sunamo.SunamoCollections;

internal class CA
{
    internal static bool IsListStringWrappedInArray<T>(List<T> v2)
    {
        var first = v2.First().ToString();
        if (v2.Count == 1 && (first == "System.Collections.Generic.List`1[System.String]" ||
                              first == "System.Collections.Generic.List`1[System.Object]")) return true;
        return false;
    }
    internal static void InitFillWith(List<string> datas, int pocet, string initWith = Consts.stringEmpty)
    {
        InitFillWith<string>(datas, pocet, initWith);
    }
    internal static void InitFillWith<T>(List<T> datas, int pocet, T initWith)
    {
        for (int i = 0; i < pocet; i++)
        {
            datas.Add(initWith);
        }
    }
    internal static void InitFillWith<T>(List<T> arr, int columns)
    {
        for (int i = 0; i < columns; i++)
        {
            arr.Add(default);
        }
    }

    internal static List<string> PostfixIfNotEnding(string pre, List<string> l)
    {
        for (int i = 0; i < l.Count; i++)
        {
            l[i] = pre + l[i];
        }
        return l;
    }


    internal static List<string> Prepend(string v, List<string> toReplace)
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