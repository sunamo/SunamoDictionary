// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
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
    public static Dictionary<string, string> GetDictionaryByKeyValueInString(string p, params string[] d1)
    {
        var sp = p.Split(d1, StringSplitOptions.RemoveEmptyEntries).ToList(); //SHSplit.Split(p, d1);
        return GetDictionaryByKeyValueInString(sp);
    }

    public static Dictionary<U, T> SwitchKeyAndValue<T, U>(Dictionary<T, U> dictionary)
    {
        var result = new Dictionary<U, T>(dictionary.Count);
        foreach (var item in dictionary)
            result.Add(item.Value, item.Key);
        return result;
    }

    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<int, T1> toAdd)
    {
        var result = new Dictionary<IDItemType, T1>(toAdd.Count);
        foreach (var item in toAdd)
            result.Add((IDItemType)(dynamic)item.Key, item.Value);
        return result;
    }

    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<short, T1> toAdd)
    {
        var result = new Dictionary<IDItemType, T1>(toAdd.Count);
        foreach (var item in toAdd)
            result.Add((IDItemType)(dynamic)item.Key, item.Value);
        return result;
    }

    public static Dictionary<T, T> GetDictionaryByKeyValueInString<T>(List<T> p)
    {
        var methodName = Exceptions.CallingMethod();
        ThrowEx.HasOddNumberOfElements("p", p);
        var result = new Dictionary<T, T>();
        for (var i = 0; i < p.Count; i++)
            result.Add(p[i], p[++i]);
        return result;
    }

    public static Dictionary<T1, T2> GetDictionaryFromTwoList<T1, T2>(List<T1> t1, List<T2> t2, bool addRandomWhenKeyExists = false)
    {
        // Zde mus� b�t .Count
        ThrowEx.DifferentCountInLists("t1", t1.Count, "t2", t2.Count);
        var list = new List<KeyValuePair<T1, T2>>();
        for (var i = 0; i < t1.Count; i++)
            list.Add(new KeyValuePair<T1, T2>(t1[i], t2[i]));
        return GetDictionaryFromIList(list, addRandomWhenKeyExists);
    }

    public static List<U> GetValuesOrEmpty<T, U>(IDictionary<T, List<U>> dict, T t)
    {
        if (dict.ContainsKey(t))
            return dict[t];
        return new List<U>();
    }

    public static string GetOrKey<T>(Dictionary<T, string> a, T item2)
    {
        if (a.ContainsKey(item2))
            return a[item2];
        return item2.ToString();
    }

    public static List<Dictionary<Key, Value>> DivideAfter<Key, Value>(Dictionary<Key, Value> dict, int value)
    {
        var retur = new List<Dictionary<Key, Value>>();
        var ds = new Dictionary<Key, Value>();
        foreach (var item in dict)
        {
            ds.Add(item.Key, item.Value);
            if (ds.Count == value)
            {
                retur.Add(ds);
                ds = new Dictionary<Key, Value>();
            }
        }

        if (ds.Count > 0)
            retur.Add(ds);
        return retur;
    }

    public static Dictionary<T1, T2> CloneDictionary<T1, T2>(Dictionary<T1, T2> filesWithTranslation)
    {
        var newDictionary = filesWithTranslation.ToDictionary(entry => entry.Key, entry => entry.Value);
        return newDictionary;
    }

    public static List<string> GetListStringFromDictionary(Dictionary<string, string> p)
    {
        var vr = new List<string>();
        foreach (var item in p)
        {
            vr.Add(item.Key);
            vr.Add(item.Value);
        }

        return vr;
    }

    public static List<string> GetListStringFromDictionaryDateTimeInt(IOrderedEnumerable<KeyValuePair<DateTime, int>> dictionary)
    {
        var vr = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            vr.Add(item.Value.ToString());
        return vr;
    }

    public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> dictionary)
    {
        var vr = new List<string>(dictionary.Count());
        foreach (var item in dictionary)
            vr.Add(item.Value.ToString());
        return vr;
    }

    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable2<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIList<T1, T2>(List<KeyValuePair<T1, T2>> enumerable, bool addRandomWhenKeyExists = false)
    {
        var dictionary = new Dictionary<T1, T2>();
        foreach (var item in enumerable)
        {
            var key = item.Key;
            var count = dictionary.ContainsKey(item.Key);
            if (count)
                if (addRandomWhenKeyExists)
                {
                    var k = key + " " + RandomHelper.RandomString(5);
                    key = (T1)(dynamic)k;
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
    /// <param name = "qs"></param>
    /// <param name = "k"></param>
    /// <param name = "v"></param>
    public static void AddOrSet<T1, T2>(IDictionary<T1, T2> qs, T1 k, T2 value)
    {
        if (qs.ContainsKey(k))
            qs[k] = value;
        else
            qs.Add(k, value);
    }

    /// <summary>
    ///     Copy elements to A1 from A2
    /// </summary>
    /// <param name = "array"></param>
    /// <param name = "arrayIndex"></param>
    public static void CopyTo<T, U>(Dictionary<T, U> _d, int arrayIndex)
    {
        var array = new KeyValuePair<T, U>[_d.Count - arrayIndex + 1];
        var i = 0;
        var add = false;
        foreach (var item in _d)
        {
            if (i == arrayIndex && !add)
            {
                add = true;
                i = 0;
            }

            if (add)
                array[i] = new KeyValuePair<T, U>(item.Key, item.Value);
            i++;
        }
    }

    public static void CopyTo<T, U>(List<KeyValuePair<T, U>> _d, int arrayIndex)
    {
        var array = new KeyValuePair<T, U>[_d.Count - arrayIndex + 1];
        var i = 0;
        var add = false;
        foreach (var item in _d)
        {
            if (i == arrayIndex && !add)
            {
                add = true;
                i = 0;
            }

            if (add)
                array[i] = new KeyValuePair<T, U>(item.Key, item.Value);
            i++;
        }
    }
}