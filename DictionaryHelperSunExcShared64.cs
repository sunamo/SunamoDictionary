namespace SunamoDictionary;


public partial class DictionaryHelper
{
    #region For easy copy from DictionaryHelperShared64.cs to SunamoExceptions
    /// <summary>
    /// Copy elements to A1 from A2
    /// </summary>
    /// <param name="array"></param>
    /// <param name="arrayIndex"></param>
    public static void CopyTo<T, U>(Dictionary<T, U> _d, KeyValuePair<T, U>[] array, int arrayIndex)
    {
        array = new KeyValuePair<T, U>[_d.Count - arrayIndex + 1];

        int i = 0;
        bool add = false;
        foreach (var item in _d)
        {
            if (i == arrayIndex && !add)
            {
                add = true;
                i = 0;
            }

            if (add)
            {
                array[i] = new KeyValuePair<T, U>(item.Key, item.Value);
            }

            i++;
        }
    }

    public static void CopyTo<T, U>(List<KeyValuePair<T, U>> _d, KeyValuePair<T, U>[] array, int arrayIndex)
    {
        array = new KeyValuePair<T, U>[_d.Count - arrayIndex + 1];

        int i = 0;
        bool add = false;
        foreach (var item in _d)
        {
            if (i == arrayIndex && !add)
            {
                add = true;
                i = 0;
            }

            if (add)
            {
                array[i] = new KeyValuePair<T, U>(item.Key, item.Value);
            }

            i++;
        }
    }

    #endregion
}
