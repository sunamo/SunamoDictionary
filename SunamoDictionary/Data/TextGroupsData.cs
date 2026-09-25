namespace SunamoDictionary.Data;

public class TextGroupsData
{
    public List<string> Entries { get; set; } = new();

    public List<string> Categories { get; set; } = new();

    public Dictionary<int, List<string>> SortedValues { get; set; } = new();

    public static Dictionary<string, List<string>> SortedValuesWithKeyString(TextGroupsData data)
    {
        var result = new Dictionary<string, List<string>>();

        foreach (var item in data.SortedValues)
        {
            result.Add(data.Categories[item.Key], item.Value);
        }

        var reversed = result.Reverse().ToList();
        return DictionaryHelper.GetDictionaryFromIList(reversed);
    }
}