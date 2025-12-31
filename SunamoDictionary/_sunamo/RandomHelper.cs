namespace SunamoDictionary._sunamo;

internal class RandomHelper
{
    internal static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(charSet => charSet[random.Next(charSet.Length)]).ToArray());
    }
}