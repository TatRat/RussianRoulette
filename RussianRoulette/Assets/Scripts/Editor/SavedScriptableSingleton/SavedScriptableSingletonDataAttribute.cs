using System;
using UnityEngine;

namespace TatRat.Editor
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class SavedScriptableSingletonDataAttribute : Attribute
    {
        public readonly string SavePath;
        public readonly string SettingsPath;
        public readonly bool IsPreference;
        public readonly HideFlags HideFlags;

        public SavedScriptableSingletonDataAttribute(
            string savePath,
            string settingsPath,
            bool isPreference = false,
            HideFlags hideFlags = HideFlags.None)
        {
            SavePath = savePath;
            SettingsPath = settingsPath;
            IsPreference = isPreference;
            HideFlags = hideFlags;
        }
    }
}