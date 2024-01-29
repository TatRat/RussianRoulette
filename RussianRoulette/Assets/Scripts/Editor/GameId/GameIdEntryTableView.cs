using System;
using System.Linq;
using UnityEditor;

namespace TatRat.Editor.GameId
{
    internal sealed class GameIdEntryTableView : SerializedArrayTableView
    {
        public event Action AddElementCallback;
        public event Action<int[]> RemoveElementsCallback;

        public GameIdEntryTableView(SerializedProperty arrayProperty) : base(arrayProperty)
        {
        }

        protected override void AddElement()
            => AddElementCallback?.Invoke();

        protected override void RemoveElement()
            => RemoveElementsCallback?.Invoke(GetSelection().OrderByDescending(x => x).ToArray());
    }
}