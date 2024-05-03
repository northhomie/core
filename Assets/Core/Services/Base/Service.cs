using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core.Services.Base
{
    public abstract class Service : MonoBehaviour, IService
    {
        public virtual UniTask InitializeAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask StartAsync()
        {
            return UniTask.CompletedTask;
        }

        public virtual UniTask StopAsync()
        {
            return UniTask.CompletedTask;
        }
    }
}