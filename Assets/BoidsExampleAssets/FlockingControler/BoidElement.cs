using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoidElement : MonoBehaviour
{
   private GameObject Controller;
    private bool initialised = false;
    private float minVelocity;
    private float maxVelocity;
    private float randomness;
    private GameObject chasee;
    private Rigidbody rigidbody;
    [HideInInspector]
    public Transform transform;
    FlockingController flockingController;// = Controller.GetComponent<FlockingController>();
    
    private void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        StartCoroutine (nameof(BoidSteering));
    }
 
    IEnumerator BoidSteering ()
    {
        while (true)
        {
            if (initialised)
            {
                rigidbody.velocity = rigidbody.velocity + Calc () * Time.deltaTime;
 
                // enforce minimum and maximum speeds for the boids
                float speed = rigidbody.velocity.magnitude;
                if (speed > maxVelocity)
                {
                    rigidbody.velocity = rigidbody.velocity.normalized * maxVelocity;
                }
                else if (speed < minVelocity)
                {
                    rigidbody.velocity = rigidbody.velocity.normalized * minVelocity;
                }

               // transform.rotation = chasee.transform.rotation;
            }
 
            float waitTime = Random.Range(0.3f, 0.5f);
            yield return new WaitForSeconds (waitTime);
        }
    }
 
    private Vector3 Calc ()
    {
        Vector3 randomize = new Vector3 ((Random.value *2) -1, (Random.value * 2) -1, (Random.value * 2) -1);
 
        randomize.Normalize();
        
        Vector3 flockCenter = flockingController.flockCenter;
        Vector3 flockVelocity = flockingController.flockVelocity;
        Vector3 follow = chasee.transform.localPosition;
 
        flockCenter = flockCenter - transform.localPosition;
        flockVelocity = flockVelocity - rigidbody.velocity;
        follow = follow - transform.localPosition;
 
        return (flockCenter + flockVelocity + follow * 2 + randomize * randomness);
    }
 
    public void SetController (GameObject theController)
    {
        Controller = theController;
        flockingController = Controller.GetComponent<FlockingController>();
        minVelocity = flockingController.minVelocity;
        maxVelocity = flockingController.maxVelocity;
        randomness = flockingController.randomness;
        chasee = flockingController.chasee;
        initialised = true;
    }

    public Vector3 BoidVelocity()
    {

        return rigidbody.velocity;
    }
}
