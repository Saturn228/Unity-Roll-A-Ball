using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    /* Public Variables */

    // Store vector to travel along
    public Vector3 travelVector;

    // Store speed of object movement
    public float speed;

    /* Private Variables */

    // Store the start position of object
    private Vector3 startPosition;

    // Store step size of movement vector
    private float step;

    // Store destination of object
    private Vector3 destination;

    // Use this for initialization
    void Start () {

        // Set starting position of object
        startPosition = transform.position;

        // Set initial destination of object
        destination = startPosition + travelVector;

        // The step size is equal to difference between current position and final position distances times speed times frame time.
        step = (destination.magnitude - transform.position.magnitude) * speed * Time.deltaTime;
        
    }

    // Update is called once per frame
    void FixedUpdate () {

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, destination, step);
        
        // Has wall reached destination
        if (transform.position == destination)
        {
            // Check if destination is the starting position of object
            if(destination == startPosition)
            {
                // Change destination of object to travel position
                destination += travelVector;
                
            }
            else
            {
                // Change destination of object to start position
                destination -= travelVector;
                
            }

        }

    }
}
