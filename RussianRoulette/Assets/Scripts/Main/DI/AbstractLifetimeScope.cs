using System;
using TatRat.API;
using UnityEngine;
using VContainer;
using VContainer.Unity;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TatRat.Main.DI
{
    internal abstract class AbstractLifetimeScope<T> : LifetimeScope where T : AbstractRegistrator
    {
        [SerializeField]
        [CheckObject]
        private T[] _registrators = Array.Empty<T>();

        protected override void Configure(IContainerBuilder builder)
        {
            for (var i = 0; i < _registrators.Length; ++i)
            {
                _registrators[i].Register(builder);
            }
        }

#if UNITY_EDITOR
        protected void GatherRegistrators()
        {
            _registrators = GetComponentsInChildren<T>();
            EditorUtility.SetDirty(this);
        }
#endif
    }
}