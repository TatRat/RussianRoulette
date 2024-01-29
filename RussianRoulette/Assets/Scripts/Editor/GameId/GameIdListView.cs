using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TatRat.Editor.GameId
{
    internal sealed class GameIdListView : TreeView
    {
        public event Action OnEnterPress, OnEscapePress;

        public GameIdListView() : base(new TreeViewState())
        {
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1);
            var items = GetItems();

            SetupParentsAndChildrenFromDepths(root, items);

            return root;
        }

        private IList<TreeViewItem> GetItems()
            => GameIdSettings.Instance.IdTuples.Select((data, index) => new GameIdListViewItem(index, data.Item2, 0,
                new GUIContent(text: data.Item2, tooltip: $"#{index} Value = {data.Item1}"))).ToArray();

        protected override void RowGUI(RowGUIArgs args)
        {
            if (args.item is not GameIdListViewItem listItem)
            {
                base.RowGUI(args);
            }
            else
            {
                EditorGUI.LabelField(args.rowRect, listItem.viewContent);
            }
        }

        protected override bool DoesItemMatchSearch(TreeViewItem item, string search)
            => item.displayName.IndexOf(search, StringComparison.InvariantCultureIgnoreCase) >= 0;

        protected override bool CanMultiSelect(TreeViewItem item) => false;

        protected override void KeyEvent()
        {
            var current = Event.current;
            var eventType = current.GetTypeForControl(treeViewControlID);

            if (eventType != EventType.KeyUp || current.modifiers != EventModifiers.None)
            {
                return;
            }

            switch (current.keyCode)
            {
                case KeyCode.KeypadEnter:
                case KeyCode.Return:
                    OnEnterPress?.Invoke();
                    break;

                case KeyCode.Escape:
                case KeyCode.Backspace:
                    OnEscapePress?.Invoke();
                    break;
            }
        }
    }
}