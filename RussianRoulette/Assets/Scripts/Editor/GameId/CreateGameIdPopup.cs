using TatRat.Editor.GameId;
using UnityEditor;
using UnityEngine;

namespace TatRat.Editor
{
    internal sealed class CreateGameIdPopup : PopupWindowContent
    {
        private const string CREATE_BUTTON_LABEL = "Create GameId";
        private readonly Rect _controlRect;
        private readonly SerializedProperty _property;
        private string _newGameIdName;

        private static float SingleLineHeight
            => EditorGUIUtility.singleLineHeight;

        private static GameIdSettings Settings
            => GameIdSettings.Instance;

        private bool IsNameFilled
            => !string.IsNullOrWhiteSpace(_newGameIdName);

        public CreateGameIdPopup(
            Rect controlRect,
            SerializedProperty property
        )
        {
            _controlRect = controlRect;
            _property = property;
        }

        public override Vector2 GetWindowSize()
            => new(x: _controlRect.width, y: SingleLineHeight * 2);

        public override void OnGUI(Rect rect)
        {
            var newNamePosition = rect;
            newNamePosition.height = SingleLineHeight;

            var createButtonPosition = rect;
            createButtonPosition.height = SingleLineHeight;
            createButtonPosition.y += SingleLineHeight;

            DrawNewGameIdName(newNamePosition);
            DrawCreateButton(createButtonPosition);
        }

        private void DrawNewGameIdName(Rect rect)
        {
            _newGameIdName = EditorGUI.TextField(rect, nameof(_newGameIdName).ToNice(), _newGameIdName);
        }

        private void DrawCreateButton(Rect rect)
        {
            using var _ = new EditorGUI.DisabledScope(!IsNameFilled);

            if (!GUI.Button(rect, CREATE_BUTTON_LABEL))
            {
                return;
            }

            CreateAndFillNewGameId();
            editorWindow.Close();
        }

        private void CreateAndFillNewGameId()
        {
            var newId = Settings.CreateNewId(_newGameIdName);
            _property.intValue = newId;
            _property.serializedObject.ApplyModifiedProperties();
        }
    }
}