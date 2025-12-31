namespace SunamoDictionary;

public partial class DictionaryHelper
{
    public static void AppendLineOrCreate<T>(Dictionary<T, StringBuilder> dictionary, T key, string text)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].AppendLine(text);
        }
        else
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(text);
            dictionary.Add(key, stringBuilder);
        }
    }
}