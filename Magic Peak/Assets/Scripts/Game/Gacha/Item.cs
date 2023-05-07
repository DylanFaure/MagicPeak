using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "New card", menuName = "Character")]
#endif
public class Item : ScriptableObject
{
    public string nameCard;
    public Sprite sprite;
    public int rarity;

    public enum EnumElements { None, Poison, Ice, Fire, Electric };
    public EnumElements firstElement;
    public EnumElements secondeElement;
}

#if UNITY_EDITOR
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
#endif