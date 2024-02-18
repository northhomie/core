using Core.Managers;
using Firebase.Auth;
using UnityEngine;

namespace Core.FirebaseFeatures
{
    public class EmailAuthenticationService : MonoBehaviour
    {
        public static EmailAuthenticationService Instance { get; private set; }

        private FirebaseAuth _auth;
        private FirebaseUser _user;
        private bool _isAvailable;

        private void Awake()
        {
            Instance ??= this;
            _auth = FirebaseAuth.DefaultInstance;
            _auth.StateChanged += OnAuthStateChanged;
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

        public void SignUpUser(string email, string password)
        {
            if (_isAvailable)
            {
                _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                    if (task.IsCanceled) {
                        Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                        return;
                    }
                    if (task.IsFaulted) {
                        Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                        return;
                    }

                    // Firebase user has been created.
                    var result = task.Result;
                    _user = result.User;
                    Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                        result.User.DisplayName, result.User.UserId);
                });
            }
            else
            {
                Debug.Log("SignUpService is unavailable");
            }
        }

        public void SignInUser(string email, string password)
        {
            _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
                if (task.IsCanceled) {
                    Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                    return;
                }
                if (task.IsFaulted) {
                    Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                    return;
                }

                var result = task.Result;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);
            });
        }

        public void SignOut()
        {
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

        private void OnAuthStateChanged(object sender, System.EventArgs eventArgs)
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
