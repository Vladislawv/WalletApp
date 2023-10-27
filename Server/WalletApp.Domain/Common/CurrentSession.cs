namespace WalletApp.Domain.Common;

public class CurrentSession
{
    private static readonly AsyncLocal<string> _sessionId = new();

    public static string SessionId
    {
        get => _sessionId.Value;
        private set => _sessionId.Value = value;
    }

    public static IDisposable SetSessionId(string sessionId)
    {
        SessionId = sessionId;
        return new DisposableAction(() => SessionId = string.Empty);
    }
}