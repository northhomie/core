using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Core.Settings
{
    [CreateAssetMenu(menuName = "Core/ApplicationSettings", fileName = "ApplicationSettings")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ApplicationSettings : ScriptableObject
    {
        public int TargetFrameRate = 60;
    }
}