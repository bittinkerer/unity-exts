using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Editors
{
    [CustomPropertyDrawer(typeof(DisableIntAttribute))]
    public class DisabledIntPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            IntegerField integerField = new IntegerField(property.displayName)
            { 
                value = property.intValue,
            };
            integerField.SetEnabled(false);
            return integerField;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.IntField(position, label, property.intValue);
            GUI.enabled = true;
        }
    }
}
