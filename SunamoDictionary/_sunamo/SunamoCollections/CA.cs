namespace SunamoDictionary._sunamo.SunamoCollections;

internal class CA
{

    internal static List<string> PostfixIfNotEnding(string prefix, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = prefix + list[i];
        }
        return list;
    }


    internal static List<string> Prepend(string prefix, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].StartsWith(prefix))
            {
                list[i] = prefix + list[i];
            }
        }
        return list;
    }
}