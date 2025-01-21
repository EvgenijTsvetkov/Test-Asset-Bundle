using Cysharp.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using Firebase.Extensions;
using UnityEngine;

namespace Project.Services
{
    public class FirebaseService : IFirebaseService
    {
        private FirebaseAuth _auth;
        private readonly UserProvider _userProvider;

        public FirebaseService(UserProvider userProvider)
        {
            _userProvider = userProvider;   
        }
        
        public async UniTask Initialize()
        {
            await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                if (task.Result == DependencyStatus.Available)
                {
                    Debug.Log("<b>[FirebaseService]</b> Firebase correctly Initialized.");
                    
                    _auth = FirebaseAuth.DefaultInstance;
                }
                
                else
                    Debug.Log($"<b>[FirebaseService]</b> Could not resolve all Firebase dependencies: {task.Result}.");
            });
        }

        public async UniTask SignInAnonymouslyAsync()
        {
            if (_auth == null)
            {
                Debug.LogError("<b>[FirebaseService]</b> Firebase not Initialized.");
                return;
            }
            
            await _auth.SignInAnonymouslyAsync().ContinueWith(task =>
            {
                if (task.IsCompleted && !task.IsFaulted && !task.IsCanceled)
                {
                    _userProvider.Value = task.Result.User;

                    Debug.LogFormat("<b>[FirebaseService]</b> User signed in successfully with Id: {0}.", _userProvider.Value.UserId);
                }
                else
                    Debug.LogError($"<b>[FirebaseService]</b> Failed to sign in: {task.Exception}.");
            });
        }
    }
}