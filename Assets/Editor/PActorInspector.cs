using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

//[CustomEditor(typeof(PActor))]
public class PActorInspector : Editor {

    PActor actor;
    static bool showTileEditor = false;

    public void OnEnable()
    {
        actor = (PActor)target;
    }

    public override void OnInspectorGUI()
    {
        //MAP DEFAULT INFORMATION
        actor.shortDisplayName = EditorGUILayout.TextField("Name", actor.shortDisplayName);

        var list = actor.startingInventory;
        int newCount = Mathf.Max(0, EditorGUILayout.IntField("size", list.Count));
        while (newCount < list.Count)
            list.RemoveAt(list.Count - 1);
        while (newCount > list.Count)
            list.Add(null);

        for (int i = 0; i < list.Count; i++)
        {
            //list[i] = (PItem)EditorGUILayout.ObjectField(list[i], typeof(PItem));
        }
    }

}
