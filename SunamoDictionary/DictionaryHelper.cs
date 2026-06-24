namespace SunamoDictionary;

// Helper methods for working with dictionaries.
// Provides utility methods for adding, creating, aggregating and manipulating dictionary entries.
public partial class DictionaryHelper
{
    // Adds or creates a dictionary entry with duplicate checking.
    // In addition to method AddOrCreate, this method checks whether value in collection does not already exist.
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

    public static string CalculateMedianAverageFloat(Dictionary<string, List<float>> dictionary, object textOutputGenerator)
    {
        throw new Exception("Deps methods, MedianAverage<T> etc.");
    }

    // Keeps only specified keys in the dictionary, removing all others.
    public static Dictionary<string, string> KeepOnlyKeys(Dictionary<string, string> dictionary, List<string> includeAlways)
    {
        foreach (var item in dictionary.Keys.ToList())
            if (!includeAlways.Contains(item))
                dictionary.Remove(item);
        return dictionary;
    }

    // Parses a list into categories and their entries.
    // Lines ending with ':' become category names, following lines become entries for that category.
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
    public static List<KeyValuePair<T, int>> CountOfItems<T>(List<T> items) where T : notnull
    {
        var pairs = new Dictionary<T, int>();
        foreach (var item in items)
            AddOrPlus(pairs, item, 1);
        var orderedPairs = pairs.OrderByDescending(pair => pair.Value);
        return orderedPairs.ToList();
    }

    public static object CreateTree(Dictionary<string, List<string>> dictionary)
    {
        throw new Exception("Code without NTreeDictionary");
    }

    // Removes a key from the dictionary if it exists.
    public static void RemoveIfExists<T, U>(Dictionary<T, List<U>> dictionary, T key) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary.Remove(key);
    }

    // Gets values for a key if it exists, optionally adding prefix and suffix.
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
    public static Dictionary<T, List<U>> GroupByValues<U, T>(Dictionary<U, T> dictionary) where U : notnull where T : notnull
    {
        var result = new Dictionary<T, List<U>>();
        foreach (var item in dictionary)
            AddOrCreate<T, U>(result, item.Value, item.Key);
        return result;
    }

    // Aggregates all values from the dictionary into a single list.
    public static List<T2> AggregateValues<T2>(Dictionary<T2, List<T2>> dictionary) where T2 : notnull
    {
        var result = new List<T2>();
        foreach (var entry in dictionary)
            result.AddRange(entry.Value);
        return result;
    }

    // Creates a copy of the dictionary.
    public static Dictionary<T, U> ReturnsCopy<T, U>(Dictionary<T, U> dictionary) where T : notnull
    {
        var result = new Dictionary<T, U>();
        foreach (var item in dictionary)
            result.Add(item.Key, item.Value);
        return result;
    }

    // Removes duplicate entries from dictionary by comparing values.
    // Entries with duplicate values are removed and optionally stored in the duplicates dictionary.
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
    public static int CountAllValues<Key, Value>(Dictionary<Key, List<Value>> dictionary) where Key : notnull
    {
        var totalCount = 0;
        foreach (var item in dictionary)
            totalCount += item.Value.Count();
        return totalCount;
    }

    // Increments the value for a key, or creates it with value 1 if it doesn't exist.
    public static void IncrementOrCreate<T>(Dictionary<T, int> dictionary, T key) where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key]++;
        else
            dictionary.Add(key, 1);
    }

    // Gets the value of the first item in the dictionary.
    public static Value? GetFirstItemValue<Key, Value>(Dictionary<Key, Value> dictionary) where Key : notnull
    {
        foreach (var item in dictionary)
            return item.Value;
        return default;
    }

    // Gets the key of the first item in the dictionary.
    public static Key? GetFirstItemKey<Key, Value>(Dictionary<Key, Value> dictionary) where Key : notnull
    {
        foreach (var item in dictionary)
            return item.Key;
        return default;
    }

    // Adds a value to the dictionary at the specified index and returns the incremented index.
    public static short AddToIndexAndReturnIncrementedShort<T>(short index, Dictionary<short, T> dictionary, T value)
    {
        dictionary.Add(index, value);
        index++;
        return index;
    }

    // Creates a dictionary from two lists of keys and values.
    public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values) where Key : notnull
    {
        ThrowEx.DifferentCountInLists("keys", keys.Count, "values", values.Count);
        var result = new Dictionary<Key, Value>();
        for (var i = 0; i < keys.Count; i++)
            result.Add(keys[i], values[i]);
        return result;
    }
}
