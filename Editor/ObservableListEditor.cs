using Packages.Estenis.UnityExts_;
using UnityEditor;
using UnityEngine;

namespace Packages.Estenis.UnityExtsEditor_
{
    [CustomPropertyDrawer(typeof(ObservableList), true)]
    public class ObservableListEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var list = property.FindPropertyRelative("_list");
            EditorGUI.PropertyField(position, list, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var list = property.FindPropertyRelative("_list");

            return EditorGUI.GetPropertyHeight(list, label, true);
        }
    }
}