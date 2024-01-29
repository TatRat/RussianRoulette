using Cysharp.Threading.Tasks;

namespace TatRat.API
{
    public interface ISceneLoadService : IService
    {
        UniTask LoadSceneAsync(int sceneIndex);
        UniTask LoadSceneAsync(string sceneName);
    }
}