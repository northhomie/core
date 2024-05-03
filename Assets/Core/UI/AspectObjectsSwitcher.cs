using System.Collections.Generic;
using Core.Events.Handlers;
using UnityEngine;
using EventBus = Core.Events.EventBus.EventBus;

namespace Core.UI
{
    public class AspectObjectsSwitcher : MonoBehaviour, IAspectChangeHandler
    {
        [Tooltip("Objects groups which depends on the different aspect ratio")]
        [SerializeField] private List<AspectObjectsGroup> _objectsGroups;

        private void OnEnable()
        {
            EventBus.Subscribe(this);
        }

        private void OnDisable()
        {
            EventBus.Unsubscribe(this);
        }

        public void OnAspectRatioChanged(float aspectRatio)
        {
            print("CALL");
            foreach (var objectsGroup in _objectsGroups)
            {
                objectsGroup.LowerAspectRatioObjects.ForEach(o => o.SetActive(aspectRatio < objectsGroup.AspectRatioBorder));
                objectsGroup.HigherAspectRatioObjects.ForEach(o => o.SetActive(aspectRatio >= objectsGroup.AspectRatioBorder));
            }
        }
    }
}

