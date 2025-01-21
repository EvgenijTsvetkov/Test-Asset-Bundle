using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Project.Services
{
    public interface IFirebaseService
    {
        UniTask Initialize();
        UniTask SignInAnonymouslyAsync();
    }
}