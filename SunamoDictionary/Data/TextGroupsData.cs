namespace SunamoDictionary.Data;

/// <summary>
/// Data structure for managing categorized text groups.
/// Stores entries, categories, and provides sorting functionality.
/// </summary>
public class TextGroupsData
{
    /// <summary>
    /// Gets or sets the list of text entries.
    /// </summary>
    public List<string> Entries { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the list of category names.
    /// </summary>
    public List<string> Categories { get; set; } = new List<string>();

    /// <summary>
    /// Gets or sets the dictionary of sorted values indexed by category.
    /// </summary>
    public Dictionary<int, List<string>> SortedValues { get; set; } = new Dictionary<int, List<string>>();

    /// <summary>
    /// Converts sorted values with integer keys to string keys using category names.
    /// </summary>
    /// <param name="data">The text groups data to convert.</param>
    /// <returns>A dictionary with category names as keys and their corresponding values.</returns>
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