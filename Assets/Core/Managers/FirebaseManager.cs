using UnityEngine;
using Firebase;
using UnityEngine.Events;

namespace Core.Managers
{
    public class FirebaseManager : MonoBehaviour
    {
        public static FirebaseManager Instance { get; private set; }
        public FirebaseApp App { get; private set; }
        
        public event UnityAction AppInitializeComplete;
        public event UnityAction AppInitializeFail;
        
        private void Awake()
        {
            Instance ??= this;
        }

        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available) {
                    App = FirebaseApp.DefaultInstance;
                    AppInitializeComplete?.Invoke();
                } else {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                    AppInitializeFail?.Invoke();
                    
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
    }
}
