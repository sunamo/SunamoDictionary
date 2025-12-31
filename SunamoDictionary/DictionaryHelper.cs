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
    /// <summary>
    ///     In addition to method AddOrCreate, more is checking whether value in collection does not exists
    /// </summary>
    /// <typeparam name = "Key"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <param name = "dictionary"></param>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
    public static void AddOrCreateIfDontExists<Key, Value>(Dictionary<Key, List<Value>> dictionary, Key key, Value value)
    {
        if (dictionary.ContainsKey(key))
        {
            if (!dictionary[key].Contains(value))
                dictionary[key].Add(value);
        }
        else
        {
            var valueList = new List<Value>();
            valueList.Add(value);
            dictionary.Add(key, valueList);
        }
    }

    public static string CalculateMedianAverageFloat(Dictionary<string, List<float>> dictionary, object textOutputGenerator)
    {
        throw new Exception("Deps methods, MedianAverage<T> etc.");
    //foreach (var item in dictionary)
    //{
    //    textOutputGenerator.Header(item.Key);
    //    var (s, ma) = NH.CalculateMedianAverageNoOut(item.Value);
    //    textOutputGenerator.AppendLine(s);
    //}
    //return textOutputGenerator.ToString();
    }

    public static Dictionary<string, string> KeepOnlyKeys(Dictionary<string, string> allParams, List<string> includeAlways)
    {
        foreach (var item in allParams.Keys.ToList())
            if (!includeAlways.Contains(item))
                allParams.Remove(item);
        return allParams;
    }

    public static Dictionary<string, List<string>> CategoryParser(List<string> list, bool shouldRemoveWhichHaveNoEntries)
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

        if (shouldRemoveWhichHaveNoEntries)
            for (var i = result.Keys.Count - 1; i >= 0; i--)
            {
                var key = result.ElementAt(i).Key;
                if (result[key][0] == "No entries")
                    result.Remove(key);
            }

        return result;
    }

    public static List<KeyValuePair<T, int>> CountOfItems<T>(List<T> items)
    {
        var pairs = new Dictionary<T, int>();
        foreach (var item in items)
            AddOrPlus(pairs, item, 1);
        var orderedPairs = pairs.OrderByDescending(dictionary => dictionary.Value);
        var result = orderedPairs.ToList();
        return result;
    }

    public static object CreateTree(Dictionary<string, List<string>> dictionary)
    {
        throw new Exception("Code without NTreeDictionary");
    //NTreeDictionary<string> t = new NTreeDic tionary<string>(string.Empty);
    //foreach (var item in dictionary)
    //{
    //    var child = t.AddChild(item.Key);
    //    foreach (var value in item.Value)
    //    {
    //        child.AddChild(value);
    //    }
    //    child.children = new LinkedList<NTreeDictionary<string>>(child.children.Reverse());
    //}
    //return t;
    }

    public static void RemoveIfExists<T, U>(Dictionary<T, List<U>> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
            dictionary.Remove(key);
    }

    public static IList<string> GetIfExists(Dictionary<string, List<string>> dictionary, string prefix, string key, bool shouldAddPrefixAndSuffix)
    {
        if (dictionary.ContainsKey(key))
        {
            var result = dictionary[key];
            if (shouldAddPrefixAndSuffix)
            {
                if (!string.IsNullOrEmpty(key))
                    result = CA.PostfixIfNotEnding(key, result);
                CA.Prepend(prefix, result);
            }

            return result;
        }

        return new List<string>();
    }

    public static Dictionary<T, List<U>> GroupByValues<U, T, ColType>(Dictionary<U, T> dictionary)
    {
        var result = new Dictionary<T, List<U>>();
        foreach (var item in dictionary)
            AddOrCreate<T, U, ColType>(result, item.Value, item.Key);
        return result;
    }

    public static List<T2> AggregateValues<T1, T2>(Dictionary<T2, List<T2>> dictionary)
    {
        var result = new List<T2>();
        foreach (var entry in dictionary)
            result.AddRange(entry.Value);
        return result;
    }

    /// <summary>
    ///     Return p1 if exists key A2 with value no equal to A3
    /// </summary>
    /// <param name = "dictionary"></param>
    private T FindIndexOfValue<T, U>(Dictionary<T, U> dictionary, U targetValue, T comparisonKey)
    {
        throw new Exception("RUn without ComparerConsts");
    //foreach (KeyValuePair<T, U> var in dictionary)
    //{
    //    if (Comparer<U>.Default.Compare(var.Value, targetValue) == ComparerConsts.Higher && Comparer<T>.Default.Compare(var.Key, comparisonKey) == ComparerConsts.Lower)
    //    {
    //        return var.Key;
    //    }
    //}
    //return default(T);
    }

    public static Dictionary<T, U> ReturnsCopy<T, U>(Dictionary<T, U> dictionary)
    {
        var result = new Dictionary<T, U>();
        foreach (var item in dictionary)
            result.Add(item.Key, item.Value);
        return result;
    }

    /// <summary>
    ///     A2 can be null
    /// </summary>
    /// <typeparam name = "T1"></typeparam>
    /// <typeparam name = "T2"></typeparam>
    /// <param name = "dictionary"></param>
    /// <param name = "duplicates"></param>
    /// <returns></returns>
    public static Dictionary<T1, T2> RemoveDuplicatedFromDictionaryByValues<T1, T2>(Dictionary<T1, T2> dictionary, Dictionary<T1, T2> duplicates)
    {
        //duplicates = new Dictionary<T1, T2>();
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

    public static int CountAllValues<Key, Value>(Dictionary<Key, List<Value>> dictionary)
    {
        var totalCount = 0;
        foreach (var item in dictionary)
            totalCount += item.Value.Count();
        return totalCount;
    }

    public static void IncrementOrCreate<T>(Dictionary<T, int> dictionary, T key)
    {
        if (dictionary.ContainsKey(key))
            dictionary[key]++;
        else
            dictionary.Add(key, 1);
    }

    public static Value GetFirstItemValue<Key, Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict)
            return item.Value;
        return default;
    }

    public static Key GetFirstItemKey<Key, Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict)
            return item.Key;
        return default;
    }

    public static short AddToIndexAndReturnIncrementedShort<T>(short index, Dictionary<short, T> dictionary, T value)
    {
        dictionary.Add(index, value);
        index++;
        return index;
    }

    public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values)
    {
        ThrowEx.DifferentCountInLists("keys", keys.Count, "values", values.Count);
        var result = new Dictionary<Key, Value>();
        for (var i = 0; i < keys.Count; i++)
            result.Add(keys[i], values[i]);
        return result;
    }
}