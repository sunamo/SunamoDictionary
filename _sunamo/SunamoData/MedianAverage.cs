namespace SunamoDictionary;


internal class MedianAverage<T>
{
    internal T median;
    internal T average;
    internal T min;
    internal T max;
    internal int count;
    public override string ToString()
    {
        return $"Count: {count}, Median: {median}, Average: {average}, Min: {min}, Max: {max}";
    }
}