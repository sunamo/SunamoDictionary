namespace SunamoDictionary;

/// <summary>
/// Helper methods for working with dictionaries.
/// Provides utility methods for adding, creating, aggregating and manipulating dictionary entries.
/// </summary>
public partial class DictionaryHelper
{
    /// <summary>
    /// Adds or creates a dictionary entry with duplicate checking.
    /// In addition to method AddOrCreate, this method checks whether value in collection does not already exist.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of the values in the list.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The value to add to the list.</param>
    public static void AddOrCreateIfDontExists<Key, Value>(Dictionary<Key, List<Value>> dictionary, Key key, Value value)
    {
        if (dictionary.ContainsKey(key))
        {
            if (!dictionary[key].Contains(value))
                dictionary[key].Add(value);
        }
        else
        {
            var values = new List<Value>();
            values.Add(value);
            dictionary.Add(key, values);
        }
    }

    /// <summary>
    /// Calculates median and average for float values in a dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary containing float lists.</param>
    /// <param name="textOutputGenerator">The text output generator for formatting results.</param>
    /// <returns>A formatted string with median and average values.</returns>
    /// <exception cref="Exception">Thrown when dependencies (MedianAverage methods) are missing.</exception>
    public static string CalculateMedianAverageFloat(Dictionary<string, List<float>> dictionary, object textOutputGenerator)
    {
        throw new Exception("Deps methods, MedianAverage<T> etc.");
    }

    /// <summary>
    /// Keeps only specified keys in the dictionary, removing all others.
    /// </summary>
    /// <param name="dictionary">The dictionary to filter.</param>
    /// <param name="includeAlways">List of keys to keep in the dictionary.</param>
    /// <returns>The filtered dictionary.</returns>
    public static Dictionary<string, string> KeepOnlyKeys(Dictionary<string, string> dictionary, List<string> includeAlways)
    {
        foreach (var item in dictionary.Keys.ToList())
            if (!includeAlways.Contains(item))
                dictionary.Remove(item);
        return dictionary;
    }

    /// <summary>
    /// Parses a list into categories and their entries.
    /// Lines ending with ':' become category names, following lines become entries for that category.
    /// </summary>
    /// <param name="list">The list of strings to parse.</param>
    /// <param name="isRemovingWhichHaveNoEntries">Whether to remove categories with "No entries" text.</param>
    /// <returns>A dictionary mapping category names to their entry lists.</returns>
    public static Dictionary<string, List<string>> CategoryParser(List<string> list, bool isRemovingWhichHaveNoEntries)
    {
        var result = new Dictionary<string, List<string>>();
        List<string> currentEntries = null;
        for (var i = 0; i < list.Count; i++)
        {
            var item = list[i].Trim();
            if (item == string.Empty)
                continue;
            if (item.EndsWith(":"))
            {
                currentEntries = new List<string>();
                result.Add(item.TrimEnd(':'), currentEntries);
            }
            else
            {
                currentEntries.Add(item);
            }
        }

        if (isRemovingWhichHaveNoEntries)
            for (var i = result.Keys.Count - 1; i >= 0; i--)
            {
                var key = result.ElementAt(i).Key;
                if (result[key][0] == "No entries")
                    result.Remove(key);
            }

        return result;
    }

    /// <summary>
    /// Counts occurrences of each item in the list.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    /// <param name="items">The list of items to count.</param>
    /// <returns>A list of key-value pairs sorted by count in descending order.</returns>
    public static List<KeyValuePair<T, int>> CountOfItems<T>(List<T> items)
    {
        var pairs = new Dictionary<T, int>();
        foreach (var item in items)
            AddOrPlus(pairs, item, 1);
        var orderedPairs = pairs.OrderByDescending(dictionary => dictionary.Value);
        var result = orderedPairs.ToList();
        return result;
    }

    /// <summary>
    /// Creates a tree structure from the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to convert to tree.</param>
    /// <returns>A tree structure.</returns>
    /// <exception cref="Exception">Thrown when NTreeDictionary dependency is missing.</exception>
    public static object CreateTree(Dictionary<string, List<string>> dictionary)
    {
        throw new Exception("Code without NTreeDictionary");
    }

    /// <summary>
    /// Removes a key from the dictionary if it exists.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to remove.</param>
    public static void RemoveIfExists<T, U>(Dictionary<T, List<U>> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
            dictionary.Remove(key);
    }

    /// <summary>
    /// Gets values for a key if it exists, optionally adding prefix and suffix.
    /// </summary>
    /// <param name="dictionary">The dictionary to query.</param>
    /// <param name="prefix">The prefix to add to each value.</param>
    /// <param name="key">The key to look up.</param>
    /// <param name="isAddingPrefixAndSuffix">Whether to add prefix and suffix to values.</param>
    /// <returns>The list of values if key exists, otherwise empty list.</returns>
    public static IList<string> GetIfExists(Dictionary<string, List<string>> dictionary, string prefix, string key, bool isAddingPrefixAndSuffix)
    {
        if (dictionary.ContainsKey(key))
        {
            var result = dictionary[key];
            if (isAddingPrefixAndSuffix)
            {
                if (!string.IsNullOrEmpty(key))
                    result = CA.PostfixIfNotEnding(key, result);
                CA.Prepend(prefix, result);
            }

            return result;
        }

        return new List<string>();
    }

    /// <summary>
    /// Groups dictionary entries by their values, swapping keys and values.
    /// </summary>
    /// <typeparam name="U">The original key type.</typeparam>
    /// <typeparam name="T">The original value type.</typeparam>
    /// <param name="dictionary">The dictionary to group.</param>
    /// <returns>A dictionary where original values become keys with lists of original keys as values.</returns>
    public static Dictionary<T, List<U>> GroupByValues<U, T>(Dictionary<U, T> dictionary)
    {
        var result = new Dictionary<T, List<U>>();
        foreach (var item in dictionary)
            AddOrCreate<T, U>(result, item.Value, item.Key);
        return result;
    }

    /// <summary>
    /// Aggregates all values from the dictionary into a single list.
    /// </summary>
    /// <typeparam name="T2">The type of values in the dictionary.</typeparam>
    /// <param name="dictionary">The dictionary containing lists of values.</param>
    /// <returns>A single list containing all values from all entries.</returns>
    public static List<T2> AggregateValues<T2>(Dictionary<T2, List<T2>> dictionary)
    {
        var result = new List<T2>();
        foreach (var entry in dictionary)
            result.AddRange(entry.Value);
        return result;
    }

    /// <summary>
    /// Creates a copy of the dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to copy.</param>
    /// <returns>A new dictionary with the same key-value pairs.</returns>
    public static Dictionary<T, U> ReturnsCopy<T, U>(Dictionary<T, U> dictionary)
    {
        var result = new Dictionary<T, U>();
        foreach (var item in dictionary)
            result.Add(item.Key, item.Value);
        return result;
    }

    /// <summary>
    /// Removes duplicate entries from dictionary by comparing values.
    /// Entries with duplicate values are removed and optionally stored in the duplicates dictionary.
    /// </summary>
    /// <typeparam name="T1">The type of the dictionary key.</typeparam>
    /// <typeparam name="T2">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to process.</param>
    /// <param name="duplicates">Optional dictionary to store removed duplicates (can be null).</param>
    /// <returns>The dictionary with duplicates removed.</returns>
    public static Dictionary<T1, T2> RemoveDuplicatedFromDictionaryByValues<T1, T2>(Dictionary<T1, T2> dictionary, Dictionary<T1, T2> duplicates)
    {
        var processed = new List<T2>();
        foreach (var item in dictionary.Keys.ToList())
        {
            var value = dictionary[item];
            if (processed.Contains(value))
            {
                if (duplicates != null)
                    duplicates.Add(item, value);
                dictionary.Remove(item);
            }
            else
            {
                processed.Add(value);
            }
        }

        return dictionary;
    }

    /// <summary>
    /// Counts total number of values across all dictionary entries.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of the values in the lists.</typeparam>
    /// <param name="dictionary">The dictionary to count.</param>
    /// <returns>The total count of all values in all lists.</returns>
    public static int CountAllValues<Key, Value>(Dictionary<Key, List<Value>> dictionary)
    {
        var totalCount = 0;
        foreach (var item in dictionary)
            totalCount += item.Value.Count();
        return totalCount;
    }

    /// <summary>
    /// Increments the value for a key, or creates it with value 1 if it doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to increment.</param>
    public static void IncrementOrCreate<T>(Dictionary<T, int> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key]++;
        else
            dictionary.Add(key, 1);
    }

    /// <summary>
    /// Gets the value of the first item in the dictionary.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to query.</param>
    /// <returns>The value of the first entry, or default if dictionary is empty.</returns>
    public static Value GetFirstItemValue<Key, Value>(Dictionary<Key, Value> dictionary)
    {
        foreach (var item in dictionary)
            return item.Value;
        return default;
    }

    /// <summary>
    /// Gets the key of the first item in the dictionary.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to query.</param>
    /// <returns>The key of the first entry, or default if dictionary is empty.</returns>
    public static Key GetFirstItemKey<Key, Value>(Dictionary<Key, Value> dictionary)
    {
        foreach (var item in dictionary)
            return item.Key;
        return default;
    }

    /// <summary>
    /// Adds a value to the dictionary at the specified index and returns the incremented index.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary value.</typeparam>
    /// <param name="index">The current index.</param>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="value">The value to add.</param>
    /// <returns>The incremented index.</returns>
    public static short AddToIndexAndReturnIncrementedShort<T>(short index, Dictionary<short, T> dictionary, T value)
    {
        dictionary.Add(index, value);
        index++;
        return index;
    }

    /// <summary>
    /// Creates a dictionary from two lists of keys and values.
    /// </summary>
    /// <typeparam name="Key">The type of the keys.</typeparam>
    /// <typeparam name="Value">The type of the values.</typeparam>
    /// <param name="keys">The list of keys.</param>
    /// <param name="values">The list of values.</param>
    /// <returns>A new dictionary with paired keys and values.</returns>
    /// <exception cref="Exception">Thrown when the lists have different counts.</exception>
    public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values)
    {
        ThrowEx.DifferentCountInLists("keys", keys.Count, "values", values.Count);
        var result = new Dictionary<Key, Value>();
        for (var i = 0; i < keys.Count; i++)
            result.Add(keys[i], values[i]);
        return result;
    }
}
