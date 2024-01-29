using UnityEngine;
using VContainer;

namespace TatRat.Main.DI
{
    internal abstract class AbstractRegistrator : MonoBehaviour
    {
        public abstract void Register(IContainerBuilder builder);
    }
}