namespace Corefly.FtpSynchronizer.Core.Utils;

internal static class PathUtil
{
    public static string GetRelativePath(string rootPath, string fullPath)
    {
        rootPath = NormalizeFilePath(rootPath);
        fullPath = NormalizeFilePath(fullPath);

        if (!fullPath.StartsWith(rootPath)) throw new InvalidCastException();

        return fullPath[rootPath.Length..];
    }

    private static string NormalizeFilePath(string filepath)
    {
        var result = Path.GetFullPath(filepath).ToLowerInvariant();

        result = result.TrimEnd(new[] { '\\' });

        return result;
    }
}
