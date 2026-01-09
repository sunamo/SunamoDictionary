// variables names: ok
namespace SunamoDictionary._sunamo.SunamoExceptions;

/// <summary>
/// Exception handling and validation helper methods.
/// Provides methods for generating exception messages and extracting stack trace information.
/// </summary>
internal sealed partial class Exceptions
{
    #region Other

    /// <summary>
    /// Adds a prefix to a message if it's not empty.
    /// </summary>
    /// <param name="before">The prefix to add.</param>
    /// <returns>The prefix with ": " appended if not empty, otherwise empty string.</returns>
    internal static string CheckBefore(string before)
    {
        return string.IsNullOrWhiteSpace(before) ? string.Empty : before + ": ";
    }

    /// <summary>
    /// Extracts the place of exception from the current stack trace.
    /// </summary>
    /// <param name="isFillingAlsoFirstTwo">Whether to fill type and method name from the trace.</param>
    /// <returns>A tuple containing type name, method name, and full stack trace text.</returns>
    internal static Tuple<string, string, string> PlaceOfException(bool isFillingAlsoFirstTwo = true)
    {
        StackTrace stackTrace = new();
        var stackTraceText = stackTrace.ToString();
        var lines = stackTraceText.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        lines.RemoveAt(0);
        var i = 0;
        string type = string.Empty;
        string methodName = string.Empty;
        for (; i < lines.Count; i++)
        {
            var item = lines[i];
            if (isFillingAlsoFirstTwo)
                if (!item.StartsWith("   at ThrowEx"))
                {
                    TypeAndMethodName(item, out type, out methodName);
                    isFillingAlsoFirstTwo = false;
                }
            if (item.StartsWith("at System."))
            {
                lines.Add(string.Empty);
                lines.Add(string.Empty);
                break;
            }
        }
        return new Tuple<string, string, string>(type, methodName, string.Join(Environment.NewLine, lines));
    }

    /// <summary>
    /// Extracts type and method name from a stack trace line.
    /// </summary>
    /// <param name="line">The stack trace line to parse.</param>
    /// <param name="type">Output parameter for the type name.</param>
    /// <param name="methodName">Output parameter for the method name.</param>
    internal static void TypeAndMethodName(string line, out string type, out string methodName)
    {
        var methodPart = line.Split("at ")[1].Trim();
        var text = methodPart.Split("(")[0];
        var parts = text.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        methodName = parts[^1];
        parts.RemoveAt(parts.Count - 1);
        type = string.Join(".", parts);
    }

    /// <summary>
    /// Gets the name of the calling method at the specified depth in the call stack.
    /// </summary>
    /// <param name="depth">The depth in the call stack (default is 1).</param>
    /// <returns>The method name, or an error message if it cannot be retrieved.</returns>
    internal static string CallingMethod(int depth = 1)
    {
        StackTrace stackTrace = new();
        var methodBase = stackTrace.GetFrame(depth)?.GetMethod();
        if (methodBase == null)
        {
            return "Method name cannot be get";
        }
        var methodName = methodBase.Name;
        return methodName;
    }
    #endregion

    #region IsNullOrWhitespace

    /// <summary>
    /// Internal StringBuilder for storing additional exception information.
    /// </summary>
    internal readonly static StringBuilder AdditionalInfoInnerStringBuilder = new();

    /// <summary>
    /// Internal StringBuilder for storing additional exception information.
    /// </summary>
    internal readonly static StringBuilder AdditionalInfoStringBuilder = new();
    #endregion

    /// <summary>
    /// Validates that a collection has an even number of elements.
    /// </summary>
    /// <param name="before">Prefix to add to the error message.</param>
    /// <param name="listName">The name of the list being validated.</param>
    /// <param name="list">The collection to validate.</param>
    /// <returns>An error message if the collection has odd number of elements, otherwise null.</returns>
    internal static string? HasOddNumberOfElements(string before, string listName, ICollection list)
    {
        return list.Count % 2 == 1 ? CheckBefore(before) + listName + " has odd number of elements " + list.Count : null;
    }

    /// <summary>
    /// Validates that two collections have the same count.
    /// </summary>
    /// <param name="before">Prefix to add to the error message.</param>
    /// <param name="firstCollectionName">The name of the first collection.</param>
    /// <param name="firstCount">The count of the first collection.</param>
    /// <param name="secondCollectionName">The name of the second collection.</param>
    /// <param name="secondCount">The count of the second collection.</param>
    /// <returns>An error message if counts differ, otherwise null.</returns>
    internal static string? DifferentCountInLists(string before, string firstCollectionName, int firstCount, string secondCollectionName, int secondCount)
    {
        if (firstCount != secondCount)
            return CheckBefore(before) + " different count elements in collection" + " " +
            string.Concat(firstCollectionName + "-" + firstCount) + " vs. " +
            string.Concat(secondCollectionName + "-" + secondCount);
        return null;
    }

    /// <summary>
    /// Validates that a key exists in a dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of the dictionary value.</typeparam>
    /// <param name="before">Prefix to add to the error message.</param>
    /// <param name="dictionary">The dictionary to check.</param>
    /// <param name="dictionaryName">The name of the dictionary.</param>
    /// <param name="key">The key to look for.</param>
    /// <returns>An error message if key is not found, otherwise null.</returns>
    internal static string? KeyNotFound<T, U>(string before, IDictionary<T, U> dictionary, string dictionaryName, T key)
    {
        return !dictionary.ContainsKey(key)
        ? CheckBefore(before) + key + " is not exists in dictionary" + " " + dictionaryName
        : null;
    }
}
