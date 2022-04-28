using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FlockManager : MonoBehaviour {

    // Access the fish prefab
    public GameObject fishPrefab;
    // Starting number of fish
    public int numFish = 20;
    // Array of fish prefabs
    public GameObject[] allFish;
    // Swimming bounds for fish
    public Vector3 swimLimits = new Vector3(5.0f, 5.0f, 5.0f);
    // Goal position
    public Vector3 goalPos;

    // Header title for Unity Editor
    [Header("Fish Settings")]
    [Range(0.0f, 5.0f)]
    public float minSpeed;          // Minimum speed range
    [Range(0.0f, 5.0f)]
    public float maxSpeed;          // Maximum speed range
    [Range(1.0f, 10.0f)]
    public float neighbourDistance; // Prefab neighbout distance
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;     // Speed at which the prefabs rotate

    private Bounds swimBound;
    void Start() {
        swimBound = new Bounds(transform.position, swimLimits * 2.0f);
        // Allocate the allFish array
        allFish = new GameObject[numFish];
        GameObject boids = new GameObject();
        boids.name = "Boids";
        // Loop through the array instantiating the prefabs.  In this case fish
        for (int i = 0; i < numFish; ++i) {

            Vector3 pos = this.transform.position + new Vector3(Random.Range(-swimLimits.x/2, swimLimits.x/2),
                                                                Random.Range(-swimLimits.y/2, swimLimits.y/2),
                                                                Random.Range(-swimLimits.z/2, swimLimits.z/2));
            allFish[i] = (GameObject)Instantiate(fishPrefab, pos, Quaternion.identity);
            allFish[i].GetComponent<Flock>().myManager = this;
            allFish[i].transform.parent = boids.transform;
        }

        // Target for the prefbas to head for
        goalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update() {

        // Update the target for the prefabs to head for with a random chance
        if (Random.Range(0.0f, 1000.0f) < 5.0f) {
            goalPos = this.transform.position + new Vector3(Random.Range(-swimLimits.x/2, swimLimits.x/2),
                                                            Random.Range(-swimLimits.y/2, swimLimits.y/2),
                                                            Random.Range(-swimLimits.z/2, swimLimits.z/2));
        }
        //goalPos = this.transform.position;
        //Debug.DrawLine(this.transform.position, goalPos, Color.red);
    }

    public Bounds GetFlockBouds()
    {
        swimBound = new Bounds(goalPos, swimLimits);
        return swimBound;
    }
    private void OnDrawGizmos()
    {
   
        Gizmos.DrawWireCube(transform.position, swimLimits * 2.0f);
     
    }
}
