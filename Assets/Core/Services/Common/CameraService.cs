using Core.Events.EventBus;
using Core.Events.Handlers;
using Core.Services.Base;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services.Common
{
    public class CameraService : Service
    {
        [SerializeField] private Camera _camera;

        private float _currentAspectRatio;

        public override UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public override UniTask StartAsync()
        {
            UpdateAspectRatio();
            return UniTask.CompletedTask;
        }

        public override UniTask StopAsync()
        {
            return UniTask.CompletedTask;
        }
        
        private void Update()
        {
            if (Mathf.Abs(_currentAspectRatio - _camera.aspect) > 0)
            {
                UpdateAspectRatio();
            }
        }

        private void UpdateAspectRatio()
        {
            _currentAspectRatio = _camera.aspect;
            
            EventBus.Fire<IAspectChangeHandler>(h => h.OnAspectRatioChanged(_currentAspectRatio));
        }
    }
}
