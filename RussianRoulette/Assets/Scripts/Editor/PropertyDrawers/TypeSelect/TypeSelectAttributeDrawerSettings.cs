using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace TatRat.Editor.TypeSelect
{
    [SavedScriptableSingletonData(
        savePath: "Library/TypeSelectAttributeDrawerSettings.asset",
        settingsPath: "Preferences/TypeSelect Attribute Drawer Settings")]
    internal sealed class TypeSelectAttributeDrawerSettings :
        SavedScriptableSingleton<TypeSelectAttributeDrawerSettings>
    {
        [SerializeField]
        private TypeSelectOriginalNameModification _nameModification = TypeSelectOriginalNameModification.AfterLastDot;

        [SerializeField]
        private bool _prettifyFinalResult = true;

        public TypeSelectOriginalNameModification NameModification => _nameModification;

        public bool PrettifyFinalResult => _prettifyFinalResult;

        [SettingsProvider]
        [UsedImplicitly]
        private static SettingsProvider GetProvider() => GetSingletonSettingsProvider();
    }
}