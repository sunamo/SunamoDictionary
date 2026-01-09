// variables names: ok
namespace SunamoDictionary.Tests;

/// <summary>
/// Unit tests for DictionaryHelper methods.
/// </summary>
public class DictionaryHelperTests
{
    /// <summary>
    /// Tests the CountOfItems method to verify it correctly counts item occurrences.
    /// </summary>
    [Fact]
    public void CountOfItems_ShouldCountOccurrencesCorrectly()
    {
        // Arrange
        var items = new List<string> { "apple", "banana", "apple", "cherry", "banana", "apple" };

        // Act
        var result = DictionaryHelper.CountOfItems(items);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal("apple", result[0].Key);
        Assert.Equal(3, result[0].Value);
        Assert.Equal("banana", result[1].Key);
        Assert.Equal(2, result[1].Value);
        Assert.Equal("cherry", result[2].Key);
        Assert.Equal(1, result[2].Value);
    }

    /// <summary>
    /// Tests the AddOrCreate method to verify it creates new lists and adds to existing ones.
    /// </summary>
    [Fact]
    public void AddOrCreate_ShouldCreateListAndAddValue()
    {
        // Arrange
        var dictionary = new Dictionary<string, List<int>>();

        // Act
        DictionaryHelper.AddOrCreate(dictionary, "key1", 10);
        DictionaryHelper.AddOrCreate(dictionary, "key1", 20);
        DictionaryHelper.AddOrCreate(dictionary, "key2", 30);

        // Assert
        Assert.Equal(2, dictionary.Count);
        Assert.Equal(2, dictionary["key1"].Count);
        Assert.Contains(10, dictionary["key1"]);
        Assert.Contains(20, dictionary["key1"]);
        Assert.Single(dictionary["key2"]);
        Assert.Contains(30, dictionary["key2"]);
    }

    /// <summary>
    /// Tests the SwitchKeyAndValue method to verify keys and values are swapped correctly.
    /// </summary>
    [Fact]
    public void SwitchKeyAndValue_ShouldSwapKeysAndValues()
    {
        // Arrange
        var dictionary = new Dictionary<int, string>
        {
            { 1, "one" },
            { 2, "two" },
            { 3, "three" }
        };

        // Act
        var result = DictionaryHelper.SwitchKeyAndValue(dictionary);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(1, result["one"]);
        Assert.Equal(2, result["two"]);
        Assert.Equal(3, result["three"]);
    }

    /// <summary>
    /// Tests the IncrementOrCreate method to verify it increments existing values and creates new ones.
    /// </summary>
    [Fact]
    public void IncrementOrCreate_ShouldIncrementOrCreateCorrectly()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>();

        // Act
        DictionaryHelper.IncrementOrCreate(dictionary, "key1");
        DictionaryHelper.IncrementOrCreate(dictionary, "key1");
        DictionaryHelper.IncrementOrCreate(dictionary, "key2");

        // Assert
        Assert.Equal(2, dictionary.Count);
        Assert.Equal(2, dictionary["key1"]);
        Assert.Equal(1, dictionary["key2"]);
    }

    /// <summary>
    /// Tests the GetDictionaryFromTwoList method to verify it creates a dictionary from two lists.
    /// </summary>
    [Fact]
    public void GetDictionaryFromTwoList_ShouldCreateDictionaryFromLists()
    {
        // Arrange
        var keys = new List<string> { "a", "b", "c" };
        var values = new List<int> { 1, 2, 3 };

        // Act
        var result = DictionaryHelper.GetDictionaryFromTwoList(keys, values);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.Equal(1, result["a"]);
        Assert.Equal(2, result["b"]);
        Assert.Equal(3, result["c"]);
    }

    /// <summary>
    /// Tests the RemoveDuplicatedFromDictionaryByValues method.
    /// </summary>
    [Fact]
    public void RemoveDuplicatedFromDictionaryByValues_ShouldRemoveDuplicates()
    {
        // Arrange
        var dictionary = new Dictionary<string, int>
        {
            { "key1", 100 },
            { "key2", 200 },
            { "key3", 100 }, // duplicate value
            { "key4", 300 }
        };
        var duplicates = new Dictionary<string, int>();

        // Act
        var result = DictionaryHelper.RemoveDuplicatedFromDictionaryByValues(dictionary, duplicates);

        // Assert
        Assert.Equal(3, result.Count);
        Assert.True(result.ContainsKey("key1"));
        Assert.True(result.ContainsKey("key2"));
        Assert.False(result.ContainsKey("key3")); // duplicate removed
        Assert.True(result.ContainsKey("key4"));
        Assert.Single(duplicates);
        Assert.True(duplicates.ContainsKey("key3"));
    }
}
