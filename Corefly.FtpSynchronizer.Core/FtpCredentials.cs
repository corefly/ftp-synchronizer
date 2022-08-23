namespace Corefly.FtpSynchronizer.Core;

public class FtpCredentials
{
    public Uri Host { get; }
    public int Port { get; }
    public string User { get; }
    public string Password { get; }

    public FtpCredentials(Uri host, int port, string user, string password)
    {
        Host = host;
        Port = port;
        User = user;
        Password = password;
    }
}
