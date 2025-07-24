using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Way Point", fileName = "New Way Point")]
public class WayPoints : ScriptableObject
{
    [SerializeField] GameObject waypoints;
    public List<GameObject> GetWaypoints()
    {
        List<GameObject> waypointList = new List<GameObject>();

        foreach (Transform child in waypoints.transform)
        {
            waypointList.Add(child.gameObject);
        }

        return waypointList;
    }
}
