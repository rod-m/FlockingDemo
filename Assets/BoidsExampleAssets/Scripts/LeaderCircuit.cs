using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderCircuit : MonoBehaviour
{
    public Transform[] waypoints;
    private Vector3 startMarker;
    private Vector3 endMarker;
    private int destination = 0;
    
    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    
    // Start is called before the first frame update
    void Start()
    {
        startMarker = transform.position;
        endMarker = waypoints[destination].position;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(waypoints[0].position, waypoints[1].position);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
        transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);
        if (Vector3.Distance(transform.position, endMarker) < 0.01f)
        {
            destination++;
            destination = destination % waypoints.Length;
            startMarker = transform.position;
            endMarker = waypoints[destination].position;
            startTime = Time.time;
            journeyLength = Vector3.Distance(startMarker, endMarker);
        }
    }
}
