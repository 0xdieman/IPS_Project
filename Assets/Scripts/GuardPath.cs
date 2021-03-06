using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardPath : AgentBehaviour
{
    public float speed = 5f;
    public float waitTime = 3f;
    public Transform pathHolder;
    private Transform targetWaypoint;

    private void Start()
    {
        Transform[] waypoints = new Transform[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i);

        }
        StartCoroutine(FollowPath(waypoints));

    }

    IEnumerator FollowPath(Transform[] waypoints)
    {
        transform.position = waypoints[0].position;
        int targetWaypointIndex = 1;
        this.targetWaypoint = waypoints[targetWaypointIndex];
        while (true)
        {
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.05f)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
}
    }
    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

    public Transform GetWaypoint()
    {
        return targetWaypoint;
    }

    public Transform GetStartingPoint()
    {
        return pathHolder.GetComponentInChildren<Transform>();
    }
}


