using UnityEditor.IMGUI.Controls;
using UnityEngine;

namespace TatRat.Editor.GameId
{
    internal sealed class GameIdListViewItem : TreeViewItem
    {
        public GUIContent viewContent { get; set; }

        public GameIdListViewItem(
            int id,
            string displayName,
            int depth,
            GUIContent viewContent
        )
        {
            this.id = id;
            this.displayName = displayName;
            this.depth = depth;
            this.viewContent = viewContent;
        }
    }
}