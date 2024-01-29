using TatRat.API;
using UnityEditor;
using UnityEngine;

namespace TatRat.Editor
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public sealed class ReadOnlyAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var _ = new EditorGUI.DisabledScope(true);
            EditorGUI.PropertyField(position, property, label, true);
        }
    }
}