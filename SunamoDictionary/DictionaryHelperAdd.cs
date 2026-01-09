// variables names: ok
namespace SunamoDictionary;

/// <summary>
/// Helper methods for working with dictionaries - Add operations.
/// Provides specialized methods for adding entries to dictionaries with various conditions.
/// </summary>
public partial class DictionaryHelper
{
    /// <summary>
    /// Adds a key-value pair only if the key doesn't already exist.
    /// This method has unclear purpose and is marked as obsolete.
    /// </summary>
    /// <typeparam name="T1">The type of the dictionary key.</typeparam>
    /// <typeparam name="T2">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add.</param>
    /// <param name="value">The value to add.</param>
    [Obsolete("Method purpose is unclear")]
    public static void AddOrNoSet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, T2 value)
        where T1 : notnull
    {
        if (!dictionary.ContainsKey(key))
            dictionary.Add(key, value);
    }

    /// <summary>
    /// Gets the value for a key if it exists, otherwise creates it using a factory function.
    /// </summary>
    /// <typeparam name="T1">The type of the dictionary key.</typeparam>
    /// <typeparam name="T2">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to query or modify.</param>
    /// <param name="key">The key to look up or add.</param>
    /// <param name="valueFactory">Function that creates a new value from the key if it doesn't exist.</param>
    /// <returns>The existing value or the newly created value.</returns>
    public static T2 AddOrGet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, Func<T1, T2> valueFactory)
        where T1 : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            return dictionary[key];
        }

        var value = valueFactory.Invoke(key);
        dictionary.Add(key, value);
        return value;
    }

    #region AddOrCreateTimeSpan

    /// <summary>
    /// Adds or creates a TimeSpan entry from a DateTime value.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The DateTime value to convert to TimeSpan and add.</param>
    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> dictionary, Key key, DateTime value)
        where Key : notnull
    {
        var timeSpan = TimeSpan.FromTicks(value.Ticks);
        AddOrCreateTimeSpan(dictionary, key, timeSpan);
    }

    /// <summary>
    /// Adds or creates a TimeSpan entry, adding to existing value if key exists.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The TimeSpan value to add.</param>
    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> dictionary, Key key, TimeSpan value)
        where Key : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = dictionary[key].Add(value);
        else
            dictionary.Add(key, value);
    }

    #endregion

    #region Other

    /// <summary>
    /// Adds a key-value pair to a new dictionary if the key exists in the source dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The source dictionary to check.</param>
    /// <param name="key">The key to look for.</param>
    /// <param name="toReplace">The target dictionary to add to.</param>
    /// <param name="isThrowingExIfNotContains">Whether to throw exception if key is not found in source.</param>
    /// <exception cref="Exception">Thrown when key is not found and isThrowingExIfNotContains is true.</exception>
    public static void AddToNewDictionary<T, U>(Dictionary<T, U> dictionary, T key, Dictionary<T, U> toReplace,
        bool isThrowingExIfNotContains = true)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            if (!toReplace.ContainsKey(key)) toReplace.Add(key, dictionary[key]);
        }
        else
        {
            if (isThrowingExIfNotContains) ThrowEx.KeyNotFound(dictionary, nameof(dictionary), key);
        }
    }

    /// <summary>
    /// Adds a value to the dictionary at the specified index and returns the incremented index.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary value.</typeparam>
    /// <param name="index">The current index.</param>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="value">The value to add.</param>
    /// <returns>The incremented index.</returns>
    public static int AddToIndexAndReturnIncrementedInt<T>(int index, Dictionary<int, T> dictionary, T value)
    {
        dictionary.Add(index, value);
        index++;
        return index;
    }

    #endregion

    #region AddOrCreate

    /// <summary>
    /// Adds a value to a list in the dictionary, creating the list if the key doesn't exist.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of values in the list.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The value to add to the list.</param>
    public static void AddOrCreate<T, U>(Dictionary<T, List<U>> dictionary, T key, U value)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].Add(value);
        }
        else
        {
            var values = new List<U>();
            values.Add(value);
            dictionary.Add(key, values);
        }
    }

    /// <summary>
    /// Gets or creates a list in the dictionary using a factory function.
    /// </summary>
    /// <typeparam name="T1">The type of the dictionary key.</typeparam>
    /// <typeparam name="T2">The type of values in the list.</typeparam>
    /// <param name="dictionary">The dictionary to query or modify.</param>
    /// <param name="key">The key to look up or add.</param>
    /// <param name="valueFactory">Function that creates a new list from the key if it doesn't exist.</param>
    /// <returns>The existing list or the newly created list.</returns>
    public static List<T2> AddOrCreate<T1, T2>(Dictionary<T1, List<T2>> dictionary, T1 key,
        Func<T1, List<T2>> valueFactory)
        where T1 : notnull
    {
        if (!dictionary.ContainsKey(key))
        {
            var result = valueFactory(key);
            dictionary.Add(key, result);
            return result;
        }

        return dictionary[key];
    }

    /// <summary>
    /// Adds multiple values to a list in the dictionary, creating the list if needed.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of values in the lists.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="values">The values to add to the list.</param>
    /// <param name="isPreventingDuplicities">Whether to prevent duplicate values from being added.</param>
    /// <param name="stringDictionary">Optional dictionary for string comparison when preventing duplicates.</param>
    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> dictionary, Key key, List<Value> values,
        bool isPreventingDuplicities = false, Dictionary<Key, List<string>>? stringDictionary = null)
        where Key : notnull
    {
        foreach (var value in values) AddOrCreate<Key, Value, object>(dictionary, key, value, isPreventingDuplicities, stringDictionary);
    }

    #region AddOrCreate for collections

    /// <summary>
    /// Adds a value to a list in the dictionary with advanced comparison options.
    /// Supports comparing collections as keys and preventing duplicate values.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of values in the list.</typeparam>
    /// <typeparam name="ColType">The element type when key is a collection (use object if key is not a collection).</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The value to add to the list.</param>
    /// <param name="isPreventingDuplicities">Whether to prevent duplicate values from being added.</param>
    /// <param name="stringDictionary">Optional dictionary for string comparison when preventing duplicates.</param>
    public static void AddOrCreate<Key, Value, ColType>(IDictionary<Key, List<Value>> dictionary, Key key, Value value,
        bool isPreventingDuplicities = false, Dictionary<Key, List<string>>? stringDictionary = null)
        where Key : notnull
    {
        var isComparingWithString = false;
        if (stringDictionary != null) isComparingWithString = true;

        if (key is IList && typeof(ColType) != typeof(object))
        {
            var currentKey = key as IList<ColType>;
            var keyExists = false;
            foreach (var item in dictionary)
            {
                var existingKey = item.Key as IList<ColType>;
                if (existingKey.SequenceEqual(currentKey)) keyExists = true;
            }

            if (keyExists)
            {
                foreach (var item in dictionary)
                {
                    var existingKey = item.Key as IList<ColType>;
                    if (existingKey.SequenceEqual(currentKey))
                    {
                        if (isPreventingDuplicities)
                            if (item.Value.Contains(value))
                                return;
                        item.Value.Add(value);
                    }
                }
            }
            else
            {
                List<Value> values = new();
                values.Add(value);
                dictionary.Add(key, values);

                if (isComparingWithString)
                {
                    List<string> stringValues = new();
                    stringValues.Add(value.ToString());
                    stringDictionary.Add(key, stringValues);
                }
            }
        }
        else
        {
            var isAdding = true;
            lock (dictionary)
            {
                if (dictionary.ContainsKey(key))
                {
                    if (isPreventingDuplicities)
                    {
                        if (dictionary[key].Contains(value))
                            isAdding = false;
                        else if (isComparingWithString)
                            if (stringDictionary[key].Contains(value.ToString()))
                                isAdding = false;
                    }

                    if (isAdding)
                    {
                        var values = dictionary[key];

                        if (values != null) values.Add(value);

                        if (isComparingWithString)
                        {
                            var stringValues = stringDictionary[key];

                            if (values != null) stringValues.Add(value.ToString());
                        }
                    }
                }
                else
                {
                    if (!dictionary.ContainsKey(key))
                    {
                        List<Value> values = new();
                        values.Add(value);
                        dictionary.Add(key, values);
                    }
                    else
                    {
                        dictionary[key].Add(value);
                    }

                    if (isComparingWithString)
                    {
                        if (!stringDictionary.ContainsKey(key))
                        {
                            List<string> stringValues = new();
                            stringValues.Add(value.ToString());
                            stringDictionary.Add(key, stringValues);
                        }
                        else
                        {
                            stringDictionary[key].Add(value.ToString());
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Adds a value to a list in the dictionary, creating the list if the key doesn't exist.
    /// If dictionary contains group with name key, adds value to this group.
    /// Otherwise creates new group in dictionary with key and value.
    /// </summary>
    /// <typeparam name="Key">The type of the dictionary key.</typeparam>
    /// <typeparam name="Value">The type of values in the list.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="value">The value to add to the list.</param>
    /// <param name="isPreventingDuplicities">Whether to prevent duplicate values from being added.</param>
    /// <param name="stringDictionary">Optional dictionary for string comparison when preventing duplicates.</param>
    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> dictionary, Key key, Value value,
        bool isPreventingDuplicities = false, Dictionary<Key, List<string>>? stringDictionary = null)
        where Key : notnull
    {
        AddOrCreate<Key, Value, object>(dictionary, key, value, isPreventingDuplicities, stringDictionary);
    }

    #endregion

    #endregion

    #region AddOrPlus

    /// <summary>
    /// Adds a new key with initial value or increments existing value by the specified amount.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="increment">The amount to add to the value.</param>
    public static void AddOrPlus<T>(Dictionary<T, int> dictionary, T key, int increment)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }

    /// <summary>
    /// Adds a new key with initial value or increments existing value by the specified amount.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <param name="dictionary">The dictionary to modify.</param>
    /// <param name="key">The key to add or update.</param>
    /// <param name="increment">The amount to add to the value.</param>
    public static void AddOrPlus<T>(Dictionary<T, long> dictionary, T key, long increment)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }

    #endregion
}
