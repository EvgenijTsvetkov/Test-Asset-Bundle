using System.Threading;

namespace Project.Extensions
{
    public static class AsyncOperationExtensions
    {
        public static void TryCancellationToken(this CancellationTokenSource tokenSource)
        {
            if (tokenSource.IsCancellationRequested == false)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
            }
        }
    }
}