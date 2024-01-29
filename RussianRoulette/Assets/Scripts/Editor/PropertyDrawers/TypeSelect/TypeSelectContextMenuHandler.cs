using UnityEditor;
using UnityEngine;

namespace TatRat.Editor.TypeSelect
{
    [InitializeOnLoad]
    internal static class TypeSelectContextMenuHandler
    {
        private const string COPY_SERIALIZED_REF_VALUE = "Copy Serialized Reference value";
        private const string PASTE_SERIALIZED_REF_VALUE = "Paste Serialized Reference value";

        private static string SystemCopyBuffer
        {
            get => EditorGUIUtility.systemCopyBuffer;
            set => EditorGUIUtility.systemCopyBuffer = value;
        }

        static TypeSelectContextMenuHandler()
        {
            EditorApplication.contextualPropertyMenu -= TryToShowUtilsActionForTypeSelect;
            EditorApplication.contextualPropertyMenu += TryToShowUtilsActionForTypeSelect;
        }

        private static void TryToShowUtilsActionForTypeSelect(GenericMenu menu, SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ManagedReference || property.isArray)
            {
                return;
            }

            ShowSerializedReferenceContextActions(menu, property.Copy());
        }

        private static void ShowSerializedReferenceContextActions(GenericMenu menu, SerializedProperty property)
        {
            ShowCopySerializedReference(menu, property);
            ShowPasteSerializedReference(menu, property);
        }

        private static void ShowCopySerializedReference(GenericMenu menu, SerializedProperty property)
        {
            if (property.managedReferenceValue is null)
            {
                menu.AddDisabledItem(COPY_SERIALIZED_REF_VALUE);
            }
            else
            {
                menu.AddItem(COPY_SERIALIZED_REF_VALUE, () => CopySerializedReferenceToSystemBuffer(property));
            }
        }

        private static void CopySerializedReferenceToSystemBuffer(SerializedProperty property)
        {
            var obj = property.managedReferenceValue;
            var type = obj.GetType();
            var json = JsonUtility.ToJson(obj);

            var data = new TypeSelectCopyPasteData(json, type.FullName);

            SystemCopyBuffer = JsonUtility.ToJson(data);
        }

        private static void ShowPasteSerializedReference(GenericMenu menu, SerializedProperty property)
        {
            TypeSelectCopyPasteData data = null;

            try
            {
                data = JsonUtility.FromJson<TypeSelectCopyPasteData>(SystemCopyBuffer);
            }
            catch
            {
                menu.AddDisabledItem(PASTE_SERIALIZED_REF_VALUE);
                return;
            }

            if (data is null || !data.IsValid)
            {
                menu.AddDisabledItem(PASTE_SERIALIZED_REF_VALUE);
                return;
            }

            var fieldTypeName = property.managedReferenceFieldTypename;
            fieldTypeName = fieldTypeName.Substring(fieldTypeName.IndexOf(' '));

            var fieldType = TypeSelectUtils.GetTypeByFullName(fieldTypeName);
            if (fieldType is null)
            {
                menu.AddDisabledItem(PASTE_SERIALIZED_REF_VALUE);
                return;
            }

            var dataType = TypeSelectUtils.GetTypeByFullName(data.Typename);
            if (dataType is null)
            {
                menu.AddDisabledItem(PASTE_SERIALIZED_REF_VALUE);
                return;
            }

            if (!TypeSelectUtils.IsTypeSubclassOrImplementingInterface(dataType, fieldType))
            {
                menu.AddDisabledItem(PASTE_SERIALIZED_REF_VALUE);
            }
            else
            {
                menu.AddItem(PASTE_SERIALIZED_REF_VALUE,
                    () => PasteSerializedReferenceFromSystemBuffer(property, data));
            }
        }

        private static void PasteSerializedReferenceFromSystemBuffer(
            SerializedProperty property,
            TypeSelectCopyPasteData data)
        {
            property.managedReferenceValue =
                JsonUtility.FromJson(data.Json, TypeSelectUtils.GetTypeByFullName(data.Typename));
            property.serializedObject.ApplyModifiedProperties();
        }
    }
}