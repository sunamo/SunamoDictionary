namespace SunamoDictionary._sunamo.SunamoExceptions;

/// <summary>
/// Exception throwing helper methods.
/// Provides methods for throwing exceptions based on validation results.
/// </summary>
internal partial class ThrowEx
{
    /// <summary>
    /// Throws an exception if a collection has an odd number of elements.
    /// </summary>
    /// <param name="listName">The name of the list being validated.</param>
    /// <param name="list">The collection to validate.</param>
    /// <returns>True if validation failed and exception was thrown, false otherwise.</returns>
    internal static bool HasOddNumberOfElements(string listName, ICollection list)
    {
        var validationFunction = Exceptions.HasOddNumberOfElements;
        return ThrowIsNotNull(validationFunction, listName, list);
    }

    /// <summary>
    /// Throws an exception if the validation function returns a non-null error message.
    /// </summary>
    /// <typeparam name="A">The type of the first argument.</typeparam>
    /// <typeparam name="B">The type of the second argument.</typeparam>
    /// <param name="validationFunction">The function that validates the arguments.</param>
    /// <param name="firstArgument">The first argument to pass to validation.</param>
    /// <param name="secondArgument">The second argument to pass to validation.</param>
    /// <returns>True if validation failed and exception was thrown, false otherwise.</returns>
    internal static bool ThrowIsNotNull<A, B>(Func<string, A, B, string?> validationFunction, A firstArgument, B secondArgument)
    {
        string? exceptionMessage = validationFunction(FullNameOfExecutedCode(), firstArgument, secondArgument);
        return ThrowIsNotNull(exceptionMessage);
    }

    /// <summary>
    /// Throws an exception if two collections have different counts.
    /// </summary>
    /// <param name="firstCollectionName">The name of the first collection.</param>
    /// <param name="firstCount">The count of the first collection.</param>
    /// <param name="secondCollectionName">The name of the second collection.</param>
    /// <param name="secondCount">The count of the second collection.</param>
    /// <returns>True if validation failed and exception was thrown, false otherwise.</returns>
    internal static bool DifferentCountInLists(string firstCollectionName, int firstCount, string secondCollectionName, int secondCount)
    {
        return ThrowIsNotNull(
            Exceptions.DifferentCountInLists(FullNameOfExecutedCode(), firstCollectionName, firstCount, secondCollectionName, secondCount));
    }

    /// <summary>
    /// Throws an exception if a key is not found in the dictionary.
    /// </summary>
    /// <typeparam name="T">The type of the dictionary key.</typeparam>
    /// <typeparam name="U">The type of the dictionary value.</typeparam>
    /// <param name="dictionary">The dictionary to check.</param>
    /// <param name="dictionaryName">The name of the dictionary.</param>
    /// <param name="key">The key to look for.</param>
    /// <returns>True if validation failed and exception was thrown, false otherwise.</returns>
    internal static bool KeyNotFound<T, U>(IDictionary<T, U> dictionary, string dictionaryName, T key)
    {
        return ThrowIsNotNull(Exceptions.KeyNotFound(FullNameOfExecutedCode(), dictionary, dictionaryName, key));
    }

    #region Other

    /// <summary>
    /// Gets the full name of the currently executed code location.
    /// </summary>
    /// <returns>A string in the format "TypeName.MethodName".</returns>
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

    /// <summary>
    /// Builds the full name of executed code from type and method information.
    /// </summary>
    /// <param name="type">The type information (Type, MethodBase, or string).</param>
    /// <param name="methodName">The method name.</param>
    /// <param name="isFromThrowEx">Whether this is called from ThrowEx methods.</param>
    /// <returns>A string in the format "TypeName.MethodName".</returns>
    static string FullNameOfExecutedCode(object type, string methodName, bool isFromThrowEx = false)
    {
        if (methodName == null)
        {
            int depth = 2;
            if (isFromThrowEx)
            {
                depth++;
            }

            methodName = Exceptions.CallingMethod(depth);
        }
        string typeFullName;
        if (type is Type typeInstance)
        {
            typeFullName = typeInstance.FullName ?? "Type cannot be get via type is Type type2";
        }
        else if (type is MethodBase method)
        {
            typeFullName = method.ReflectedType?.FullName ?? "Type cannot be get via type is MethodBase method";
            methodName = method.Name;
        }
        else if (type is string)
        {
            typeFullName = type.ToString() ?? "Type cannot be get via type is string";
        }
        else
        {
            Type runtimeType = type.GetType();
            typeFullName = runtimeType.FullName ?? "Type cannot be get via type.GetType()";
        }
        return string.Concat(typeFullName, ".", methodName);
    }

    /// <summary>
    /// Throws an exception if the exception message is not null.
    /// </summary>
    /// <param name="exception">The exception message to check.</param>
    /// <param name="isReallyThrowing">Whether to actually throw the exception (default true).</param>
    /// <returns>True if exception message was not null, false otherwise.</returns>
    internal static bool ThrowIsNotNull(string? exception, bool isReallyThrowing = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (isReallyThrowing)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }
    #endregion
}
