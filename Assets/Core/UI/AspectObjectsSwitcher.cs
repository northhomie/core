using System.Collections.Generic;
using Core.Managers;
using UnityEngine;

namespace Core.UI
{
    public class AspectObjectsSwitcher : MonoBehaviour
    {
        [Tooltip("Objects groups which depends on the different aspect ratio")]
        [SerializeField] private List<AspectObjectsGroup> _objectsGroups;

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
            foreach (var objectsGroup in _objectsGroups)
            {
                objectsGroup.LowerAspectRatioObjects.ForEach(o => o.SetActive(currentAspectRatio < objectsGroup.AspectRatioBorder));
                objectsGroup.HigherAspectRatioObjects.ForEach(o => o.SetActive(currentAspectRatio >= objectsGroup.AspectRatioBorder));
            }
        }
    }
}

