// variables names: ok
namespace SunamoDictionary._sunamo;

/// <summary>
/// Helper class for generating random values.
/// Provides methods for creating random strings and other random data.
/// </summary>
internal class RandomHelper
{
    /// <summary>
    /// Generates a random alphanumeric string of the specified length.
    /// </summary>
    /// <param name="length">The length of the random string to generate.</param>
    /// <returns>A random string containing uppercase letters and digits.</returns>
    internal static string RandomString(int length)
    {
        Random random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(characterSet => characterSet[random.Next(characterSet.Length)]).ToArray());
    }
}
