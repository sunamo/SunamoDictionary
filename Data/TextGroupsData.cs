namespace SunamoDictionary;

public class TextGroupsData
{
    public List<string> entries = new List<string>();
    public List<string> categories = new List<string>();
    public Dictionary<int, List<string>> sortedValues = new Dictionary<int, List<string>>();

    public static Dictionary<string, List<string>> SortedValuesWithKeyString(TextGroupsData d)
    {
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

        foreach (var item in d.sortedValues)
        {
            result.Add(d.categories[item.Key], item.Value);
        }

        var reversed = result.Reverse().ToList();
        return DictionaryHelper.GetDictionaryFromIList(reversed);
    }
}
