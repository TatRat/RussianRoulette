using System;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UEditor = UnityEditor.Editor;

namespace TatRat.Editor.GameId
{
    [CustomEditor(typeof(GameIdSettings))]
    public sealed class GameIdSettingsEditor : UEditor
    {
        private const string TABLE_VIEW_PROPERTY_NAME = "_gameIds";
        private SerializedProperty _arrayProperty;
        private GameIdEntryTableView _tableView;

        private void OnEnable()
        {
            _arrayProperty = serializedObject.FindProperty(TABLE_VIEW_PROPERTY_NAME);
            _tableView = new GameIdEntryTableView(_arrayProperty);
            _tableView.AddElementCallback += AddNewElement;
            _tableView.RemoveElementsCallback += RemoveSelectedElements;
            _tableView.multiColumnHeader.ResizeToFit();
        }

        private void OnDisable()
        {
            _arrayProperty = null;
            _tableView.AddElementCallback -= AddNewElement;
            _tableView.RemoveElementsCallback -= RemoveSelectedElements;
            _tableView = null;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.UpdateIfRequiredOrScript();
            base.OnInspectorGUI();
            _tableView.OnGUILayout();
            serializedObject.ApplyModifiedProperties();
        }

        private void AddNewElement()
        {
            if (target is not GameIdSettings settings)
            {
                return;
            }

            settings.CreateNewId();

            serializedObject.UpdateIfRequiredOrScript();
            _tableView.Reload();
            _tableView.SetSelection(new[] { _arrayProperty.arraySize - 1 }, TreeViewSelectionOptions.RevealAndFrame);
            _tableView.SetFocusAndEnsureSelectedItem();
        }

        private void RemoveSelectedElements(int[] selectedElements)
        {
            if (target is not GameIdSettings settings)
            {
                return;
            }

            settings.RemoveElements(selectedElements);
            serializedObject.UpdateIfRequiredOrScript();
            _tableView.SetSelection(Array.Empty<int>());
            _tableView.Reload();
        }
    }
}