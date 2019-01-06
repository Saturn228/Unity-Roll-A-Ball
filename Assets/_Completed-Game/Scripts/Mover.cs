using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    /* Public Variables */

    // Store vector to travel along
    public Vector3 travelVector;

    // Store speed of wall movement
    public float speed;

    /* Private Variables */

    // Store the start position of the wall
    private Vector3 startPosition;

    // Store step size of movement vector
    private float step;
    
    // Use this for initialization
    void Start () {

        // Set starting position of wall
        startPosition = transform.position;

        // The step size is equal to difference between current position and final position distances times speed times frame time.
        step = (travelVector.magnitude + startPosition.magnitude - transform.position.magnitude) * speed * Time.deltaTime;
        
    }

    // Update is called once per frame
    void Update () {

        // Move our position a step closer to the target.
        transform.position = Vector3.MoveTowards(transform.position, startPosition + travelVector, step);
        
        // Has wall reached destination or starting position
        if (transform.position == startPosition + travelVector || transform.position == startPosition)
        {
            // Change direction of travel
            step *= -1;
            
        }
        
    }
}
