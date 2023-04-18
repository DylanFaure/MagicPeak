using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New card", menuName = "Character")]
public class Item : ScriptableObject
{
    public string nameCard;
    public Sprite sprite;

    [Range(1, 100)]
    public int dropRate;

    public enum EnumElements { None, Poison, Ice, Fire, Electric };
    public EnumElements firstElement;
    public EnumElements secondeElement;
}

[CustomEditor(typeof(Item))]
public class CustomDropdown : Editor {
    SerializedProperty enumPropsElements;

    void OnEnable() {
        enumPropsElements = serializedObject.FindProperty("EnumElements");
    }

    public new void OnInspectorGUI() {
        serializedObject.Update();

        EditorGUILayout.PropertyField(enumPropsElements);

        serializedObject.ApplyModifiedProperties();
    }
}