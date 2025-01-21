using Cysharp.Threading.Tasks;
using Project.Services;
using Project.Ui;
using UnityEngine;

namespace Project
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private UiProvider _uiProvider;
        
        private IFirebaseService _firebaseService;
        private UserProvider _userProvider;
        private IAuthenticationService _authenticationService;
        private RemoteConfig _remoteConfig;
        private AssetProvider _assetProvider;

        private const float DelayForInitializeSystems = 1.5f;
        
        private void Awake()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
            _userProvider = new UserProvider();
            _assetProvider = new AssetProvider();
            _firebaseService = new FirebaseService(_userProvider);
            _authenticationService = new AuthenticationService(_firebaseService);
            _remoteConfig = new RemoteConfig();

            _uiProvider.MainView.SetDependency(_assetProvider, _uiProvider, _remoteConfig);
        }

        private async void Start()
        {
            _uiProvider.LoadingView.SetDuration(DelayForInitializeSystems);
            _uiProvider.LoadingView.Show();
            
            await _firebaseService.Initialize();
            await _remoteConfig.UpdateConfigs();
            await _assetProvider.LoadBundle();
                
            await UniTask.WaitUntil(() => _remoteConfig.IsLoaded);
            
            if (_authenticationService.IsSignedIn == false) 
                await _authenticationService.SignInAnonymouslyAsync();

            await UniTask.WaitForSeconds(DelayForInitializeSystems);
            
            _uiProvider.LoadingView.Hide();
            
            _uiProvider.MainView.UpdateDisplay();
            _uiProvider.MainView.Show();
        }
    }
}