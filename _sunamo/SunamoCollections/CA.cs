namespace SunamoDictionary._sunamo.SunamoCollections;


internal class CA
{
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
