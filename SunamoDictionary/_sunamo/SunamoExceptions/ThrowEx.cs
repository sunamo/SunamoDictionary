namespace SunamoDictionary._sunamo.SunamoExceptions;

internal partial class ThrowEx
{
    internal static bool HasOddNumberOfElements(string listName, ICollection list)
    {
        var validationFunction = Exceptions.HasOddNumberOfElements;
        return ThrowIsNotNull(validationFunction, listName, list);
    }

    internal static bool ThrowIsNotNull<A, B>(Func<string, A, B, string?> validationFunction, A firstArgument, B secondArgument)
    {
        string? exceptionMessage = validationFunction(FullNameOfExecutedCode(), firstArgument, secondArgument);
        return ThrowIsNotNull(exceptionMessage);
    }

    internal static bool DifferentCountInLists(string firstCollectionName, int firstCount, string secondCollectionName, int secondCount)
    {
        return ThrowIsNotNull(
            Exceptions.DifferentCountInLists(FullNameOfExecutedCode(), firstCollectionName, firstCount, secondCollectionName, secondCount));
    }



    internal static bool KeyNotFound<T, U>(IDictionary<T, U> dictionary, string dictionaryName, T key)
    { return ThrowIsNotNull(Exceptions.KeyNotFound(FullNameOfExecutedCode(), dictionary, dictionaryName, key)); }

    #region Other
    internal static string FullNameOfExecutedCode()
    {
        Tuple<string, string, string> placeOfException = Exceptions.PlaceOfException();
        string fullName = FullNameOfExecutedCode(placeOfException.Item1, placeOfException.Item2, true);
        return fullName;
    }

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

    internal static bool ThrowIsNotNull(string? exception, bool shouldReallyThrow = true)
    {
        if (exception != null)
        {
            Debugger.Break();
            if (shouldReallyThrow)
            {
                throw new Exception(exception);
            }
            return true;
        }
        return false;
    }
    #endregion
}