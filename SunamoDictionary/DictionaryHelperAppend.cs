namespace SunamoDictionary;

/// <summary>
/// Helper methods for working with dictionaries - Append operations.
/// Provides methods for appending to StringBuilder values in dictionaries.
/// </summary>
public partial class DictionaryHelper
{
    /// <summary>
    /// Appends a line to a StringBuilder in the dictionary, creating it if it doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="text">The text to append.</param>
    public static void AppendLineOrCreate<T>(Dictionary<T, StringBuilder> dictionary, T key, string text)
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
