internal class MyBackgroundService : BackgroundService
{
    private long _counter = 0;
    private readonly bool _isBlocking;

    public MyBackgroundService(bool isBlocking)
    {
        _isBlocking = isBlocking;
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        if (_isBlocking)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await DoSomethingAsync();
            }
        }
        else
        {
            await Task.Factory.StartNew(async () =>
            {
                await this.ExecuteAsyncCore(cancellationToken).ConfigureAwait(false);
            }, cancellationToken);
        }
    }

    private async Task ExecuteAsyncCore(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            await DoSomethingAsync();
        }
    }

    private Task DoSomethingAsync()
    {
        if (_counter == long.MaxValue) _counter = 0;
        _counter++;
        return Task.CompletedTask;
    }

    public long Counter => _counter;
}