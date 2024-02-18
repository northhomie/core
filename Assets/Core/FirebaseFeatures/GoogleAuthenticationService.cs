using System;
using System.Threading.Tasks;
using Core.Managers;
using Firebase.Auth;
using Google;
using UnityEngine;

namespace Core.FirebaseFeatures
{
    public class GoogleAuthenticationService : MonoBehaviour
    {
        private const string GoogleWebAPI = "255741655223-81vtdtbuoh7c1a40t76qhhhe8kkoadpr.apps.googleusercontent.com";
        
        public static GoogleAuthenticationService Instance { get; private set; }

        private GoogleSignInConfiguration _signInConfiguration;
        private FirebaseAuth _auth;
        private FirebaseUser _user;
        private bool _isAvailable;

        private void Awake()
        {
            Instance ??= this;
            _auth = FirebaseAuth.DefaultInstance;
            _auth.StateChanged += OnAuthStateChanged;

            _signInConfiguration = new GoogleSignInConfiguration
            {
                WebClientId = GoogleWebAPI,
                RequestIdToken = true
            };
        }
        
        private void OnDestroy()
        {
            _auth.StateChanged -= OnAuthStateChanged;
            _auth = null;
        }

        private void OnEnable()
        {
            FirebaseManager.Instance.AppInitializeComplete += OnAppInitializeCompleted;
            FirebaseManager.Instance.AppInitializeFail += OnAppInitializeFailed;
        }

        private void OnDisable()
        {
            FirebaseManager.Instance.AppInitializeComplete -= OnAppInitializeCompleted;
            FirebaseManager.Instance.AppInitializeFail -= OnAppInitializeFailed;
        }
        
        public void SignInUser()
        {
            if (!_isAvailable)
            {
                Debug.Log("Google Authentication Service is unavailable!");
                return;
            }
                
            GoogleSignIn.Configuration = _signInConfiguration;
            GoogleSignIn.Configuration.UseGameSignIn = false;
            GoogleSignIn.Configuration.RequestIdToken = true;
            GoogleSignIn.Configuration.RequestEmail = true;

            GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticationFinished);
        }

        public void SignOut()
        {
            if (!_isAvailable)
                return;
            
            _auth.SignOut();
        }

        private void OnAppInitializeCompleted()
        {
            _isAvailable = true;
            OnAuthStateChanged(this, null);
        }

        private void OnAppInitializeFailed()
        {
            _isAvailable = false;
        }

        private void OnGoogleAuthenticationFinished(Task<GoogleSignInUser> task)
        {
            var credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
            _auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWith(authTask => {
                if (authTask.IsCanceled) {
                    Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                    return;
                }
                if (authTask.IsFaulted) {
                    Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + authTask.Exception);
                    return;
                }

                var result = authTask.Result;
                _user = result.User;
                Debug.LogFormat("User signed in successfully: {0} ({1})", _user.DisplayName, _user.UserId);
            });
        }
        
        private void OnAuthStateChanged(object sender, EventArgs args)
        {
            if (_auth.CurrentUser == _user) 
                return;
            
            var signedIn = _user != _auth.CurrentUser && _auth.CurrentUser != null
                                                      && _auth.CurrentUser.IsValid();
            if (!signedIn && _user != null) {
                Debug.Log("Signed out " + _user.UserId);
            }
            _user = _auth.CurrentUser;
                
            if (signedIn) {
                Debug.Log("Signed in " + _user.UserId);
                    
                // Override new user's data here
            }
        }
    }
}
