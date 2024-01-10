namespace SunamoDictionary;


public partial class DictionaryHelper
{
    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList<T1, T2>(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIOrderedEnumerable2<T1, T2>(IOrderedEnumerable<KeyValuePair<T1, T2>> orderedEnumerable)
    {
        return GetDictionaryFromIList<T1, T2>(orderedEnumerable.ToList());
    }

    public static Dictionary<T1, T2> GetDictionaryFromIList<T1, T2>(List<KeyValuePair<T1, T2>> enumerable, bool addRandomWhenKeyExists = false)
    {
        Dictionary<T1, T2> d = new Dictionary<T1, T2>();
        foreach (var item in enumerable)
        {
            var key = item.Key;

            var c = d.ContainsKey(item.Key);
            if (c)
            {
                if (addRandomWhenKeyExists)
                {
                    var k = key.ToString() + " " + RandomHelper.RandomString(5);
                    key = (T1)(dynamic)k;
                }
            }
            d.Add(key, item.Value);
        }
        return d;
    }

    /// <summary>
    /// In addition to method AddOrCreate, more is checking whether value in collection does not exists
    /// </summary>
    /// <typeparam name = "Key"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <param name = "sl"></param>
    /// <param name = "key"></param>
    /// <param name = "value"></param>
    public static void AddOrCreateIfDontExists<Key, Value>(Dictionary<Key, List<Value>> sl, Key key, Value value)
    {
        if (sl.ContainsKey(key))
        {
            if (!sl[key].Contains(value))
            {
                sl[key].Add(value);
            }
        }
        else
        {
            List<Value> ad = new List<Value>();
            ad.Add(value);
            sl.Add(key, ad);
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
}
