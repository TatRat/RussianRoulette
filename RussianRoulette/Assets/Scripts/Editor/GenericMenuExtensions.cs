using System;
using UnityEditor;

namespace TatRat.Editor
{
    public static class GenericMenuExtensions
    {
        public static void AddItem(this GenericMenu menu, string label, Action callback)
            => menu.AddItem(label.ToGUI(), false, () => callback?.Invoke());

        public static void AddDisabledItem(this GenericMenu menu, string label)
            => menu.AddDisabledItem(label.ToGUI());

        public static void AddEmptySeparator(this GenericMenu menu)
            => menu.AddSeparator(string.Empty);
    }
}