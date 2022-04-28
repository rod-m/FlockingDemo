using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]

public class FlockingController : MonoBehaviour
{
    public float minVelocity = 5;
    public float maxVelocity = 20;
    public float randomness = 1;
    public int flockSize = 20;
    public GameObject prefab;
    public GameObject chasee;
 
    public Vector3 flockCenter;
    public Vector3 flockVelocity;

    private Collider collider;

    private BoidElement[] boidComponents;
    void Start()
    {
        collider = GetComponent<Collider>();
        boidComponents = new BoidElement[flockSize];
        // Use collider to contrain start position
        for (var i=0; i<flockSize; i++)
        {
            Vector3 position = new Vector3 (
                                   Random.value * collider.bounds.size.x,
                                   Random.value * collider.bounds.size.y,
                                   Random.value * collider.bounds.size.z
                               ) - collider.bounds.extents;
 
            GameObject boid = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            boid.transform.parent = transform;
            boid.transform.localPosition = position;
            boidComponents[i] = boid.GetComponent<BoidElement>(); 
            boidComponents[i].SetController (gameObject);
        
           
        }
    }
 
    void Update ()
    {
        Vector3 theCenter = Vector3.zero;
        Vector3 theVelocity = Vector3.zero;

        foreach (var boidComponent in boidComponents)
        {
            // get boid position and velocity relative to the controller
            theCenter = theCenter + boidComponent.transform.localPosition;
            theVelocity = theVelocity + boidComponent.BoidVelocity();
        }
        // average out overall position and velocity of the floack
        flockCenter = theCenter/(flockSize);
        flockVelocity = theVelocity/(flockSize);
    }
}
