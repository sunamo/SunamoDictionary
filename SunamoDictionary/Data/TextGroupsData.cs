namespace SunamoDictionary.Data;

public class TextGroupsData
{
    public List<string> Entries { get; set; } = new List<string>();
    public List<string> Categories { get; set; } = new List<string>();
    public Dictionary<int, List<string>> SortedValues { get; set; } = new Dictionary<int, List<string>>();

    public static Dictionary<string, List<string>> SortedValuesWithKeyString(TextGroupsData data)
    {
        Dictionary<string, List<string>> result = new Dictionary<string, List<string>>();

        foreach (var item in data.SortedValues)
        {
            result.Add(data.Categories[item.Key], item.Value);
        }

        var reversed = result.Reverse().ToList();
        return DictionaryHelper.GetDictionaryFromIList(reversed);
    }
}