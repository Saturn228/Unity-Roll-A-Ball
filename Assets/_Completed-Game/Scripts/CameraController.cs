using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    /* Public Variables */

	// Player game object, so we can refer to it's Transform
	public GameObject player;

    // Store speed of camera movement
    public float speed;

    // Store rotation angle
    public float rotateAngle;

    /* Private Variables */

    // Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
    private Vector3 offset;

    // Store desired camera offset distance from player
    private float radius;

    // At the start of the game..
    void Start ()
	{
		// Create an offset by subtracting the Camera's position from the player's position and set that as desired starting radius
		offset = transform.position - player.transform.position;
        radius = offset.magnitude;

        // Angle of rotation convert to radians
        //rotateAngle = rotateAngle*Mathf.PI / 180;
	}

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    void LateUpdate()
    {
        
        // Zoom in or out
        if ((offset.y > 5 && Input.mouseScrollDelta.y > 0) || (offset.y < 10 && -Input.mouseScrollDelta.y > 0))
        {
            // Create scroll delta vector
            Vector3 scrolldata = new Vector3(0, -Input.mouseScrollDelta.y * speed, Input.mouseScrollDelta.y * speed);

            // Update camera offset
            offset += scrolldata;

            // Update desired radius value
            if (Input.mouseScrollDelta.y > 0)
                radius += -scrolldata.magnitude;    
            else
                radius += scrolldata.magnitude;

        }
        
        // Rotate Camera
        if (Input.GetAxis("Mouse X") != 0)
        {
            // Rotate camera around player
            offset = Quaternion.AngleAxis(-rotateAngle * Input.GetAxis("Mouse X") * speed, Vector3.up) * offset;
            
        }
        
        // Camera rotation using keyboard
        /*
        // Rotate right
        if (Input.GetKeyDown("e"))
        {
            // Rotate camera counterclockwise around player
            offset = Quaternion.AngleAxis(-rotateAngle, Vector3.up) * offset;
            
        }

        // Rotate left
        if (Input.GetKeyDown("q"))
        {
            
            // Rotate camera counterclockwise around player
            offset = Quaternion.AngleAxis(rotateAngle, Vector3.up) * offset;
            
        }
    */

        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount
        transform.position = player.transform.position + offset;

        // Check if distance is at target radius
        if (offset.magnitude != radius)
        {

            // The step size is equal to difference between ideal and actual distances times speed times frame time.
            float step = (offset.magnitude - radius) * speed * Time.deltaTime;

            // Move our position a step closer to the target.
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, step);

        }

        // Look at player object
        transform.LookAt(player.transform);

        
    }
}