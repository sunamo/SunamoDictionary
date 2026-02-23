namespace SunamoDictionary;

/// <summary>
/// Helper methods for working with dictionaries - Part 1.
/// Provides utility methods for creating dictionaries from strings and lists, and performing transformations.
/// </summary>
public partial class DictionaryHelper
{
    /// <summary>
    /// Creates a dictionary from a string by splitting it with delimiters.
    /// </summary>
    /// <param name="text">The text to parse into key-value pairs.</param>
    /// <param name="delimiters">The delimiters to split the text.</param>
    /// <returns>A dictionary created from alternating key-value pairs in the split text.</returns>
    public static Dictionary<string, string> GetDictionaryByKeyValueInString(string text, params string[] delimiters)
    {
        var parts = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
        return GetDictionaryByKeyValueInString(parts);
    }

    /// <summary>
    /// Swaps keys and values in a dictionary.
    /// </summary>
    /// <typeparam name="T">The original key type.</typeparam>
    /// <typeparam name="U">The original value type.</typeparam>
    /// <param name="dictionary">The dictionary to transform.</param>
    /// <returns>A new dictionary with keys and values swapped.</returns>
    public static Dictionary<U, T> SwitchKeyAndValue<T, U>(Dictionary<T, U> dictionary)
        where T : notnull
        where U : notnull
    {
        var result = new Dictionary<U, T>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add(item.Value, item.Key);
        return result;
    }

    /// <summary>
    /// Changes the type of dictionary keys from int to the specified type.
    /// </summary>
    /// <typeparam name="TKey">The target key type.</typeparam>
    /// <typeparam name="T1">The value type.</typeparam>
    /// <param name="dictionary">The dictionary with int keys.</param>
    /// <returns>A new dictionary with converted key types.</returns>
    public static Dictionary<TKey, T1> ChangeTypeOfKey<TKey, T1>(Dictionary<int, T1> dictionary)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, T1>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add((TKey)(dynamic)item.Key, item.Value);
        return result;
    }

    /// <summary>
    /// Changes the type of dictionary keys from short to the specified type.
    /// </summary>
    /// <typeparam name="TKey">The target key type.</typeparam>
    /// <typeparam name="T1">The value type.</typeparam>
    /// <param name="dictionary">The dictionary with short keys.</param>
    /// <returns>A new dictionary with converted key types.</returns>
    public static Dictionary<TKey, T1> ChangeTypeOfKey<TKey, T1>(Dictionary<short, T1> dictionary)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, T1>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add((TKey)(dynamic)item.Key, item.Value);
        return result;
    }

    /// <summary>
    /// Creates a dictionary from a list where alternating elements become key-value pairs.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="list">The list to convert (must have even number of elements).</param>
    /// <returns>A dictionary created from alternating key-value pairs.</returns>
    /// <exception cref="Exception">Thrown when the list has odd number of elements.</exception>
    public static Dictionary<T, T> GetDictionaryByKeyValueInString<T>(List<T> list)
        where T : notnull
    {
        ThrowEx.HasOddNumberOfElements("list", list);
        var result = new Dictionary<T, T>();
        for (var i = 0; i < list.Count; i++)
            result.Add(list[i], list[++i]);
        return result;
    }

    /// <summary>
    /// Creates a dictionary from two lists of equal length.
    /// </summary>
    /// <typeparam name="T1">The type of keys.</typeparam>
    /// <typeparam name="T2">The type of values.</typeparam>
    /// <param name="firstList">The list of keys.</param>
    /// <param name="secondList">The list of values.</param>
    /// <param name="isAddingRandomWhenKeyExists">Whether to add random suffix when duplicate keys are found.</param>
    /// <returns>A dictionary mapping first list elements to second list elements.</returns>
    /// <exception cref="Exception">Thrown when the lists have different counts.</exception>
    public static Dictionary<T1, T2> GetDictionaryFromTwoList<T1, T2>(List<T1> firstList, List<T2> secondList, bool isAddingRandomWhenKeyExists = false)
        where T1 : notnull
    {
        ThrowEx.DifferentCountInLists("firstList", firstList.Count, "secondList", secondList.Count);
        var list = new List<KeyValuePair<T1, T2>>();
        for (var i = 0; i < firstList.Count; i++)
            list.Add(new KeyValuePair<T1, T2>(firstList[i], secondList[i]));
        return GetDictionaryFromIList(list, isAddingRandomWhenKeyExists);
    }

    /// <summary>
    /// Gets values for a key if it exists, otherwise returns empty list.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of values in the list.</typeparam>
    /// <param name="dictionary">The dictionary to query.</param>
    /// <param name="key">The key to look up.</param>
    /// <returns>The list of values if key exists, otherwise empty list.</returns>
    public static List<U> GetValuesOrEmpty<T, U>(IDictionary<T, List<U>> dictionary, T key)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
            return dictionary[key];
        return new List<U>();
    }

    /// <summary>
    /// Gets the value for a key, or the key itself converted to string if not found.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <param name="dictionary">The dictionary to query.</param>
    /// <param name="key">The key to look up.</param>
    /// <returns>The value if key exists, otherwise the key as string.</returns>
    public static string GetOrKey<T>(Dictionary<T, string> dictionary, T key)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
            return dictionary[key];
        return key.ToString()!;
    }

    /// <summary>
    /// Divides a dictionary into chunks of specified size.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to divide.</param>
    /// <param name="chunkSize">The maximum number of entries per chunk.</param>
    /// <returns>A list of dictionaries, each containing up to chunkSize entries.</returns>
    public static List<Dictionary<Key, Value>> DivideAfter<Key, Value>(Dictionary<Key, Value> dictionary, int chunkSize)
        where Key : notnull
    {
        var result = new List<Dictionary<Key, Value>>();
        var currentDictionary = new Dictionary<Key, Value>();
        foreach (var item in dictionary)
        {
            currentDictionary.Add(item.Key, item.Value);
            if (currentDictionary.Count == chunkSize)
            {
                result.Add(currentDictionary);
                currentDictionary = new Dictionary<Key, Value>();
            }
        }

        if (currentDictionary.Count > 0)
            result.Add(currentDictionary);
        return result;
    }

    /// <summary>
    /// Creates a shallow copy of a dictionary.
    /// </summary>
    /// <typeparam name="T1">The type of the dictionary key.</typeparam>
    /// <typeparam name="T2">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to clone.</param>
    /// <returns>A new dictionary with the same key-value pairs.</returns>
    public static Dictionary<T1, T2> CloneDictionary<T1, T2>(Dictionary<T1, T2> dictionary)
        where T1 : notnull
    {
        var newDictionary = dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        return newDictionary;
    }

    /// <summary>
    /// Converts a dictionary to a flat list alternating between keys and values.
    /// </summary>
    /// <param name="dictionary">The dictionary to convert.</param>
    /// <returns>A list with alternating key-value entries.</returns>
    public static List<string> GetListStringFromDictionary(Dictionary<string, string> dictionary)
    {
        var result = new List<string>();
        foreach (var item in dictionary)
        {
            result.Add(item.Key);
            result.Add(item.Value);
        }

        return result;
    }

    /// <summary>
    /// Extracts values from an ordered dictionary of DateTime to int entries.
    /// </summary>
    /// <param name="dictionary">The ordered dictionary to process.</param>
    /// <returns>A list of string representations of the values.</returns>
    public static List<string> GetListStringFromDictionaryDateTimeInt(IOrderedEnumerable<KeyValuePair<DateTime, int>> dictionary)
    {
        var result = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            result.Add(item.Value.ToString());
        return result;
    }

    /// <summary>
    /// Extracts values from an ordered dictionary of int to int entries.
    /// </summary>
    /// <param name="dictionary">The ordered dictionary to process.</param>
    /// <returns>A list of string representations of the values.</returns>
    public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> dictionary)
    {
        var result = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            result.Add(item.Value.ToString());
        return result;
    }

    /// <summary>
    /// Converts an ordered enumerable of key-value pairs to a dictionary.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="orderedEnumerable">The ordered enumerable to convert.</param>
    /// <returns>A dictionary containing the key-value pairs.</returns>
    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
        where T1 : notnull
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    /// <summary>
    /// Converts a list of key-value pairs to a dictionary.
    /// </summary>
    /// <typeparam name="T1">The type of the key.</typeparam>
    /// <typeparam name="T2">The type of the value.</typeparam>
    /// <param name="list">The list of key-value pairs.</param>
    /// <param name="isAddingRandomWhenKeyExists">Whether to add random suffix when duplicate keys are found.</param>
    /// <returns>A dictionary containing the key-value pairs.</returns>
    public static Dictionary<T1, T2> GetDictionaryFromIList<T1, T2>(List<KeyValuePair<T1, T2>> list, bool isAddingRandomWhenKeyExists = false)
        where T1 : notnull
    {
        var dictionary = new Dictionary<T1, T2>();
        foreach (var item in list)
        {
            var key = item.Key;
            var keyExists = dictionary.ContainsKey(item.Key);
            if (keyExists)
                if (isAddingRandomWhenKeyExists)
                {
                    var randomSuffix = key + " " + RandomHelper.RandomString(5);
                    key = (T1)(dynamic)randomSuffix;
                }

            dictionary.Add(key, item.Value);
        }

        return dictionary;
    }

    /// <summary>
    /// Adds a new key-value pair or sets the value if the key already exists.
    /// </summary>
    /// <typeparam name="T1">The type of the dictionary key.</typeparam>
    /// <typeparam name="T2">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The value to set.</param>
    public static void AddOrSet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, T2 value)
        where T1 : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = value;
        else
            dictionary.Add(key, value);
    }

    /// <summary>
    /// Copies elements from a dictionary starting at a specified index.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to copy from.</param>
    /// <param name="arrayIndex">The starting index to copy from.</param>
    public static void CopyTo<T, U>(Dictionary<T, U> dictionary, int arrayIndex)
        where T : notnull
    {
        var array = new KeyValuePair<T, U>[dictionary.Count - arrayIndex + 1];
        var currentIndex = 0;
        var shouldAdd = false;
        foreach (var item in dictionary)
        {
            if (currentIndex == arrayIndex && !shouldAdd)
            {
                shouldAdd = true;
                currentIndex = 0;
            }

            if (shouldAdd)
                array[currentIndex] = new KeyValuePair<T, U>(item.Key, item.Value);
            currentIndex++;
        }
    }

    /// <summary>
    /// Copies elements from a list of key-value pairs starting at a specified index.
    /// </summary>
    /// <typeparam name="T">The type of the key.</typeparam>
    /// <typeparam name="U">The type of the value.</typeparam>
    /// <param name="list">The list to copy from.</param>
    /// <param name="arrayIndex">The starting index to copy from.</param>
    public static void CopyTo<T, U>(List<KeyValuePair<T, U>> list, int arrayIndex)
    {
        var array = new KeyValuePair<T, U>[list.Count - arrayIndex + 1];
        var currentIndex = 0;
        var shouldAdd = false;
        foreach (var item in list)
        {
            if (currentIndex == arrayIndex && !shouldAdd)
            {
                shouldAdd = true;
                currentIndex = 0;
            }

            if (shouldAdd)
                array[currentIndex] = new KeyValuePair<T, U>(item.Key, item.Value);
            currentIndex++;
        }
    }
}