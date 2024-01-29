using System;
using System.Linq;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TatRat.Editor.TypeSelect
{
    internal sealed class TypeSelectListView : TreeView
    {
        private readonly string[] _labels;

        public event Action OnEnterPress, OnEscapePress;

        public TypeSelectListView(string[] labels) :
            base(new TreeViewState())
        {
            _labels = labels;
            showAlternatingRowBackgrounds = true;
            Reload();
        }

        protected override TreeViewItem BuildRoot()
        {
            var root = new TreeViewItem(-1, -1);
            var items = _labels.Select((label, i) => new TreeViewItem(i, 0, label)).ToArray();

            SetupParentsAndChildrenFromDepths(root, items);

            return root;
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