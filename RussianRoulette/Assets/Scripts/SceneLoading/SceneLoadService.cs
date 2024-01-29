using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using TatRat.API;
using UnityEngine.SceneManagement;

namespace TatRat.SceneLoading
{
    [UsedImplicitly]
    public sealed class SceneLoadService : ISceneLoadService
    {
        private readonly ILogService _logService;

        public SceneLoadService(ILogService logService)
        {
            _logService = logService;
        }

        async UniTask ISceneLoadService.LoadSceneAsync(int sceneIndex)
        {
            _logService.Warn($"Loading scene with index {sceneIndex}");
            await SceneManager.LoadSceneAsync(sceneIndex);
        }

        public async UniTask LoadSceneAsync(string sceneName)
        {
            _logService.Warn($"Loading scene {sceneName}");
            await SceneManager.LoadSceneAsync(sceneName);
        }
    }
}