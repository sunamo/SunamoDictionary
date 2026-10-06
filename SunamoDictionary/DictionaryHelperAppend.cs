namespace SunamoDictionary;

// Helper methods for working with dictionaries - Append operations.
// Provides methods for appending to StringBuilder values in dictionaries.
public partial class DictionaryHelper
{
    // Appends a line to a StringBuilder in the dictionary, creating it if it doesn't exist.
    public static void AppendLineOrCreate<T>(Dictionary<T, StringBuilder> dictionary, T key, string text)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].AppendLine(text);
        }
        else
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(text);
            dictionary.Add(key, stringBuilder);
        }
    }
}
