using Cysharp.Threading.Tasks;

namespace Project.Services
{
    public interface IAuthenticationService
    {
        bool IsSignedIn { get; }

        UniTask SignInAnonymouslyAsync();
    }
}