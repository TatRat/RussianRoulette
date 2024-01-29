using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TatRat.Editor.TypeSelect
{
    internal sealed class TypeSelectPopup : PopupWindowContent
    {
        private const int MAX_VISIBLE_LINES = 12;
        private const string SELECT_BUTTON_LABEL = "Select";

        private readonly Rect _controlRect;
        private readonly SerializedProperty _property;
        private readonly Type[] _allowedTypes;

        private readonly SearchField _searchField;
        private readonly TypeSelectListView _listView;

        private string _searchString;
        private bool _isOnGuiFiredOnce;

        private object ManagedRefValue
        {
            get => _property.managedReferenceValue;
            set
            {
                _property.managedReferenceValue = value;
                _property.serializedObject.ApplyModifiedProperties();
            }
        }

        private bool IsAnyTypeSelected
            => _listView.HasSelection();

        private Type SelectedType
            => _allowedTypes[_listView.GetSelection().First()];

        private static float SingleLineHeight
            => EditorGUIUtility.singleLineHeight;

        public TypeSelectPopup(
            Rect controlRect,
            SerializedProperty property,
            Type[] allowedTypes
        )
        {
            _controlRect = controlRect;
            _property = property;
            _allowedTypes = allowedTypes;

            _searchField = new SearchField();
            _searchField.downOrUpArrowKeyPressed += OnSearchFieldUpDownArrowPress;

            _listView = new TypeSelectListView(_allowedTypes.Select(type => type.Name.ToNice()).ToArray());
            _listView.OnEnterPress += OnEnterPress;
            _listView.OnEscapePress += OnEscapePress;
        }

        public override Vector2 GetWindowSize() =>
            new(_controlRect.width, SingleLineHeight * MAX_VISIBLE_LINES);

        public override void OnGUI(Rect rect)
        {
            RevealSelectionWithFirstOnGUI();

            var searchFieldRect = rect;
            searchFieldRect.height = SingleLineHeight;

            var listViewRect = rect;
            listViewRect.height -= SingleLineHeight * 2;
            listViewRect.y += SingleLineHeight;

            var selectButtonRect = rect;
            selectButtonRect.height = SingleLineHeight;
            selectButtonRect.y = rect.y + rect.height - SingleLineHeight;

            DrawSearchField(searchFieldRect);
            DrawList(listViewRect);
            DrawSelectButton(selectButtonRect);
        }

        private void RevealSelectionWithFirstOnGUI()
        {
            if (_isOnGuiFiredOnce) return;

            _isOnGuiFiredOnce = true;

            if (ManagedRefValue is null)
            {
                return;
            }

            var type = ManagedRefValue.GetType();
            var index = Array.IndexOf(_allowedTypes, type);

            if (index < 0)
            {
                return;
            }

            _listView.SetSelection(new List<int>() { index },
                TreeViewSelectionOptions.FireSelectionChanged | TreeViewSelectionOptions.RevealAndFrame);
            _listView.SetFocusAndEnsureSelectedItem();
        }

        private void DrawSearchField(Rect rect)
        {
            using var changeCheck = new EditorGUI.ChangeCheckScope();
            _searchString = _searchField.OnGUI(rect, _searchString);

            if (changeCheck.changed)
            {
                _listView.searchString = _searchString;
            }
        }

        private void DrawList(Rect rect)
            => _listView.OnGUI(rect);

        private void DrawSelectButton(Rect rect)
        {
            using var _ = new EditorGUI.DisabledScope(!IsAnyTypeSelected);

            if (!GUI.Button(rect, SELECT_BUTTON_LABEL))
            {
                return;
            }

            ManagedRefValue = Activator.CreateInstance(SelectedType);
            CloseWindow();
        }

        private void OnSearchFieldUpDownArrowPress()
            => _listView.SetFocusAndEnsureSelectedItem();

        private void OnEnterPress()
        {
            if (IsAnyTypeSelected)
            {
                ManagedRefValue = Activator.CreateInstance(SelectedType);
            }

            CloseWindow();
        }

        private void OnEscapePress()
            => CloseWindow();

        private void CloseWindow()
            => editorWindow.Close();
    }
}