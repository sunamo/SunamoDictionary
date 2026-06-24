namespace SunamoDictionary;

// Helper methods for working with dictionaries - Add operations.
// Provides specialized methods for adding entries to dictionaries with various conditions.
public partial class DictionaryHelper
{
    // Adds a key-value pair only if the key doesn't already exist.
    // This method has unclear purpose and is marked as obsolete.
    [Obsolete("Method purpose is unclear")]
    public static void AddOrNoSet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, T2 value)
        where T1 : notnull
    {
        if (!dictionary.ContainsKey(key))
            dictionary.Add(key, value);
    }

    // Gets the value for a key if it exists, otherwise creates it using a factory function.
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

    // Adds or creates a TimeSpan entry from a DateTime value.
    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> dictionary, Key key, DateTime value)
        where Key : notnull
    {
        var timeSpan = TimeSpan.FromTicks(value.Ticks);
        AddOrCreateTimeSpan(dictionary, key, timeSpan);
    }

    // Adds or creates a TimeSpan entry, adding to existing value if key exists.
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

    // Adds a key-value pair to a new dictionary if the key exists in the source dictionary.
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

    // Adds a value to the dictionary at the specified index and returns the incremented index.
    public static int AddToIndexAndReturnIncrementedInt<T>(int index, Dictionary<int, T> dictionary, T value)
    {
        dictionary.Add(index, value);
        index++;
        return index;
    }

    #endregion

    #region AddOrCreate

    // Adds a value to a list in the dictionary, creating the list if the key doesn't exist.
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

    // Gets or creates a list in the dictionary using a factory function.
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

    // Adds multiple values to a list in the dictionary, creating the list if needed.
    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> dictionary, Key key, List<Value> values,
        bool isPreventingDuplicities = false, Dictionary<Key, List<string>>? stringDictionary = null)
        where Key : notnull
    {
        foreach (var value in values) AddOrCreate<Key, Value, object>(dictionary, key, value, isPreventingDuplicities, stringDictionary);
    }

    #region AddOrCreate for collections

    // Adds a value to a list in the dictionary with advanced comparison options.
    // Supports comparing collections as keys and preventing duplicate values.
    public static void AddOrCreate<Key, Value, ColType>(IDictionary<Key, List<Value>> dictionary, Key key, Value value,
        bool isPreventingDuplicities = false, Dictionary<Key, List<string>>? stringDictionary = null)
        where Key : notnull
    {
        var isComparingWithString = stringDictionary != null;

        if (key is IList && typeof(ColType) != typeof(object))
        {
            var currentKey = key as IList<ColType>;
            var keyExists = false;
            foreach (var item in dictionary)
            {
                var existingKey = item.Key as IList<ColType>;
                if (existingKey!.SequenceEqual(currentKey!)) keyExists = true;
            }

            if (keyExists)
            {
                foreach (var item in dictionary)
                {
                    var existingKey = item.Key as IList<ColType>;
                    if (existingKey!.SequenceEqual(currentKey!))
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
                    stringValues.Add(value!.ToString()!);
                    stringDictionary!.Add(key, stringValues);
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
                            if (stringDictionary![key].Contains(value!.ToString()!))
                                isAdding = false;
                    }

                    if (isAdding)
                    {
                        var values = dictionary[key];

                        if (values != null) values.Add(value);

                        if (isComparingWithString)
                        {
                            var stringValues = stringDictionary![key];

                            if (values != null) stringValues.Add(value!.ToString()!);
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
                        if (!stringDictionary!.ContainsKey(key))
                        {
                            List<string> stringValues = new();
                            stringValues.Add(value!.ToString()!);
                            stringDictionary.Add(key, stringValues);
                        }
                        else
                        {
                            stringDictionary[key].Add(value!.ToString()!);
                        }
                    }
                }
            }
        }
    }

    // Adds a value to a list in the dictionary, creating the list if the key doesn't exist.
    // If dictionary contains group with name key, adds value to this group.
    // Otherwise creates new group in dictionary with key and value.
    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> dictionary, Key key, Value value,
        bool isPreventingDuplicities = false, Dictionary<Key, List<string>>? stringDictionary = null)
        where Key : notnull
    {
        AddOrCreate<Key, Value, object>(dictionary, key, value, isPreventingDuplicities, stringDictionary);
    }

    #endregion

    #endregion

    #region AddOrPlus

    // Adds a new key with initial value or increments existing value by the specified amount.
    public static void AddOrPlus<T>(Dictionary<T, int> dictionary, T key, int increment)
        where T : notnull
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }

    // Adds a new key with initial value or increments existing value by the specified amount.
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
