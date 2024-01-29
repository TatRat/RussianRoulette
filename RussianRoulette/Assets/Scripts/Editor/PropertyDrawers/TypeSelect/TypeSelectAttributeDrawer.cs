using System;
using System.Linq;
using TatRat.API;
using UnityEditor;
using UnityEngine;

namespace TatRat.Editor.TypeSelect
{
    [CustomPropertyDrawer(typeof(TypeSelectAttribute))]
    internal sealed class TypeSelectAttributeDrawer : PropertyDrawer
    {
        private const SerializedPropertyType SUPPORTED_TYPE = SerializedPropertyType.ManagedReference;

        private const int CLEAR_BUTTON_WIDTH = 45;
        private const string CLEAR_BUTTON_LABEL = "Clear";

        private static TypeSelectAttributeDrawerSettings Settings
            => TypeSelectAttributeDrawerSettings.Instance;

        private static float SingleLineHeight
            => EditorGUIUtility.singleLineHeight;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SUPPORTED_TYPE)
            {
                throw new NotSupportedException($"Only {SUPPORTED_TYPE} supported");
            }

            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);

            var dropdownRect = position;
            var objectRect = position;

            dropdownRect.height = SingleLineHeight;

            DrawObjectSelector(dropdownRect, property, label);
            DrawDefaultProperty(objectRect, property);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
            => EditorGUI.GetPropertyHeight(property, true);

        private void DrawObjectSelector(Rect position, SerializedProperty property, GUIContent label)
        {
            position = EditorGUI.PrefixLabel(position, label);

            using var _ = new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel);
            var dropdownRect = position;
            var clearButtonRect = position;

            dropdownRect.width -= CLEAR_BUTTON_WIDTH;
            clearButtonRect.x += dropdownRect.width;
            clearButtonRect.width = CLEAR_BUTTON_WIDTH;

            DrawTypeDropdown(dropdownRect, property);
            DrawClearButton(clearButtonRect, property);
        }

        private void DrawTypeDropdown(Rect position, SerializedProperty property)
        {
            if (!EditorGUI.DropdownButton(position,
                    GetManagedRefTypeName(property).ToGUI(),
                    FocusType.Keyboard | FocusType.Passive))
            {
                return;
            }

            ShowPopup(position, property.Copy());
        }

        private static string GetManagedRefTypeName(SerializedProperty property)
        {
            var typeName = property.managedReferenceFullTypename;
            var subStringPosition = -1;

            switch (Settings.NameModification)
            {
                case TypeSelectOriginalNameModification.AfterWhiteSpace:
                    subStringPosition = typeName.IndexOf(' ');
                    break;

                case TypeSelectOriginalNameModification.AfterLastDot:
                    subStringPosition = typeName.LastIndexOf('.');
                    break;
            }

            if (subStringPosition >= 0)
            {
                typeName = typeName.Substring(subStringPosition + 1);
            }

            if (Settings.PrettifyFinalResult)
            {
                typeName = ObjectNames.NicifyVariableName(typeName);
            }

            return typeName;
        }

        private void DrawClearButton(Rect rect, SerializedProperty property)
        {
            if (!GUI.Button(rect, CLEAR_BUTTON_LABEL))
            {
                return;
            }

            ClearObject(property);
        }

        private void ClearObject(SerializedProperty property) 
            => property.managedReferenceValue = null;

        private void ShowPopup(Rect position, SerializedProperty property)
        {
            var allowedTypes = TypeSelectUtils.GetAllowedTypes(fieldInfo.FieldType);
            if (!allowedTypes.Any())
            {
                return;
            }
            
            PopupWindow.Show(position, new TypeSelectPopup(position, property, allowedTypes));
        }

        private void DrawDefaultProperty(Rect position, SerializedProperty property)
            => EditorGUI.PropertyField(position, property, true);
    }
}