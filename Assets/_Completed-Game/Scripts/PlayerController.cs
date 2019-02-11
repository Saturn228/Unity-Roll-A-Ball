using UnityEngine;

// Include the namespace required to use Unity UI
using System.Collections;

public class PlayerController : MonoBehaviour {

	/* Public Variables */

    // Timer for player message text clearing
    public double playerMessageTimer;

    // Player speed
    public float speed;

    // Jumping force
    public float JumpForce;

    // Camera object
    public GameObject camera;

    // Count of pick up objects picked up so far
    public int count;

    // Is player finished level
    public bool isFinished;

    // Player game object
    public GameObject Canvas;

    ///////////////////////////////////////

    /* Private Variables */

    // UI controller script
    UIController UIControllerScript;

    // Rigidbody component on the player
    private Rigidbody rb;
    
    // Jump controlling variable
    private bool hasjumped;
    
	// At the start of the game..
	void Start ()
	{
        // Retrieve player controller script
        UIControllerScript = Canvas.GetComponent<UIController>();

        // Assign the Rigidbody component to our private rb variable
        rb = GetComponent<Rigidbody>();

		// Set the count to zero and timer to 5s 
		count = 0;
        playerMessageTimer = Time.time + 5.0;
        
        // Set jump controlling and level finished bool to false
        hasjumped = false;
        isFinished = false;
    }
    
    // Each physics step..
    void FixedUpdate ()
	{
        
        // Set some local float variables equal to the value of our Horizontal and Vertical Inputs
        float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
       
        //camera forward and right vectors:
        Vector3 forward = camera.transform.forward;
        Vector3 right = camera.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        
        // Create a Vector3 variable, and assign X and Z to feature our horizontal and vertical float variables above
        Vector3 movement = forward * moveVertical + right * moveHorizontal;

        // Check if player can and wants to jump
        if (Input.GetKeyDown("space") && hasjumped == false)
        {
            Jump(rb, JumpForce);
        }

        // Add a physical force to our Player rigidbody using our 'movement' Vector3 above, 
        // multiplying it by 'speed' - our public player speed that appears in the inspector
        rb.AddForce (movement * speed);
	}

	// When this game object intersects a collider with 'is trigger' checked, 
	// store a reference to that collider in a variable named 'other'..
	void OnTriggerEnter(Collider other) 
	{
        /*
		// ..and if the game object we intersect has the tag 'Pick Up' assigned to it..
		if (other.gameObject.CompareTag ("Pick Up"))
		{
			// Make the other game object (the pick up) inactive, to make it disappear
			other.gameObject.SetActive (false);

			// Add one to the score variable 'count'
			count = count + 1;

			// Run the 'SetCountText()' function (see below)
			SetCountText ();
		}
        */

        // if other game object is follower
        if (other.gameObject.CompareTag("Pick Up"))
        {
            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            UIControllerScript.SetCountText(count.ToString());

            // Destroy pick up object
            other.gameObject.SetActive(false);

        }
        
        // ..and if the game object we intersect has the tag 'Jump Pad' assigned to it..
        if (other.gameObject.CompareTag("Jump Pad"))
        {
            // Applies jump to player object and extra force to counteract momentum of dropping on jump pad
            Jump(rb, JumpForce + 2*rb.velocity.magnitude);

            // Allow player object to jump after landing on jump pad
            hasjumped = false;
            
        }

        // if other game object is the finish platform
        if (other.gameObject.CompareTag("Finish"))
        {
            // Check if all pick ups collected
            if (count >= 4)
            {
                // Player is finished level
                isFinished = true;
            }
            else
            {
                // Tell player to collect all pick ups
                UIControllerScript.SetPlayerText("Need to collect all pick ups to finish");
                playerMessageTimer = Time.time + 5.0;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // if other game object is ground
        if (other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Wall"))
        {
            // Player has landed and may jump again
            hasjumped = false;
        }
    }
    
    // Player object jumps vertically
    void Jump(Rigidbody rb, float JumpForce)
    {
        // removes ability to jump after jumping
        hasjumped = true;

        // Apply force on player to jump
        rb.AddForce(Vector3.up * JumpForce * speed);       
    }

}