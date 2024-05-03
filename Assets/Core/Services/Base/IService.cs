using Cysharp.Threading.Tasks;

namespace Core.Services.Base
{
    public interface IService
    {
        public UniTask InitializeAsync();
        
        public UniTask StartAsync();

        public UniTask StopAsync();
    }
}
