using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace System_Programming.Lesson10
{
    [CustomPropertyDrawer(typeof(ColorPoint))]
    public class ColorPointDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            label = EditorGUI.BeginProperty(position, label, property);
            var contentPosition = EditorGUI.PrefixLabel(position, label);

            if (SmallWidth())
            {
                position.height = 16f;
                EditorGUI.indentLevel += 1;
                contentPosition = EditorGUI.IndentedRect(position);
                contentPosition.y += 18f;
            }

            contentPosition.width *= 0.75f;

            EditorGUI.indentLevel = 0;
            EditorGUI.PropertyField(contentPosition,
                property.FindPropertyRelative("Position"), GUIContent.none);

            contentPosition.x += contentPosition.width;
            contentPosition.width /= 3f;

            EditorGUIUtility.labelWidth = 14f;

            EditorGUI.PropertyField(contentPosition,
                property.FindPropertyRelative("Color"), new GUIContent("C"));

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return SmallWidth() ? 16f + 18f : 18f;
        }

        private static bool SmallWidth()
        {
            return Screen.width < 419;
        }
    }
}