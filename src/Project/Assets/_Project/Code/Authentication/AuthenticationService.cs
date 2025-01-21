using Cysharp.Threading.Tasks;

namespace Project.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IFirebaseService _firebaseService;

        public AuthenticationService(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public bool IsSignedIn { get; private set; }
        
        public async UniTask SignInAnonymouslyAsync()
        {
            await _firebaseService.SignInAnonymouslyAsync();
        }
    }
}