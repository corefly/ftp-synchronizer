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
        await _client.UploadDirectoryAsync(_localDirectory, _ftpDirectory, FtpFolderSyncMode.Mirror);
    }
}
