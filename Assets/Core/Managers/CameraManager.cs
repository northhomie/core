using UnityEngine;
using UnityEngine.Events;

namespace Core.Managers
{
    public class CameraManager : MonoBehaviour
    {
        public static CameraManager Instance { get; private set; }
        
        public event UnityAction<float> AspectRatioChange;

        [SerializeField] private Camera _camera;

        private float _currentAspectRatio;
        
        private void Awake()
        {
            Instance ??= this;
        }

        private void Start()
        {
            UpdateAspectRatio();
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
            AspectRatioChange?.Invoke(_currentAspectRatio);
        }
    }
}
