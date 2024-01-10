namespace SunamoDictionary;





public partial class DictionaryHelper : DictionaryHelperSE
{



    private static Type type = typeof(DictionaryHelper);


    public static void IncrementOrCreate<T>(Dictionary<T, int> sl, T baseNazevTabulky)
    {
        if (sl.ContainsKey(baseNazevTabulky))
        {
            sl[baseNazevTabulky]++;
        }
        else
        {
            sl.Add(baseNazevTabulky, 1);
        }
    }
    public static Value GetFirstItemValue<Key, Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict)
        {
            return item.Value;
        }

        return default(Value);
    }

    public static Key GetFirstItemKey<Key, Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict)
        {
            return item.Key;
        }

        return default(Key);
    }

    public static short AddToIndexAndReturnIncrementedShort<T>(short i, Dictionary<short, T> colors, T colorOnWeb)
    {
        colors.Add(i, colorOnWeb);
        i++;
        return i;
    }

    public static Dictionary<Key, Value> GetDictionary<Key, Value>(List<Key> keys, List<Value> values)
    {
        ThrowEx.DifferentCountInLists("keys", keys.Count, "values", values.Count);
        Dictionary<Key, Value> result = new Dictionary<Key, Value>();
        for (int i = 0; i < keys.Count; i++)
        {
            result.Add(keys[i], values[i]);
        }

        return result;
    }

    public static Dictionary<string, string> GetDictionaryByKeyValueInString(string p, params string[] d1)
    {
        var sp = SHSplit.SplitMore(p, d1);
        return GetDictionaryByKeyValueInString<string>(sp);
    }

    public static Dictionary<U, T> SwitchKeyAndValue<T, U>(Dictionary<T, U> dictionary)
    {
        Dictionary<U, T> d = new Dictionary<U, T>(dictionary.Count);
        foreach (var item in dictionary)
        {
            d.Add(item.Value, item.Key);
        }
        return d;
    }


    public static void AddOrNoSet<T1, T2>(IDictionary<T1, T2> qs, T1 k, T2 v)
    {
        if (qs.ContainsKey(k))
        {
            //qs[k] = v;
        }
        else
        {
            qs.Add(k, v);
        }
    }

    public static T2 AddOrGet<T1, T2>(IDictionary<T1, T2> qs, T1 k, Func<T1, T2> i)
    {
        if (qs.ContainsKey(k))
        {
            return qs[k];
        }
        else
        {
            var v = i.Invoke(k);
            qs.Add(k, v);
            return v;
        }
    }

    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<int, T1> toAdd)
    {
        Dictionary<IDItemType, T1> r = new Dictionary<IDItemType, T1>(toAdd.Count);
        foreach (var item in toAdd)
        {
            r.Add((IDItemType)(dynamic)item.Key, item.Value);
        }
        return r;
    }

    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<short, T1> toAdd)
    {
        Dictionary<IDItemType, T1> r = new Dictionary<IDItemType, T1>(toAdd.Count);
        foreach (var item in toAdd)
        {
            r.Add((IDItemType)(dynamic)item.Key, item.Value);
        }
        return r;
    }

    public static Dictionary<T, T> GetDictionaryByKeyValueInString<T>(List<T> p)
    {
        var methodName = Exc.CallingMethod();
        ThrowEx.IsOdd("p", p);

        Dictionary<T, T> result = new Dictionary<T, T>();
        for (int i = 0; i < p.Count; i++)
        {
            result.Add(p[i], p[++i]);
        }
        return result;
    }



    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> sl, Key key, List<Value> values, bool withoutDuplicitiesInValue = false, Dictionary<Key, List<string>> dictS = null)
    {
        foreach (var value in values)
        {
            AddOrCreate<Key, Value, object>(sl, key, value, withoutDuplicitiesInValue, dictS);
        }
    }




    public static Dictionary<T1, T2> GetDictionaryFromTwoList<T1, T2>(List<T1> t1, List<T2> t2, bool addRandomWhenKeyExists = false)
    {
        // Zde mus� b�t .Count
        ThrowEx.DifferentCountInLists("t1", t1.Count, "t2", t2.Count);

        List<KeyValuePair<T1, T2>> l = new List<KeyValuePair<T1, T2>>();

        for (int i = 0; i < t1.Count; i++)
        {
            l.Add(new KeyValuePair<T1, T2>(t1[i], t2[i]));
        }

        return GetDictionaryFromIList<T1, T2>(l, addRandomWhenKeyExists);
    }



    public static List<U> GetValuesOrEmpty<T, U>(IDictionary<T, List<U>> dict, T t, U u)
    {
        if (dict.ContainsKey(t))
        {
            return dict[t];
        }
        return new List<U>();
    }

    public static string GetOrKey<T>(Dictionary<T, string> a, T item2)
    {
        if (a.ContainsKey(item2))
        {
            return a[item2];
        }
        return item2.ToString();
    }

    public static List<Dictionary<Key, Value>> DivideAfter<Key, Value>(Dictionary<Key, Value> dict, int v)
    {
        List<Dictionary<Key, Value>> retur = new List<Dictionary<Key, Value>>();
        Dictionary<Key, Value> ds = new Dictionary<Key, Value>();

        foreach (var item in dict)
        {
            ds.Add(item.Key, item.Value);
            if (ds.Count == v)
            {
                retur.Add(ds);
                ds = new Dictionary<Key, Value>();
            }
        }

        if (ds.Count > 0)
        {
            retur.Add(ds);
        }

        return retur;
    }


    public static Dictionary<T1, T2> CloneDictionary<T1, T2>(Dictionary<T1, T2> filesWithTranslation)
    {
        var newDictionary = filesWithTranslation.ToDictionary(entry => entry.Key,
        entry => entry.Value);
        return newDictionary;
    }

    public static List<string> GetListStringFromDictionary(Dictionary<string, string> p)
    {
        List<string> vr = new List<string>();

        foreach (var item in p)
        {
            vr.Add(item.Key);
            vr.Add(item.Value);
        }

        return vr;
    }

    public static void AddOrSet(Dictionary<string, string> qs, string k, string v)
    {
        if (qs.ContainsKey(k))
        {
            qs[k] = v;
        }
        else
        {
            qs.Add(k, v);
        }
    }

    public static List<string> GetListStringFromDictionaryDateTimeInt(IOrderedEnumerable<KeyValuePair<System.DateTime, int>> d)
    {
        List<string> vr = new List<string>(d.Count());
        foreach (var item in d)
        {
            vr.Add(item.Value.ToString());
        }

        return vr;
    }

    public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> d)
    {
        List<string> vr = new List<string>(d.Count());
        foreach (var item in d)
        {
            vr.Add(item.Value.ToString());
        }

        return vr;
    }


}
