namespace WalletApp.Domain.Common;

public class DisposableAction : IDisposable
{
    private Action _dispose;

    public DisposableAction(Action dispose)
    {
        ArgumentNullException.ThrowIfNull(dispose);
        _dispose = dispose;
    }

    public void Dispose()
    {
        _dispose();
        _dispose = null;
    }
}