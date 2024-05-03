using System.Collections.Generic;
using Core.Services.Base;
using Core.Settings;
using Core.Utilities;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private List<Service> _services;
        [SerializeField] private ApplicationSettings _applicationSettings;
        
        private void Start()
        {
            StartAsync().Forget();
        }

        private async UniTask StartAsync()
        {
            foreach (var service in _services)
            {
                await service.InitializeAsync();
                
                ServiceContainer.Add(service);
            }

            foreach (var service in _services)
            {
                await service.StartAsync();
            }

            ApplyApplicationSettings();
        }
        
        private void ApplyApplicationSettings()
        {
            Application.targetFrameRate = _applicationSettings.TargetFrameRate;
        }
    }
}
