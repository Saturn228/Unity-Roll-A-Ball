using UnityEngine;

// Include the namespace required to use Unity UI
using UnityEngine.UI;

using System.Collections;

public class PlayerController : MonoBehaviour {

	/* Public Variables */

    // Text UI game objects
    public Text countText;
	public Text winText;

    // Player speed
    public float speed;

    // Jumping force
    public float JumpForce;

    // Camera object
    public new GameObject camera;

    // Pause Menu
    public new GameObject PauseMenu;

    // Pause Menu
    public new GameObject OptionsMenu;
    ///////////////////////////////////////

    /* Private Variables */

    // Rigidbody component on the player
    private Rigidbody rb;

    // Count of pick up objects picked up so far
    private int count;

    // Jump controlling variable
    private bool hasjumped;

	// At the start of the game..
	void Start ()
	{
		// Assign the Rigidbody component to our private rb variable
		rb = GetComponent<Rigidbody>();

		// Set the count to zero 
		count = 0;

		// Run the SetCountText function to update the UI (see below)
		SetCountText ();

		// Set the text property of our Win Text UI to an empty string, making the 'You Win' (game over message) blank
		winText.text = "";

        // Set jump controlling bool to false
        hasjumped = false;

        // Hide pause menu
        PauseMenu.SetActive(false);
    }

    void Update()
    {
        // Check if player wants to pause/unpause game
        if (Input.GetKeyDown("escape"))
        {
            Pause();

        }
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

        // ..and if the game object we intersect has the tag 'Jump Pad' assigned to it..
        if (other.gameObject.CompareTag("Jump Pad"))
        {
            // Applies jump to player object and extra force to counteract momentum of dropping on jump pad
            Jump(rb, JumpForce + 2*rb.velocity.magnitude);

            // Allow player object to jump after landing on jump pad
            hasjumped = false;
            
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

        // if other game object is follower
        if (other.gameObject.CompareTag("Follower"))
        {
            // Add one to the score variable 'count'
            count = count + 1;

            // Run the 'SetCountText()' function (see below)
            SetCountText();

        }

    }
    
	// Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
	void SetCountText()
	{
		// Update the text field of our 'countText' variable
		countText.text = "Count: " + count.ToString ();

		// Check if our 'count' is equal to or exceeded 12
		if (count >= 12) 
		{
			// Set the text value of our 'winText'
			winText.text = "You Win!";
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

    // Pause / unpause game
    public void Pause()
    {
        // Check if game is currently paused
        if(PauseMenu.activeSelf == false)
        {
            // Pause game physics
            Time.timeScale = 0;
            camera.GetComponent<CameraController>().enabled = false;

            // Show pause menu
            PauseMenu.SetActive(true);

            // Reveal and unlock cursor
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // Unpause game physics
            Time.timeScale = 1;
            camera.GetComponent<CameraController>().enabled = true;

            // Hide pause menu
            PauseMenu.SetActive(false);

            // Hide and lock cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }


    }

    // Open / close options menu
    public void Options()
    {
        // Check if game is currently paused
        if (OptionsMenu.activeSelf == false)
        {
            // Show pause menu
            OptionsMenu.SetActive(true);
            
        }
        else
        {
            // Hide pause menu
            OptionsMenu.SetActive(false);
            
        }


    }
}