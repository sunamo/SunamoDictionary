namespace SunamoDictionary;

/// <summary>
///     Už jsem z toho blázen
///     Mám tu DictionaryHelper a DictionaryHelper se stejným obsahem
///     V jiných částech řešení se stále používá DictionaryHelper
///     Takhle jsem to chtěl, abych neimportoval WithDeps do jiných projektů
///     Když jsem vyndal _sunamo a zkusil zkompilovat, deps chyběli v DictionaryHelper, 1x i v DictionaryHelperAdd
/// </summary>
public partial class DictionaryHelper
{
    private static Type type = typeof(DictionaryHelper);

    /// <summary>
    ///     In addition to method AddOrCreate, more is checking whether value in collection does not exists
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="sl"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddOrCreateIfDontExists<Key, Value>(Dictionary<Key, List<Value>> sl, Key key, Value value)
    {
        if (sl.ContainsKey(key))
        {
            if (!sl[key].Contains(value)) sl[key].Add(value);
        }
        else
        {
            var ad = new List<Value>();
            ad.Add(value);
            sl.Add(key, ad);
        }
    }


    public static string CalculateMedianAverageFloat(Dictionary<string, List<float>> dict, object tog)
    {
        throw new Exception("Deps methods, MedianAverage<T> etc.");
        //foreach (var item in dict)
        //{
        //    tog.Header(item.Key);

        //    var (s, ma) = NH.CalculateMedianAverageNoOut(item.Value);

        //    tog.AppendLine(s);
        //}

        //return tog.ToString();
    }

    public static Dictionary<string, string> KeepOnlyKeys(Dictionary<string, string> allParams,
        List<string> includeAlways)
    {
        foreach (var item in allParams.Keys.ToList())
            if (!includeAlways.Contains(item))
                allParams.Remove(item);

        return allParams;
    }

    public static Dictionary<string, List<string>> CategoryParser(List<string> l, bool removeWhichHaveNoEntries)
    {
        var ds = new Dictionary<string, List<string>>();

        List<string> lsToAdd = null;

        for (var i = 0; i < l.Count; i++)
        {
            var item = l[i].Trim();
            if (item == string.Empty) continue;
            if (item.EndsWith(":"))
            {
                lsToAdd = new List<string>();
                ds.Add(item.TrimEnd(':'), lsToAdd);
            }
            else
            {
                lsToAdd.Add(item);
            }
        }

        if (removeWhichHaveNoEntries)
            for (var i = ds.Keys.Count - 1; i >= 0; i--)
            {
                var key = ds.ElementAt(i).Key;
                if (ds[key][0] == "No entries") ds.Remove(key);
            }

        return ds;
    }


    public static List<KeyValuePair<T, int>> CountOfItems<T>(List<T> streets)
    {
        var pairs = new Dictionary<T, int>();
        foreach (var item in streets) AddOrPlus(pairs, item, 1);

        var v = pairs.OrderByDescending(d => d.Value);
        var r = v.ToList();
        return r;
    }

    public static object CreateTree(Dictionary<string, List<string>> d)
    {
        throw new Exception("Code without NTreeDictionary");
        //NTreeDictionary<string> t = new NTreeDic tionary<string>(string.Empty);

        //foreach (var item in d)
        //{
        //    var child = t.AddChild(item.Key);

        //    foreach (var v in item.Value)
        //    {
        //        child.AddChild(v);
        //    }

        //    child.children = new LinkedList<NTreeDictionary<string>>(child.children.Reverse());
        //}


        //return t;
    }

    public static void RemoveIfExists<T, U>(Dictionary<T, List<U>> st, T v)
    {
        if (st.ContainsKey(v)) st.Remove(v);
    }

    public static IList<string> GetIfExists(Dictionary<string, List<string>> filesInSolutionReal, string prefix,
        string v, bool postfixWithA2)
    {
        if (filesInSolutionReal.ContainsKey(v))
        {
            var r = filesInSolutionReal[v];
            if (postfixWithA2)
            {
                if (!string.IsNullOrEmpty(v)) r = CA.PostfixIfNotEnding(v, r);
                CA.Prepend(prefix, r);
            }

            return r;
        }

        return new List<string>();
    }

    public static Dictionary<T, List<U>> GroupByValues<U, T, ColType>(Dictionary<U, T> dictionary)
    {
        var result = new Dictionary<T, List<U>>();
        foreach (var item in dictionary) AddOrCreate<T, U, ColType>(result, item.Value, item.Key);

        return result;
    }

    public static List<T2> AggregateValues<T1, T2>(Dictionary<T2, List<T2>> lowCostNotFoundEurope)
    {
        var result = new List<T2>();
        foreach (var lcCountry in lowCostNotFoundEurope) result.AddRange(lcCountry.Value);

        return result;
    }

    /// <summary>
    ///     Return p1 if exists key A2 with value no equal to A3
    /// </summary>
    /// <param name="g"></param>
    private T FindIndexOfValue<T, U>(Dictionary<T, U> g, U p1, T p2)
    {
        throw new Exception("RUn without ComparerConsts");
        //foreach (KeyValuePair<T, U> var in g)
        //{
        //    if (Comparer<U>.Default.Compare(var.Value, p1) == ComparerConsts.Higher && Comparer<T>.Default.Compare(var.Key, p2) == ComparerConsts.Lower)
        //    {
        //        return var.Key;
        //    }
        //}

        //return default(T);
    }


    public static Dictionary<T, U> ReturnsCopy<T, U>(Dictionary<T, U> slovnik)
    {
        var tu = new Dictionary<T, U>();
        foreach (var item in slovnik) tu.Add(item.Key, item.Value);

        return tu;
    }


    /// <summary>
    ///     A2 can be null
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="airPlaneCompanies"></param>
    /// <param name="twoTimes"></param>
    /// <returns></returns>
    public static Dictionary<T1, T2> RemoveDuplicatedFromDictionaryByValues<T1, T2>(
        Dictionary<T1, T2> airPlaneCompanies, Dictionary<T1, T2> twoTimes)
    {
        //twoTimes = new Dictionary<T1, T2>();
        var processed = new List<T2>();
        foreach (var item in airPlaneCompanies.Keys.ToList())
        {
            var value = airPlaneCompanies[item];
            if (processed.Contains(value))
            {
                if (twoTimes != null) twoTimes.Add(item, value);

                airPlaneCompanies.Remove(item);
            }
            else
            {
                processed.Add(value);
            }
        }

        return airPlaneCompanies;
    }

    public static int CountAllValues<Key, Value>(Dictionary<Key, List<Value>> fe)
    {
        var nt = 0;
        foreach (var item in fe) nt += item.Value.Count();

        return nt;
    }


    public static void IncrementOrCreate<T>(Dictionary<T, int> sl, T baseNazevTabulky)
    {
        if (sl.ContainsKey(baseNazevTabulky))
            sl[baseNazevTabulky]++;
        else
            sl.Add(baseNazevTabulky, 1);
    }

    public static Value GetFirstItemValue<Key, Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict) return item.Value;

        return default;
    }

    public static Key GetFirstItemKey<Key, Value>(Dictionary<Key, Value> dict)
    {
        foreach (var item in dict) return item.Key;

        return default;
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
        var result = new Dictionary<Key, Value>();
        for (var i = 0; i < keys.Count; i++) result.Add(keys[i], values[i]);

        return result;
    }

    public static Dictionary<string, string> GetDictionaryByKeyValueInString(string p, params string[] d1)
    {
        var sp = p.Split(d1, StringSplitOptions.RemoveEmptyEntries).ToList(); //SHSplit.Split(p, d1);
        return GetDictionaryByKeyValueInString(sp);
    }

    public static Dictionary<U, T> SwitchKeyAndValue<T, U>(Dictionary<T, U> dictionary)
    {
        var d = new Dictionary<U, T>(dictionary.Count);
        foreach (var item in dictionary) d.Add(item.Value, item.Key);
        return d;
    }


    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<int, T1> toAdd)
    {
        var r = new Dictionary<IDItemType, T1>(toAdd.Count);
        foreach (var item in toAdd) r.Add((IDItemType)(dynamic)item.Key, item.Value);
        return r;
    }

    public static Dictionary<IDItemType, T1> ChangeTypeOfKey<IDItemType, T1>(Dictionary<short, T1> toAdd)
    {
        var r = new Dictionary<IDItemType, T1>(toAdd.Count);
        foreach (var item in toAdd) r.Add((IDItemType)(dynamic)item.Key, item.Value);
        return r;
    }

    public static Dictionary<T, T> GetDictionaryByKeyValueInString<T>(List<T> p)
    {
        var methodName = Exceptions.CallingMethod();
        ThrowEx.HasOddNumberOfElements("p", p);

        var result = new Dictionary<T, T>();
        for (var i = 0; i < p.Count; i++) result.Add(p[i], p[++i]);
        return result;
    }


    public static Dictionary<T1, T2> GetDictionaryFromTwoList<T1, T2>(List<T1> t1, List<T2> t2,
        bool addRandomWhenKeyExists = false)
    {
        // Zde mus� b�t .Count
        ThrowEx.DifferentCountInLists("t1", t1.Count, "t2", t2.Count);

        var l = new List<KeyValuePair<T1, T2>>();

        for (var i = 0; i < t1.Count; i++) l.Add(new KeyValuePair<T1, T2>(t1[i], t2[i]));

        return GetDictionaryFromIList(l, addRandomWhenKeyExists);
    }


    public static List<U> GetValuesOrEmpty<T, U>(IDictionary<T, List<U>> dict, T t)
    {
        if (dict.ContainsKey(t)) return dict[t];
        return new List<U>();
    }

    public static string GetOrKey<T>(Dictionary<T, string> a, T item2)
    {
        if (a.ContainsKey(item2)) return a[item2];
        return item2.ToString();
    }

    public static List<Dictionary<Key, Value>> DivideAfter<Key, Value>(Dictionary<Key, Value> dict, int v)
    {
        var retur = new List<Dictionary<Key, Value>>();
        var ds = new Dictionary<Key, Value>();

        foreach (var item in dict)
        {
            ds.Add(item.Key, item.Value);
            if (ds.Count == v)
            {
                retur.Add(ds);
                ds = new Dictionary<Key, Value>();
            }
        }

        if (ds.Count > 0) retur.Add(ds);

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
        var vr = new List<string>();

        foreach (var item in p)
        {
            vr.Add(item.Key);
            vr.Add(item.Value);
        }

        return vr;
    }


    public static List<string> GetListStringFromDictionaryDateTimeInt(IOrderedEnumerable<KeyValuePair<DateTime, int>> d)
    {
        var vr = new List<string>(d.Count());
        foreach (var item in d) vr.Add(item.Value.ToString());

        return vr;
    }

    public static List<string> GetListStringFromDictionaryIntInt(IOrderedEnumerable<KeyValuePair<int, int>> d)
    {
        var vr = new List<string>(d.Count());
        foreach (var item in d) vr.Add(item.Value.ToString());

        return vr;
    }


    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable<T1, T2>(
        IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable2<T1, T2>(
        IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIList<T1, T2>(List<KeyValuePair<T1, T2>> enumerable,
        bool addRandomWhenKeyExists = false)
    {
        var d = new Dictionary<T1, T2>();
        foreach (var item in enumerable)
        {
            var key = item.Key;

            var c = d.ContainsKey(item.Key);
            if (c)
                if (addRandomWhenKeyExists)
                {
                    var k = key + " " + RandomHelper.RandomString(5);
                    key = (T1)(dynamic)k;
                }

            d.Add(key, item.Value);
        }

        return d;
    }


    #region For easy copy from DictionaryHelperShared.cs to SunamoExceptions

    /// <summary>
    ///     If exists index A2, set to it A3
    ///     if don't, add with A3
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="qs"></param>
    /// <param name="k"></param>
    /// <param name="v"></param>
    public static void AddOrSet<T1, T2>(IDictionary<T1, T2> qs, T1 k, T2 v)
    {
        if (qs.ContainsKey(k))
            qs[k] = v;
        else
            qs.Add(k, v);
    }

    #endregion

    #region For easy copy from DictionaryHelperShared64.cs to SunamoExceptions

    /// <summary>
    ///     Copy elements to A1 from A2
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
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

            if (add) array[i] = new KeyValuePair<T, U>(item.Key, item.Value);

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

            if (add) array[i] = new KeyValuePair<T, U>(item.Key, item.Value);

            i++;
        }
    }

    #endregion
}