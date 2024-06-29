/// <summary>
/// AddOrCreateTimeSpan - umí přidávat časové úseky jako TimeSpan
/// AddOrCreate - ve value je List, 
/// AddOrPlus - ve value je number
/// AddOrSet - va value je string
/// 
/// 
/// </summary>

namespace SunamoDictionary;
public partial class DictionaryHelper
{
    #region AddOrCreateTimeSpan
    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> sl, Key key, DateTime value)
    {
        TimeSpan ts = TimeSpan.FromTicks(value.Ticks);
        AddOrCreateTimeSpan<Key>(sl, key, ts);
    }

    public static void AddOrCreateTimeSpan<Key>(Dictionary<Key, TimeSpan> sl, Key key, TimeSpan value)
    {
        if (sl.ContainsKey(key))
        {
            sl[key] = sl[key].Add(value);
        }
        else
        {
            sl.Add(key, value);
        }
    }
    #endregion

    #region Other
    /// <summary>
    /// Přidá do nového slovníku pokud existuje
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="l"></param>
    /// <param name="item"></param>
    /// <param name="toReplace"></param>
    /// <param name="throwExIfNotContains"></param>
    public static void AddToNewDictionary<T, U>(Dictionary<T, U> l, T item, Dictionary<T, U> toReplace, bool throwExIfNotContains = true)
    {
        if (l.ContainsKey(item))
        {
            if (!toReplace.ContainsKey(item))
            {
                toReplace.Add(item, l[item]);
            }
        }
        else
        {
            if (throwExIfNotContains)
            {
                ThrowEx.KeyNotFound<T, U>(l, nameof(l), item);
            }
        }
    }



    public static int AddToIndexAndReturnIncrementedInt<T>(int i, Dictionary<int, T> colors, T colorOnWeb)
    {
        colors.Add(i, colorOnWeb);
        i++;
        return i;
    }
    #endregion

    #region AddOrCreate
    public static void AddOrCreate<T, U>(Dictionary<T, List<U>> dict, T key, U value)
    {
        if (dict.ContainsKey(key))
        {
            dict[key].Add(value);
        }
        else
        {
            var d = new List<U>();
            d.Add(value);
            dict.Add(key, d);
        }
    }

    public static List<T2> AddOrCreate<T1, T2>(Dictionary<T1, List<T2>> b64Images, T1 idApp, Func<T1, List<T2>> base64ImagesOfApp)
    {
        if (!b64Images.ContainsKey(idApp))
        {
            var r = base64ImagesOfApp(idApp);
            b64Images.Add(idApp, r);
            return r;
        }
        return b64Images[idApp];
    }

    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> sl, Key key, List<Value> values, bool withoutDuplicitiesInValue = false, Dictionary<Key, List<string>> dictS = null)
    {
        foreach (var value in values)
        {
            AddOrCreate<Key, Value, object>(sl, key, value, withoutDuplicitiesInValue, dictS);
        }
    }

    #region AddOrCreate když key i value není list
    /// <summary>
    ///     A3 is inner type of collection entries
    ///     dictS => is comparing with string
    ///     As inner must be List, not IList etc.
    ///     From outside is not possible as inner use other class based on IList
    /// </summary>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <typeparam name="ColType"></typeparam>
    /// <param name="sl"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddOrCreate<Key, Value, ColType>(IDictionary<Key, List<Value>> dict, Key key, Value value,
    bool withoutDuplicitiesInValue = false, Dictionary<Key, List<string>> dictS = null)
    {
        var compWithString = false;
        if (dictS != null) compWithString = true;

        if (key is IList && typeof(ColType) != typeof(Object))
        {
            var keyE = key as IList<ColType>;
            var contains = false;
            foreach (var item in dict)
            {
                var keyD = item.Key as IList<ColType>;
                if (keyD.SequenceEqual(keyE)) contains = true;
            }

            if (contains)
            {
                foreach (var item in dict)
                {
                    var keyD = item.Key as IList<ColType>;
                    if (keyD.SequenceEqual(keyE))
                    {
                        if (withoutDuplicitiesInValue)
                            if (item.Value.Contains(value))
                                return;
                        item.Value.Add(value);
                    }
                }
            }
            else
            {
                List<Value> ad = new();
                ad.Add(value);
                dict.Add(key, ad);

                if (compWithString)
                {
                    List<string> ad2 = new();
                    ad2.Add(value.ToString());
                    dictS.Add(key, ad2);
                }
            }
        }
        else
        {
            var add = true;
            lock (dict)
            {
                if (dict.ContainsKey(key))
                {
                    if (withoutDuplicitiesInValue)
                    {
                        if (dict[key].Contains(value))
                            add = false;
                        else if (compWithString)
                            if (dictS[key].Contains(value.ToString()))
                                add = false;
                    }

                    if (add)
                    {
                        var val = dict[key];

                        if (val != null) val.Add(value);

                        if (compWithString)
                        {
                            var val2 = dictS[key];

                            if (val != null) val2.Add(value.ToString());
                        }
                    }
                }
                else
                {
                    if (!dict.ContainsKey(key))
                    {
                        List<Value> ad = new();
                        ad.Add(value);
                        dict.Add(key, ad);
                    }
                    else
                    {
                        dict[key].Add(value);
                    }

                    if (compWithString)
                    {
                        if (!dictS.ContainsKey(key))
                        {
                            List<string> ad2 = new();
                            ad2.Add(value.ToString());
                            dictS.Add(key, ad2);
                        }
                        else
                        {
                            dictS[key].Add(value.ToString());
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
    /// <param name="sl"></param>
    /// <param name="key"></param>
    /// <param name="p"></param>
    public static void AddOrCreate<Key, Value>(IDictionary<Key, List<Value>> sl, Key key, Value value,
    bool withoutDuplicitiesInValue = false, Dictionary<Key, List<string>> dictS = null)
    {
        AddOrCreate<Key, Value, object>(sl, key, value, withoutDuplicitiesInValue, dictS);
    }
    #endregion
    #endregion

    #region AddOrPlus
    public static void AddOrPlus<T>(Dictionary<T, int> sl, T key, int p)
    {
        if (sl.ContainsKey(key))
            sl[key] += p;
        else
            sl.Add(key, p);
    }

    public static void AddOrPlus<T>(Dictionary<T, long> sl, T key, long p)
    {
        if (sl.ContainsKey(key))
            sl[key] += p;
        else
            sl.Add(key, p);
    }
    #endregion

    [Obsolete("Vůbec nechápu smysl této metody")]
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

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="qs"></param>
    /// <param name="k"></param>
    /// <param name="i"></param>
    /// <returns></returns>
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

    //public static void AddOrSet(Dictionary<string, string> qs, string k, string v)
    //{
    //    if (qs.ContainsKey(k))
    //    {
    //        qs[k] = v;
    //    }
    //    else
    //    {
    //        qs.Add(k, v);
    //    }
    //}
}