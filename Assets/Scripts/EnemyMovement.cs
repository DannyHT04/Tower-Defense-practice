using UnityEngine;
using System.Linq;
public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    public float speed = 2f;
    private int currentWaypoint = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {

    }
    void Start()
    {
        GameObject waypointsParent = GameObject.Find("Waypoints");
        waypoints = waypointsParent.GetComponentsInChildren<Transform>()
            .Where(t => t != waypointsParent.transform)
            .ToArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints == null) return;
        if (currentWaypoint == waypoints.Length) return;
        Transform targetWaypoint = waypoints[currentWaypoint];
        transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypoint++;

        }
    }
}
