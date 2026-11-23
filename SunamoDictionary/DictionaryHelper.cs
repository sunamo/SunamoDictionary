namespace SunamoDictionary;

// Helper methods for working with dictionaries.
// Provides utility methods for adding, creating, aggregating and manipulating dictionary entries.
/// <summary>
/// Represents the dictionary helper type.
/// </summary>
public partial class DictionaryHelper
{
    // Adds or creates a dictionary entry with duplicate checking.
    // In addition to method AddOrCreate, this method checks whether value in collection does not already exist.
    /// <summary>
    /// Add or create if dont exists key value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>The add or create if dont exists key value result.</returns>
    public static void AddOrCreateIfDontExists<Key, Value>(Dictionary<Key, List<Value>> dictionary, Key key, Value value) where Key : notnull
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
    /// Calculate median average float.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="textOutputGenerator">The text output generator.</param>
    /// <returns>The calculate median average float result.</returns>
    public static string CalculateMedianAverageFloat(Dictionary<string, List<float>> dictionary, object textOutputGenerator)
    {
        throw new Exception("Deps methods, MedianAverage<T> etc.");
    }

    // Keeps only specified keys in the dictionary, removing all others.
    /// <summary>
    /// Keep only keys.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="includeAlways">The include always.</param>
    /// <returns>The keep only keys result.</returns>
    public static Dictionary<string, string> KeepOnlyKeys(Dictionary<string, string> dictionary, List<string> includeAlways)
    {
        foreach (var item in dictionary.Keys.ToList())
            if (!includeAlways.Contains(item))
                dictionary.Remove(item);
        return dictionary;
    }

    // Parses a list into categories and their entries.
    // Lines ending with ':' become category names, following lines become entries for that category.
    /// <summary>
    /// Category parser.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="isRemovingWhichHaveNoEntries">The is removing which have no entries.</param>
    /// <returns>The category parser result.</returns>
    public static Dictionary<string, List<string>> CategoryParser(List<string> list, bool isRemovingWhichHaveNoEntries)
    {
        var result = new Dictionary<string, List<string>>();
        List<string>? currentEntries = null;
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
                currentEntries!.Add(item);
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

    // Counts occurrences of each item in the list.
    /// <summary>
    /// Count of items t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="items">The items.</param>
    /// <returns>The count of items t result.</returns>
    public static List<KeyValuePair<T, int>> CountOfItems<T>(List<T> items) where T : notnull
    {
        var pairs = new Dictionary<T, int>();
        foreach (var item in items)
            AddOrPlus(pairs, item, 1);
        var orderedPairs = pairs.OrderByDescending(pair => pair.Value);
        return orderedPairs.ToList();
    }

    /// <summary>
    /// Create tree.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The create tree result.</returns>
    public static object CreateTree(Dictionary<string, List<string>> dictionary)
    {
        throw new Exception("Code without NTreeDictionary");
    }

    // Removes a key from the dictionary if it exists.
    /// <summary>
    /// Remove if exists t u.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <returns>The remove if exists t u result.</returns>
    public static void RemoveIfExists<T, U>(Dictionary<T, List<U>> dictionary, T key) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary.Remove(key);
    }

    // Gets values for a key if it exists, optionally adding prefix and suffix.
    /// <summary>
    /// Get if exists.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="prefix">The prefix.</param>
    /// <param name="key">The key.</param>
    /// <param name="isAddingPrefixAndSuffix">The is adding prefix and suffix.</param>
    /// <returns>The get if exists result.</returns>
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

    // Groups dictionary entries by their values, swapping keys and values.
    /// <summary>
    /// Group by values u t.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The group by values u t result.</returns>
    public static Dictionary<T, List<U>> GroupByValues<U, T>(Dictionary<U, T> dictionary) where U : notnull where T : notnull
    {
        var result = new Dictionary<T, List<U>>();
        foreach (var item in dictionary)
            AddOrCreate<T, U>(result, item.Value, item.Key);
        return result;
    }

    // Aggregates all values from the dictionary into a single list.
    /// <summary>
    /// Aggregate values t 2.
    /// </summary>
    /// <typeparam name="T2">The type T2.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The aggregate values t 2 result.</returns>
    public static List<T2> AggregateValues<T2>(Dictionary<T2, List<T2>> dictionary) where T2 : notnull
    {
        var result = new List<T2>();
        foreach (var entry in dictionary)
            result.AddRange(entry.Value);
        return result;
    }

    // Creates a copy of the dictionary.
    /// <summary>
    /// Returns copy t u.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The returns copy t u result.</returns>
    public static Dictionary<T, U> ReturnsCopy<T, U>(Dictionary<T, U> dictionary) where T : notnull
    {
        var result = new Dictionary<T, U>();
        foreach (var item in dictionary)
            result.Add(item.Key, item.Value);
        return result;
    }

    // Removes duplicate entries from dictionary by comparing values.
    // Entries with duplicate values are removed and optionally stored in the duplicates dictionary.
    /// <summary>
    /// Remove duplicated from dictionary by values t 1 t 2.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="duplicates">The duplicates.</param>
    /// <returns>The remove duplicated from dictionary by values t 1 t 2 result.</returns>
    public static Dictionary<T1, T2> RemoveDuplicatedFromDictionaryByValues<T1, T2>(Dictionary<T1, T2> dictionary, Dictionary<T1, T2>? duplicates) where T1 : notnull
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

    // Counts total number of values across all dictionary entries.
    /// <summary>
    /// Count all values key value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The count all values key value result.</returns>
    public static int CountAllValues<Key, Value>(Dictionary<Key, List<Value>> dictionary) where Key : notnull
    {
        var totalCount = 0;
        foreach (var item in dictionary)
            totalCount += item.Value.Count();
        return totalCount;
    }

    // Increments the value for a key, or creates it with value 1 if it doesn't exist.
    /// <summary>
    /// Increment or create t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    public static void IncrementOrCreate<T>(Dictionary<T, int> dictionary, T key) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key]++;
        else
            dictionary.Add(key, 1);
    }

    // Gets the value of the first item in the dictionary.
    /// <summary>
    /// Get first item value key value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The get first item value key value result.</returns>
    public static Value? GetFirstItemValue<Key, Value>(Dictionary<Key, Value> dictionary) where Key : notnull
    {
        foreach (var item in dictionary)
            return item.Value;
        return default;
    }

    // Gets the key of the first item in the dictionary.
    /// <summary>
    /// Get first item key key value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The get first item key key value result.</returns>
    public static Key? GetFirstItemKey<Key, Value>(Dictionary<Key, Value> dictionary) where Key : notnull
    {
        foreach (var item in dictionary)
            return item.Key;
        return default;
    }

    // Adds a value to the dictionary at the specified index and returns the incremented index.
    /// <summary>
    /// Add to index and return incremented short t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="index">The index.</param>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="value">The value.</param>
    /// <returns>The add to index and return incremented short t result.</returns>
    public static short AddToIndexAndReturnIncrementedShort<T>(short index, Dictionary<short, T> dictionary, T value)
    {
        dictionary.Add(index, value);
        index++;
        return index;
    }

    // Creates a dictionary from two lists of keys and values.
    /// <summary>
    /// Get dictionary key value.
    /// </summary>
    /// <param name="keys">The keys.</param>
    /// <param name="values">The values.</param>
    /// <returns>The get dictionary key value result.</returns>
    public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values) where Key : notnull
    {
        ThrowEx.DifferentCountInLists("keys", keys.Count, "values", values.Count);
        var result = new Dictionary<Key, Value>();
        for (var i = 0; i < keys.Count; i++)
            result.Add(keys[i], values[i]);
        return result;
    }
}
