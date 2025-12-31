namespace SunamoDictionary;

/// <summary>
///     Už jsem z toho blázen
///     Mám tu DictionaryHelper a DictionaryHelper se stejným obsahem
///     value jiných částech řešení se stále používá DictionaryHelper
///     Takhle jsem to chtěl, abych neimportoval WithDeps do jiných projektů
///     Když jsem vyndal _sunamo a zkusil zkompilovat, deps chyběli value DictionaryHelper, 1x i value DictionaryHelperAdd
/// </summary>
public partial class DictionaryHelper
{
    public static Dictionary<string, string> GetDictionaryByKeyValueInString(string text, params string[] delimiters)
    {
        var parts = text.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToList(); //SHSplit.Split(text, delimiters);
        return GetDictionaryByKeyValueInString(parts);
    }

    public static Dictionary<U, T> SwitchKeyAndValue<T, U>(Dictionary<T, U> dictionary)
    {
        var result = new Dictionary<U, T>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add(item.Value, item.Key);
        return result;
    }

    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<int, T1> dictionary)
    {
        var result = new Dictionary<IDItemType, T1>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add((IDItemType)(dynamic)item.Key, item.Value);
        return result;
    }

    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<short, T1> dictionary)
    {
        var result = new Dictionary<IDItemType, T1>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add((IDItemType)(dynamic)item.Key, item.Value);
        return result;
    }

    public static Dictionary<T, T> GetDictionaryByKeyValueInString<T>(List<T> list)
    {
        var methodName = Exceptions.CallingMethod();
        ThrowEx.HasOddNumberOfElements("list", list);
        var result = new Dictionary<T, T>();
        for (var i = 0; i < list.Count; i++)
            result.Add(list[i], list[++i]);
        return result;
    }

    public static Dictionary<T1, T2> GetDictionaryFromTwoList<T1, T2>(List<T1> firstList, List<T2> secondList, bool shouldAddRandomWhenKeyExists = false)
    {
        // Zde mus� b�t .Count
        ThrowEx.DifferentCountInLists("firstList", firstList.Count, "secondList", secondList.Count);
        var list = new List<KeyValuePair<T1, T2>>();
        for (var i = 0; i < firstList.Count; i++)
            list.Add(new KeyValuePair<T1, T2>(firstList[i], secondList[i]));
        return GetDictionaryFromIList(list, shouldAddRandomWhenKeyExists);
    }

    public static List<U> GetValuesOrEmpty<T, U>(IDictionary<T, List<U>> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
            return dictionary[key];
        return new List<U>();
    }

    public static string GetOrKey<T>(Dictionary<T, string> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
            return dictionary[key];
        return key.ToString();
    }

    public static List<Dictionary<Key, Value>> DivideAfter<Key, Value>(Dictionary<Key, Value> dictionary, int chunkSize)
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

    public static Dictionary<T1, T2> CloneDictionary<T1, T2>(Dictionary<T1, T2> dictionary)
    {
        var newDictionary = dictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
        return newDictionary;
    }

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

    public static List<string> GetListStringFromDictionaryDateTimeInt(IOrderedEnumerable<KeyValuePair<DateTime, int>> dictionary)
    {
        var result = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            result.Add(item.Value.ToString());
        return result;
    }

    public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> dictionary)
    {
        var result = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            result.Add(item.Value.ToString());
        return result;
    }

    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable2<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIList<T1, T2>(List<KeyValuePair<T1, T2>> list, bool shouldAddRandomWhenKeyExists = false)
    {
        var dictionary = new Dictionary<T1, T2>();
        foreach (var item in list)
        {
            var key = item.Key;
            var keyExists = dictionary.ContainsKey(item.Key);
            if (keyExists)
                if (shouldAddRandomWhenKeyExists)
                {
                    var randomSuffix = key + " " + RandomHelper.RandomString(5);
                    key = (T1)(dynamic)randomSuffix;
                }

            dictionary.Add(key, item.Value);
        }

        return dictionary;
    }

    /// <summary>
    ///     If exists index A2, set to it A3
    ///     if don't, add with A3
    /// </summary>
    /// <typeparam name = "T1"></typeparam>
    /// <typeparam name = "T2"></typeparam>
    /// <param name = "dictionary"></param>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
    public static void AddOrSet<T1, T2>(IDictionary<T1, T2> dictionary, T1 key, T2 value)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key] = value;
        else
            dictionary.Add(key, value);
    }

    /// <summary>
    ///     Copy elements to A1 from A2
    /// </summary>
    /// <param name = "array"></param>
    /// <param name = "arrayIndex"></param>
    public static void CopyTo<T, U>(Dictionary<T, U> dictionary, int arrayIndex)
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