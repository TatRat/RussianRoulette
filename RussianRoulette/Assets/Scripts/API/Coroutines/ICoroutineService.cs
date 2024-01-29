using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace TatRat.API
{
    public interface ICoroutineService : IService
    {
        void Init();
        void Dispose();
        Coroutine StartCoroutine(IEnumerator enumerator);
        void StopCoroutine(Coroutine coroutine);
        UniTask RunCoroutineAsync(IEnumerator enumerator);
    }
}