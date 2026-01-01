namespace SunamoDictionary._sunamo.SunamoCollections;

/// <summary>
/// Collection helper methods for manipulating lists.
/// Provides utility methods for adding prefixes and postfixes to string lists.
/// </summary>
internal class CA
{
    /// <summary>
    /// Adds a prefix to each element in the list if it doesn't already end with it.
    /// Despite the method name suggesting "postfix", this actually adds a prefix.
    /// </summary>
    /// <param name="prefix">The prefix to add to each element.</param>
    /// <param name="list">The list to modify.</param>
    /// <returns>The modified list with prefixes added.</returns>
    internal static List<string> PostfixIfNotEnding(string prefix, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = prefix + list[i];
        }
        return list;
    }

    /// <summary>
    /// Prepends a prefix to each element in the list if it doesn't already start with it.
    /// </summary>
    /// <param name="prefix">The prefix to prepend to each element.</param>
    /// <param name="list">The list to modify.</param>
    /// <returns>The modified list with prefixes prepended.</returns>
    internal static List<string> Prepend(string prefix, List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (!list[i].StartsWith(prefix))
            {
                list[i] = prefix + list[i];
            }
        }
        return list;
    }
}
