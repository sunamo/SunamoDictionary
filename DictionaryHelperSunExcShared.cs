namespace SunamoDictionary;


public partial class DictionaryHelper
{
    #region For easy copy from DictionaryHelperShared.cs to SunamoExceptions
    /// <summary>
    /// If exists index A2, set to it A3
    /// if don't, add with A3
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="qs"></param>
    /// <param name="k"></param>
    /// <param name="v"></param>
    public static void AddOrSet<T1, T2>(IDictionary<T1, T2> qs, T1 k, T2 v)
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
    #endregion
}
