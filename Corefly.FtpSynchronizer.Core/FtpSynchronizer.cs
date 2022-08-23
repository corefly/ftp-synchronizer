using Corefly.FtpSynchronizer.Core.Utils;
using FluentFTP;

namespace Corefly.FtpSynchronizer.Core;

public class FtpSynchronizer : IDisposable
{
    private readonly FtpClient _client;
    private readonly string _localDirectory;
    private readonly string _ftpDirectory;

    public FtpSynchronizer(FtpCredentials credentials, string localDirectory, string ftpDirectory)
    {
        _client = new FtpClient(credentials.Host, credentials.Port, credentials.User, credentials.Password);
        _client.Connect();

        _localDirectory = localDirectory;
        _ftpDirectory = ftpDirectory;

        Directory.CreateDirectory(_localDirectory);
    }

    public async Task Sync()
    {
        await SyncDirectory();
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    private async Task SyncDirectory()
    {
        foreach (var file in Directory.EnumerateFiles(_localDirectory, "*.*", SearchOption.AllDirectories))
        {
            await SyncFile(file);
        }
    }

    private async Task SyncFile(string file)
    {
        var relativeFilePath = PathUtil.GetRelativePath(_localDirectory, file);
        var remotePath = Path.Combine(_ftpDirectory, relativeFilePath);
        var compareResult = await _client.CompareFileAsync(file, remotePath);

        if (compareResult is FtpCompareResult.NotEqual or FtpCompareResult.FileNotExisting)
        {
            var status = await _client.UploadFileAsync(file, remotePath, FtpRemoteExists.Overwrite, true);

            if (status == FtpStatus.Success)
            {
                Console.WriteLine($"Copied: {file}");
            }
        }
    }
}
