using TatRat.API;
using UnityEditor;
using UnityEngine;

namespace TatRat.Editor
{
    [CustomPropertyDrawer(typeof(AnimatorHash))]
    public class AnimatorHashPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            using var propertyScope = new EditorGUI.PropertyScope(position, label, property);
            var rect = EditorGUI.PrefixLabel(position, propertyScope.content);
            property.NextVisible(true);

            using var checkScope = new EditorGUI.ChangeCheckScope();
            EditorGUI.PropertyField(rect, property, GUIContent.none);
            var hashName = property.stringValue;

            if (checkScope.changed)
            {
                property.Next(false);
                property.intValue = Animator.StringToHash(hashName);
            }
        }
    }
}