namespace SunamoDictionary;

public partial class DictionaryHelper
{
    [Obsolete("Vůbec nechápu smysl této metody")]
    public static void AddOrNoSet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, T2 value)
    {
        if (dictionary.ContainsKey(key))
        {
            //dictionary[key] = value;
        }
        else
        {
            dictionary.Add(key, value);
        }
    }

    /// <summary>
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="valueFactory"></param>
    /// <returns></returns>
    public static T2 AddOrGet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, Func<T1, T2> valueFactory)
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

    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> dictionary, Key key, DateTime value)
    {
        var timeSpan = TimeSpan.FromTicks(value.Ticks);
        AddOrCreateTimeSpan(dictionary, key, timeSpan);
    }

    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> dictionary, Key key, TimeSpan value)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = dictionary[key].Add(value);
        else
            dictionary.Add(key, value);
    }

    #endregion

    #region Other

    /// <summary>
    ///     Přidá do nového slovníku pokud existuje
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="toReplace"></param>
    /// <param name="throwExIfNotContains"></param>
    public static void AddToNewDictionary<T, U>(Dictionary<T, U> dictionary, T key, Dictionary<T, U> toReplace,
        bool shouldThrowExIfNotContains = true)
    {
        if (dictionary.ContainsKey(key))
        {
            if (!toReplace.ContainsKey(key)) toReplace.Add(key, dictionary[key]);
        }
        else
        {
            if (shouldThrowExIfNotContains) ThrowEx.KeyNotFound(dictionary, nameof(dictionary), key);
        }
    }


    public static int AddToIndexAndReturnIncrementedInt<T>(int index, Dictionary<int, T> dictionary, T value)
    {
        dictionary.Add(index, value);
        index++;
        return index;
    }

    #endregion

    #region AddOrCreate

    public static void AddOrCreate<T, U>(Dictionary<T, List<U>> dictionary, T key, U value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].Add(value);
        }
        else
        {
            var valueList = new List<U>();
            valueList.Add(value);
            dictionary.Add(key, valueList);
        }
    }

    public static List<T2> AddOrCreate<T1, T2>(Dictionary<T1, List<T2>> dictionary, T1 key,
        Func<T1, List<T2>> valueFactory)
    {
        if (!dictionary.ContainsKey(key))
        {
            var result = valueFactory(key);
            dictionary.Add(key, result);
            return result;
        }

        return dictionary[key];
    }

    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> dictionary, Key key, List<Value> values,
        bool shouldPreventDuplicities = false, Dictionary<Key, List<string>> stringDictionary = null)
    {
        foreach (var value in values) AddOrCreate<Key, Value, object>(dictionary, key, value, shouldPreventDuplicities, stringDictionary);
    }

    #region AddOrCreate když key i value není list

    /// <summary>
    ///     A3 is inner type of collection entries
    ///     stringDictionary => is comparing with string
    ///     As inner must be List, not IList etc.
    ///     From outside is not possible as inner use other class based on IList
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <typeparam name="ColType"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddOrCreate<Key, Value, ColType>(IDictionary<Key, List<Value>> dictionary, Key key, Value value,
        bool shouldPreventDuplicities = false, Dictionary<Key, List<string>> stringDictionary = null)
    {
        var shouldCompareWithString = false;
        if (stringDictionary != null) shouldCompareWithString = true;

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
                        if (shouldPreventDuplicities)
                            if (item.Value.Contains(value))
                                return;
                        item.Value.Add(value);
                    }
                }
            }
            else
            {
                List<Value> valueList = new();
                valueList.Add(value);
                dictionary.Add(key, valueList);

                if (shouldCompareWithString)
                {
                    List<string> stringList = new();
                    stringList.Add(value.ToString());
                    stringDictionary.Add(key, stringList);
                }
            }
        }
        else
        {
            var shouldAdd = true;
            lock (dictionary)
            {
                if (dictionary.ContainsKey(key))
                {
                    if (shouldPreventDuplicities)
                    {
                        if (dictionary[key].Contains(value))
                            shouldAdd = false;
                        else if (shouldCompareWithString)
                            if (stringDictionary[key].Contains(value.ToString()))
                                shouldAdd = false;
                    }

                    if (shouldAdd)
                    {
                        var valueList = dictionary[key];

                        if (valueList != null) valueList.Add(value);

                        if (shouldCompareWithString)
                        {
                            var stringList = stringDictionary[key];

                            if (valueList != null) stringList.Add(value.ToString());
                        }
                    }
                }
                else
                {
                    if (!dictionary.ContainsKey(key))
                    {
                        List<Value> valueList = new();
                        valueList.Add(value);
                        dictionary.Add(key, valueList);
                    }
                    else
                    {
                        dictionary[key].Add(value);
                    }

                    if (shouldCompareWithString)
                    {
                        if (!stringDictionary.ContainsKey(key))
                        {
                            List<string> stringList = new();
                            stringList.Add(value.ToString());
                            stringDictionary.Add(key, stringList);
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
    ///     Pokud A1 bude obsahovat skupinu pod názvem A2, vložím do této skupiny prvek A3
    ///     Jinak do A1 vytvořím novou skupinu s klíčem A2 s hodnotou A3
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> dictionary, Key key, Value value,
        bool shouldPreventDuplicities = false, Dictionary<Key, List<string>> stringDictionary = null)
    {
        AddOrCreate<Key, Value, object>(dictionary, key, value, shouldPreventDuplicities, stringDictionary);
    }

    #endregion

    #endregion

    #region AddOrPlus

    public static void AddOrPlus<T>(Dictionary<T, int> dictionary, T key, int increment)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }

    public static void AddOrPlus<T>(Dictionary<T, long> dictionary, T key, long increment)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] += increment;
        else
            dictionary.Add(key, increment);
    }

    #endregion

    //public static void AddOrSet(Dictionary<string, string> qs, string k, string value)
    //{
    //    if (qs.ContainsKey(k))
    //    {
    //        qs[k] = value;
    //    }
    //    else
    //    {
    //        qs.Add(k, value);
    //    }
    //}
}