using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Core.UI
{
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public struct AspectObjectsGroup
    {
        [Tooltip("This field is just for visualisation in editor")]
        public string GroupName;
        
        [Tooltip("Aspect's boundary at which objects switch")]
        public float AspectRatioBorder;

        [Tooltip("Objects that will be active while Camera's aspect is lower than border")]
        public List<GameObject> LowerAspectRatioObjects;

        [Tooltip("Objects that will be active while Camera's aspect is higher than border")]
        public List<GameObject> HigherAspectRatioObjects;
    }
}
