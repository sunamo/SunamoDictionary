namespace SunamoDictionary;

// Helper methods for working with dictionaries - Part 1.
// Provides utility methods for creating dictionaries from strings and lists, and performing transformations.
public partial class DictionaryHelper
{
    // Creates a dictionary from a string by splitting it with delimiters.
    /// <summary>
    /// Get dictionary by key value in string.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="delimiters">The delimiters.</param>
    /// <returns>The get dictionary by key value in string result.</returns>
    public static Dictionary<string, string> GetDictionaryByKeyValueInString(string text, params string[] delimiters)
    {
        var parts = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList();
        return GetDictionaryByKeyValueInString(parts);
    }

    // Swaps keys and values in a dictionary.
    /// <summary>
    /// Switch key and value t u.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The switch key and value t u result.</returns>
    public static Dictionary<U, T> SwitchKeyAndValue<T, U>(Dictionary<T, U> dictionary)
        where T : notnull
        where U : notnull
    {
        var result = new Dictionary<U, T>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add(item.Value, item.Key);
        return result;
    }

    // Changes the type of dictionary keys from int to the specified type.
    /// <summary>
    /// Change type of key t key t 1.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The change type of key t key t 1 result.</returns>
    public static Dictionary<TKey, T1> ChangeTypeOfKey<TKey, T1>(Dictionary<int, T1> dictionary)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, T1>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add((TKey)(dynamic)item.Key, item.Value);
        return result;
    }

    // Changes the type of dictionary keys from short to the specified type.
    /// <summary>
    /// Change type of key t key t 1.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The change type of key t key t 1 result.</returns>
    public static Dictionary<TKey, T1> ChangeTypeOfKey<TKey, T1>(Dictionary<short, T1> dictionary)
        where TKey : notnull
    {
        var result = new Dictionary<TKey, T1>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add((TKey)(dynamic)item.Key, item.Value);
        return result;
    }

    // Creates a dictionary from a list where alternating elements become key-value pairs.
    /// <summary>
    /// Get dictionary by key value in string t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="list">The list.</param>
    /// <returns>The get dictionary by key value in string t result.</returns>
    public static Dictionary<T, T> GetDictionaryByKeyValueInString<T>(List<T> list)
        where T : notnull
    {
        ThrowEx.HasOddNumberOfElements("list", list);
        var result = new Dictionary<T, T>();
        for (var i = 0; i < list.Count; i++)
            result.Add(list[i], list[++i]);
        return result;
    }

    // Creates a dictionary from two lists of equal length.
    /// <summary>
    /// Get dictionary from two list t 1 t 2.
    /// </summary>
    /// <param name="firstList">The first list.</param>
    /// <param name="secondList">The second list.</param>
    /// <param name="isAddingRandomWhenKeyExists">The is adding random when key exists.</param>
    /// <returns>The get dictionary from two list t 1 t 2 result.</returns>
    public static Dictionary<T1, T2> GetDictionaryFromTwoList<T1, T2>(List<T1> firstList, List<T2> secondList, bool isAddingRandomWhenKeyExists = false)
        where T1 : notnull
    {
        ThrowEx.DifferentCountInLists("firstList", firstList.Count, "secondList", secondList.Count);
        var list = new List<KeyValuePair<T1, T2>>();
        for (var i = 0; i < firstList.Count; i++)
            list.Add(new KeyValuePair<T1, T2>(firstList[i], secondList[i]));
        return GetDictionaryFromIList(list, isAddingRandomWhenKeyExists);
    }

    // Gets values for a key if it exists, otherwise returns empty list.
    /// <summary>
    /// Get values or empty t u.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <returns>The get values or empty t u result.</returns>
    public static List<U> GetValuesOrEmpty<T, U>(IDictionary<T, List<U>> dictionary, T key)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
            return dictionary[key];
        return new List<U>();
    }

    // Gets the value for a key, or the key itself converted to string if not found.
    /// <summary>
    /// Get or key t.
    /// </summary>
    /// <typeparam name="T">The type T.</typeparam>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <returns>The get or key t result.</returns>
    public static string GetOrKey<T>(Dictionary<T, string> dictionary, T key)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
            return dictionary[key];
        return key.ToString()!;
    }

    // Divides a dictionary into chunks of specified size.
    /// <summary>
    /// Divide after key value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="chunkSize">The chunk size.</param>
    /// <returns>The divide after key value result.</returns>
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

    // Creates a shallow copy of a dictionary.
    /// <summary>
    /// Clone dictionary t 1 t 2.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The clone dictionary t 1 t 2 result.</returns>
    public static Dictionary<T1, T2> CloneDictionary<T1, T2>(Dictionary<T1, T2> dictionary)
        where T1 : notnull
    {
        return dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
    }

    // Converts a dictionary to a flat list alternating between keys and values.
    /// <summary>
    /// Get list string from dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The get list string from dictionary result.</returns>
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

    // Extracts values from an ordered dictionary of DateTime to int entries.
    /// <summary>
    /// Get list string from dictionary date time int.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The get list string from dictionary date time int result.</returns>
    public static List<string> GetListStringFromDictionaryDateTimeInt(IOrderedEnumerable<KeyValuePair<DateTime, int>> dictionary)
    {
        var result = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            result.Add(item.Value.ToString());
        return result;
    }

    // Extracts values from an ordered dictionary of int to int entries.
    /// <summary>
    /// Get list string from dictionary int int.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The get list string from dictionary int int result.</returns>
    public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> dictionary)
    {
        var result = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            result.Add(item.Value.ToString());
        return result;
    }

    // Converts an ordered enumerable of key-value pairs to a dictionary.
    /// <summary>
    /// Get dictionary from i ordered enumerable t 1 t 2.
    /// </summary>
    /// <param name="orderedEnumerable">The ordered enumerable.</param>
    /// <returns>The get dictionary from i ordered enumerable t 1 t 2 result.</returns>
    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
        where T1 : notnull
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    // Converts a list of key-value pairs to a dictionary.
    /// <summary>
    /// Get dictionary from i list t 1 t 2.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="isAddingRandomWhenKeyExists">The is adding random when key exists.</param>
    /// <returns>The get dictionary from i list t 1 t 2 result.</returns>
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

    // Adds a new key-value pair or sets the value if the key already exists.
    /// <summary>
    /// Add or set t 1 t 2.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    /// <returns>The add or set t 1 t 2 result.</returns>
    public static void AddOrSet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, T2 value)
        where T1 : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = value;
        else
            dictionary.Add(key, value);
    }

    // Copies elements from a dictionary starting at a specified index.
    /// <summary>
    /// Copy to t u.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="arrayIndex">The array index.</param>
    /// <returns>The copy to t u result.</returns>
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

    // Copies elements from a list of key-value pairs starting at a specified index.
    /// <summary>
    /// Copy to t u.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="arrayIndex">The array index.</param>
    /// <returns>The copy to t u result.</returns>
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
