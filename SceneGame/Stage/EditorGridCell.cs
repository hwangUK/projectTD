#if UNITY_EDITOR

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridCell))]
public class EditorGridCell : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.color = Color.red;
        if(GUILayout.Button("타일 생성하기"))
        {
            GridCell t = (GridCell)target;
            t.GenerateCell();
        }
    }
}
#endif
