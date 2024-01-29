using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TatRat.Editor.GameId
{
    internal sealed class SelectGameIdPopup : PopupWindowContent
    {
        private const int MAX_VISIBLE_LINES = 12;
        private const string SELECT_BUTTON_LABEL = "Select";

        private readonly Rect _controlRect;
        private readonly SerializedProperty _property;
        private readonly SearchField _searchField;
        private readonly GameIdListView _idListView;

        private string _searchString;
        private bool _isOnGuiFireOnce;

        private int GameIdValue
        {
            get => _property.intValue;
            set
            {
                _property.intValue = value;
                _property.serializedObject.ApplyModifiedProperties();
            }
        }

        private static float SingleLineHeight
            => EditorGUIUtility.singleLineHeight;

        private static GameIdSettings Settings
            => GameIdSettings.Instance;

        private bool IsNewIdSelected
            => _idListView.HasSelection();

        private int NewIdIndex
            => _idListView.GetSelection().First();

        public SelectGameIdPopup(
            Rect controlRect,
            SerializedProperty property
        )
        {
            _controlRect = controlRect;
            _property = property;

            _searchField = new SearchField();
            _searchField.downOrUpArrowKeyPressed += OnDownOrUpKeyPressed;

            _idListView = new GameIdListView();
            _idListView.OnEnterPress += OnEnterPress;
            _idListView.OnEscapePress += OnEscapePress;
        }

        public override Vector2 GetWindowSize() =>
            new(_controlRect.width, SingleLineHeight * MAX_VISIBLE_LINES);

        public override void OnGUI(Rect rect)
        {
            RevealSelectionWithFirstOnGUI();

            var searchFieldRect = rect;
            searchFieldRect.height = SingleLineHeight;

            var listRect = rect;
            listRect.y += SingleLineHeight;
            listRect.height -= SingleLineHeight * 2;

            var selectButtonRect = rect;
            selectButtonRect.height = SingleLineHeight;
            selectButtonRect.y = rect.yMax - SingleLineHeight;

            DrawSearchField(searchFieldRect);
            DrawList(listRect);
            DrawSelectButton(selectButtonRect);
        }

        private void RevealSelectionWithFirstOnGUI()
        {
            if (_isOnGuiFireOnce) return;

            _idListView.SetSelection(new List<int>() { Settings.GetIndexById(GameIdValue) },
                TreeViewSelectionOptions.FireSelectionChanged | TreeViewSelectionOptions.RevealAndFrame);
            _idListView.SetFocusAndEnsureSelectedItem();
            _isOnGuiFireOnce = true;
        }

        private void DrawSearchField(Rect rect)
        {
            using var changeCheck = new EditorGUI.ChangeCheckScope();
            _searchString = _searchField.OnGUI(rect, _searchString);

            if (!changeCheck.changed)
            {
                return;
            }

            _idListView.searchString = _searchString;
        }

        private void DrawList(Rect rect)
        {
            _idListView.OnGUI(rect);
        }

        private void DrawSelectButton(Rect rect)
        {
            using var _ = new EditorGUI.DisabledScope(!IsNewIdSelected);

            if (!GUI.Button(rect, SELECT_BUTTON_LABEL))
            {
                return;
            }

            GameIdValue = Settings.GetIdByIndex(NewIdIndex);
            CloseWindow();
        }

        private void OnDownOrUpKeyPressed()
            => _idListView.SetFocusAndEnsureSelectedItem();

        private void OnEnterPress()
        {
            if (IsNewIdSelected)
            {
                GameIdValue = Settings.GetIdByIndex(NewIdIndex);
            }

            CloseWindow();
        }

        private void OnEscapePress()
            => CloseWindow();

        private void CloseWindow()
            => editorWindow.Close();
    }
}