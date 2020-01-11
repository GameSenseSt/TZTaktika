using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class EnemyWayPointsController : MonoBehaviour
{
    #region PublicFields
    [HideInInspector]
    public List<Transform> WayPoints; //List of waypoints
    #endregion

    #region SerializedFields
    [SerializeField]
    private GameObject WayPointPrefab; //Prefab
    [SerializeField]
    private Transform startPoint, finishPoint; //Start & finish
    #endregion

    #region MonoBehaviourCallbacks
    private void Start()
    {
        Initialization(); //Initialization
    }

    private void Update()
    {
        DrawWayLines();
    }
    #endregion

    #region PublicMethods
    public void AddPoint() //Adding new way point
    {
        GameObject tempWayPoint = Instantiate(WayPointPrefab, transform);
        tempWayPoint.transform.position = WayPoints[WayPoints.Count - 2].position;
        tempWayPoint.gameObject.name = "WayPoint" + (WayPoints.Count - 1);
        WayPoints.RemoveAt(WayPoints.Count - 1);
        Selection.activeGameObject = tempWayPoint;
        WayPoints.Add(tempWayPoint.transform);
        WayPoints.Add(finishPoint);
    }

    public void RemoveLastAddedPoint() //Removing last waypoint
    {
        DestroyImmediate(WayPoints[WayPoints.Count - 2].gameObject);
        WayPoints.RemoveAt(WayPoints.Count - 2);
    }

    public void RefreshList() //Removing all waypoints except start & finish
    {
        for (int z=1; z<WayPoints.Count-1; z++)
        {
            DestroyImmediate(WayPoints[z].gameObject);
        }
        WayPoints.Clear();
        WayPoints.Add(startPoint);
        WayPoints.Add(finishPoint);
    }
    #endregion

    #region PrivateMethods
    private void Initialization() //Initialization
    {
        if (WayPoints.Count == 0) //Adding start & finish
        {
            WayPoints.Add(startPoint);
            WayPoints.Add(finishPoint);
        }
    }

    private void DrawWayLines() //Connecting all points with lines
    {
        for (int z=0; z<WayPoints.Count-1; z++)
        {
            Debug.DrawLine(WayPoints[z].position, WayPoints[z+1].position, Color.red);
        }
    }
    #endregion
}
