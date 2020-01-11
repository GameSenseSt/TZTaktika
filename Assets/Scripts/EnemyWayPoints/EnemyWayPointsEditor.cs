using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyWayPointsController))]
public class EnemyWayPointsEditor : Editor 
{
    //Adding buttons to Inspector
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyWayPointsController wpController = (EnemyWayPointsController)target;
        if (GUILayout.Button("Add Point"))
        {
            wpController.AddPoint();
        }
        if (GUILayout.Button("Remove Last Added"))
        {
            wpController.RemoveLastAddedPoint();
        }
        if (GUILayout.Button("Refresh List"))
        {
            wpController.RefreshList();
        }
    }
}
