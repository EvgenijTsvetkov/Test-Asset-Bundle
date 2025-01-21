using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Project.Services;
using UnityEngine;

namespace Project
{
    public class Bootstrapper : MonoBehaviour
    {
        private IFirebaseService _firebaseService;
        private UserProvider _userProvider;
        private IAuthenticationService _authenticationService;

        private void Awake()
        {
            InitializeServices();
        }

        private void InitializeServices()
        {
            _userProvider = new UserProvider();
            _firebaseService = new FirebaseService(_userProvider);
            _authenticationService = new AuthenticationService(_firebaseService);
        }

        private async void Start()
        {
            await _firebaseService.Initialize();
            
            if (_authenticationService.IsSignedIn == false) 
                await _authenticationService.SignInAnonymouslyAsync();
        }
    }
}