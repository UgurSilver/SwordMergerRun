using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SliceablePosManager))]
public class SliceablePosEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SliceablePosManager sliceablePosManager = (SliceablePosManager)target;
        sliceablePosManager.initialPos = EditorGUILayout.Vector3Field("IntialPos", sliceablePosManager.initialPos);
        sliceablePosManager.initialRot = EditorGUILayout.Vector3Field("IntialRot", sliceablePosManager.initialRot);
        sliceablePosManager.xDistance = EditorGUILayout.FloatField("xDistance", sliceablePosManager.xDistance);
        sliceablePosManager.zDistance = EditorGUILayout.FloatField("zDistance", sliceablePosManager.zDistance);
        sliceablePosManager.isXMove = EditorGUILayout.IntField("isXMove", sliceablePosManager.isXMove);
        sliceablePosManager.maxXPos = EditorGUILayout.FloatField("MaxXPos", sliceablePosManager.maxXPos);
        if (GUILayout.Button("Set Pos"))
        {
            sliceablePosManager.initialPos = EditorGUILayout.Vector3Field("IntialPos", sliceablePosManager.initialPos);
            sliceablePosManager.initialRot = EditorGUILayout.Vector3Field("IntialRot", sliceablePosManager.initialRot);
            sliceablePosManager.xDistance = EditorGUILayout.FloatField("xDistance", sliceablePosManager.xDistance);
            sliceablePosManager.zDistance = EditorGUILayout.FloatField("zDistance", sliceablePosManager.zDistance);
            sliceablePosManager.isXMove = EditorGUILayout.IntField("isXMove", sliceablePosManager.isXMove);
            sliceablePosManager.maxXPos = EditorGUILayout.FloatField("MaxXPos", sliceablePosManager.maxXPos);
            sliceablePosManager.SetPos();
        }
    }
}
