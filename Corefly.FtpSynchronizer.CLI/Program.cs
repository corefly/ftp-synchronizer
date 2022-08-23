using Corefly.FtpSynchronizer.Core;

namespace Corefly.FtpSynchronizer.CLI;

internal class Program
{
    static async Task Main(string[] args)
    {
        var ftpCredentials = new FtpCredentials(new Uri("ftp://192.168.1.1"), 21, "ftptest", "ftptest");
        using var ftpSynchronizer = new Core.FtpSynchronizer(ftpCredentials, @"D:\source", "/");

        await ftpSynchronizer.Sync();
    }
}
