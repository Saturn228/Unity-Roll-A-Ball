using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    // Public variables
    public GameObject player;
    public float speed;

    // Private variables
    private Rigidbody rb;
    private float radius;
    private float offset;

    // Use this for initialization
    void Start () {

        // Get player rigidbody component
	     rb = player.GetComponent<Rigidbody>();

        // Set orbiting radius of object
         radius = 2.0f;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        // Find distance between object and player
        offset = (transform.position - player.transform.position).magnitude;
        
        // Check if distance is at target radius
        if (offset != radius)
        {
            
            // The step size is equal to difference between ideal and actual distances times speed times frame time.
            float step = (offset - radius) * speed * Time.deltaTime;
            
            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);
            
        }
        
        // Spin the object around the player object
        transform.RotateAround(player.transform.position, Vector3.up, (20 + 10*rb.velocity.magnitude) * Time.deltaTime);
        
    }
}
