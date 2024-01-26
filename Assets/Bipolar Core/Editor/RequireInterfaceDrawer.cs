﻿using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace Bipolar.Editor
{
    [CustomPropertyDrawer(typeof(RequireInterfaceAttribute))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        private const string errorMessage = "Property is not a reference type";

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                var requiredAttribute = attribute as RequireInterfaceAttribute;
                EditorGUI.BeginProperty(position, label, property);
                property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, requiredAttribute.RequiredType, true);
                EditorGUI.EndProperty();
            }
            else
            {
                var previousColor = GUI.color;
                GUI.color = Color.red;
                EditorGUI.LabelField(position, label, new GUIContent(errorMessage));
                GUI.color = previousColor;
            }
        }
    }
}
#endif
