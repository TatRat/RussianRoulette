using System;
using TatRat.API;
using UnityEditor;
using UnityEngine;

namespace TatRat.Editor.GameId
{
    [CustomPropertyDrawer(typeof(API.GameId))]
    internal sealed class GameIdPropertyDrawer : PropertyDrawer
    {
        private const int CLEAR_BUTTON_WIDTH = 45;
        private const string CLEAR_BUTTON_LABEL = "Clear";
        private const int CREATE_BUTTON_WIDTH = 50;
        private const string CREATE_BUTTON_LABEL = "Create";

        private static GameIdSettings Settings
            => GameIdSettings.Instance;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (Attribute.IsDefined(fieldInfo, typeof(OldGameIdViewAttribute)))
            {
                OLD_OnGUI(position, property, label);
            }
            else
            {
                NEW_OnGUI(position, property, label);
            }
        }

        private void OLD_OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var _ = new EditorGUI.PropertyScope(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);
            property.NextVisible(true);

            var currentId = property.intValue;
            var currentIdName = Settings.GetIdName(currentId);
            var idNames = Settings.IdNames;
            var currentIndex = Array.IndexOf(idNames, currentIdName);

            var popupPosition = position;
            popupPosition.width -= CLEAR_BUTTON_WIDTH;

            var buttonPosition = position;
            buttonPosition.width = CLEAR_BUTTON_WIDTH;
            buttonPosition.x = position.x + position.width - CLEAR_BUTTON_WIDTH;

            using var indent = new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel);
            using var changeCheck = new EditorGUI.ChangeCheckScope();
            var newIndex = EditorGUI.Popup(popupPosition, string.Empty, currentIndex, idNames);

            if (changeCheck.changed)
            {
                property.intValue = Settings.GetIdByIndex(newIndex);
            }

            if (GUI.Button(buttonPosition, CLEAR_BUTTON_LABEL))
            {
                property.intValue = 0;
            }
        }

        private void NEW_OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var _ = new EditorGUI.PropertyScope(position, label, property);

            position = EditorGUI.PrefixLabel(position, label);
            property.NextVisible(true);

            var currentId = property.intValue;
            var currentIdName = Settings.GetIdName(currentId);

            var dropdownPosition = position;
            dropdownPosition.width -= CLEAR_BUTTON_WIDTH + CREATE_BUTTON_WIDTH;

            var createButtonPosition = position;
            createButtonPosition.width = CREATE_BUTTON_WIDTH;
            createButtonPosition.x += dropdownPosition.width;

            var clearButtonPosition = position;
            clearButtonPosition.x += dropdownPosition.width + CREATE_BUTTON_WIDTH;
            clearButtonPosition.width = CLEAR_BUTTON_WIDTH;

            using var indent = new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel);

            if (EditorGUI.DropdownButton(
                    dropdownPosition,
                    currentIdName.ToGUI(),
                    FocusType.Keyboard | FocusType.Passive))
            {
                ShowSelectGameIdPopup(position, property.Copy());
            }

            if (GUI.Button(createButtonPosition, CREATE_BUTTON_LABEL))
            {
                ShowCreateGameIdPopup(position, property.Copy());
            }

            if (GUI.Button(clearButtonPosition, CLEAR_BUTTON_LABEL))
            {
                property.intValue = 0;
            }
        }

        private void ShowSelectGameIdPopup(Rect rect, SerializedProperty property)
            => PopupWindow.Show(rect, new SelectGameIdPopup(rect, property));

        private void ShowCreateGameIdPopup(Rect rect, SerializedProperty property)
            => PopupWindow.Show(rect, new CreateGameIdPopup(rect, property));
    }
}