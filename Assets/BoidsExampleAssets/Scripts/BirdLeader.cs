using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdLeader : MonoBehaviour
{
    private Rigidbody rb;

    public float speed = 3.0f;
    public float rotationSpeed = 100.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float rotationX = Input.GetAxis("Vertical") * rotationSpeed;
        float rotationY = Input.GetAxis("Horizontal") * rotationSpeed;
        // Rotate around our x/y-axis
        rotationX *= Time.deltaTime;
        rotationY *= Time.deltaTime;
        transform.Rotate(rotationX, rotationY, 0);
        
        // Make it move 10 meters per second instead of 10 meters per frame...
       

        // Move translation along the object's z-axis
        transform.Translate(0, 0, speed * Time.deltaTime);

        
        
    }
}
