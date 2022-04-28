using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidCameraFollow : MonoBehaviour
{
    public Transform boidController;
 
    void LateUpdate ()
    {
        if (boidController)
        {
            Vector3 watchPoint = boidController.GetComponent<FlockingController>().flockCenter;
            transform.LookAt(watchPoint+boidController.transform.position);
        }
    }
}
