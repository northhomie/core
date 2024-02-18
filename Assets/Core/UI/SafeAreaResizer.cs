using Core.Managers;
using UnityEngine;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaResizer : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private void OnEnable()
        {
            CameraManager.Instance.AspectRatioChange += OnAspectRatioChanged;
        }

        private void OnDisable()
        {
            CameraManager.Instance.AspectRatioChange -= OnAspectRatioChanged;
        }

        private void OnAspectRatioChanged(float currentAspectRatio)
        {
            var safeArea = Screen.safeArea;
            
            var anchorMin = safeArea.position;
            var anchorMax = safeArea.position + safeArea.size;
            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;
            
            _rectTransform.anchorMin = anchorMin;
            _rectTransform.anchorMax = anchorMax;
        }
    }
}
