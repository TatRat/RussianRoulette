using System;
using System.Linq;
using TatRat.API;
using UnityEditor;
using UnityEngine;

namespace TatRat.Editor
{
    [CustomPropertyDrawer(typeof(SceneIndexAttribute))]
    public sealed class SceneIndexAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Integer)
            {
                throw new NotSupportedException(property.propertyType.ToString());
            }

            var sceneNames = EditorBuildSettings.scenes.Select(x => x.path).ToArray();
            property.intValue = EditorGUI.Popup(position, property.displayName, property.intValue, sceneNames);
        }
    }
}