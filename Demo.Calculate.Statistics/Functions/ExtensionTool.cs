namespace Demo.Calculate.Statistics.Functions;
internal static class ExtensionTool
{
    internal static T AddAll<T>(params T[] elements) where T : IMonoid<T>
    {
        var result = T.Zero;
        foreach (var element in elements)
        {
            result += element;
        }
        return result;
    }

    //整數&小數點通用
    public static T Calc<T>(T x, T y) where T : INumber<T> => x + y;

    public static float Median(float[] arraies)
    {
        Array.Sort(arraies);
        if (arraies.Length is 0) return default;
        if (arraies.Length % 2 is 0) return (arraies[arraies.Length / 2] + arraies[arraies.Length / 2 - 1]) / 2;
        return arraies[arraies.Length / 2];
    }
}