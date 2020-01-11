using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWayPointsHolder : MonoBehaviour
{
    #region PublicFields
    public static List<Transform> WayPoints; //Public list of way points
    #endregion

    #region MonoBehaviourCallbacks
    private void Awake()
    {
        WayPoints = new List<Transform>();
        foreach (Transform child in GetComponent<EnemyWayPointsController>().WayPoints)
        {
            WayPoints.Add(child);
        }
    }
    #endregion
}
