using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace TatRat.Editor
{
    public abstract class SavedScriptableSingleton<T> : ScriptableObject
        where T : ScriptableObject
    {
        private static T _instance;
        private const HideFlags DEFAULT_HIDE_FLAGS = HideFlags.None;

        public static T Instance =>
            _instance != null ? _instance : _instance = CreateOrLoadInstance();

        private static T CreateOrLoadInstance()
        {
            var singletonPath = GetSingletonPath();
            if (string.IsNullOrWhiteSpace(singletonPath))
            {
                throw new SystemException($"Singleton path of {typeof(T).FullName} is empty");
            }

            if (File.Exists(singletonPath))
            {
                return InternalEditorUtility.LoadSerializedFileAndForget(singletonPath)[0] as T;
            }
            else
            {
                var newInstance = CreateInstance<T>();
                newInstance.hideFlags = GetSingletonHideFlags();
                return newInstance;
            }
        }

        protected static string GetSingletonPath()
        {
            var singletonType = typeof(T);

            if (!Attribute.IsDefined(singletonType, typeof(SavedScriptableSingletonDataAttribute)))
            {
                throw new SystemException(
                    $"{nameof(SavedScriptableSingletonDataAttribute)} is not defined at {singletonType.FullName} but expected");
            }

            var singletonDataAttr = singletonType.GetCustomAttribute<SavedScriptableSingletonDataAttribute>();
            if (string.IsNullOrWhiteSpace(singletonDataAttr.SavePath))
            {
                throw new SystemException($"Singleton save path of {singletonType.FullName} is empty");
            }

            return singletonDataAttr.IsPreference
                ? Path.Combine(InternalEditorUtility.unityPreferencesFolder, singletonDataAttr.SavePath)
                : singletonDataAttr.SavePath;
        }

        protected static HideFlags GetSingletonHideFlags()
        {
            var singletonType = typeof(T);

            if (!Attribute.IsDefined(singletonType, typeof(SavedScriptableSingletonDataAttribute)))
            {
                throw new SystemException(
                    $"{nameof(SavedScriptableSingletonDataAttribute)} is not defined at {singletonType.FullName} but expected");
            }

            return singletonType.GetCustomAttribute<SavedScriptableSingletonDataAttribute>().HideFlags;
        }

        protected static SettingsProvider GetSingletonSettingsProvider()
        {
            var singletonType = typeof(T);

            if (!Attribute.IsDefined(singletonType, typeof(SavedScriptableSingletonDataAttribute)))
            {
                throw new SystemException(
                    $"{nameof(SavedScriptableSingletonDataAttribute)} is not defined at {singletonType.FullName} but expected");
            }

            var singletonDataAttr = singletonType.GetCustomAttribute<SavedScriptableSingletonDataAttribute>();
            if (string.IsNullOrWhiteSpace(singletonDataAttr.SettingsPath))
            {
                throw new SystemException($"Singleton settings path of {singletonType.FullName} is empty");
            }

            if (Instance == null)
            {
                throw new SystemException($"Instance of {singletonType.FullName} is null");
            }

            var keywords = SettingsProvider.GetSearchKeywordsFromSerializedObject(new SerializedObject(Instance));
            var settingsPath = singletonDataAttr.SettingsPath;
            //  Settings provider will be shown at ProjectSettings only. See AssetSettingsProvider constructor
            var provider = AssetSettingsProvider.CreateProviderFromObject(settingsPath, Instance, keywords);
            provider.deactivateHandler += Save;

            return provider;
        }

        protected static void Save()
        {
            if (Instance == null)
            {
                throw new NullReferenceException($"Instance of {typeof(T).FullName} is null");
            }

            var singletonPath = GetSingletonPath();
            if (string.IsNullOrWhiteSpace(singletonPath))
            {
                throw new SystemException($"Singleton path of {typeof(T).FullName} is empty");
            }

            var folder = Path.GetDirectoryName(singletonPath);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            InternalEditorUtility.SaveToSerializedFileAndForget(new[] { _instance }, singletonPath, true);
        }
    }
}