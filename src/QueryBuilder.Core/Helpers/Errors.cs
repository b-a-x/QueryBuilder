using System.Runtime.CompilerServices;

namespace QueryBuilder.Core.Helpers;

public static class Errors
{
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void CantFormatToString<T>(T value) where T : struct
    {
        throw new Exception($"Can't format '{value}' to string");
    }
}
