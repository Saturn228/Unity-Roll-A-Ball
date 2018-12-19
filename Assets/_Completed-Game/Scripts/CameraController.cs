using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// store a public reference to the Player game object, so we can refer to it's Transform
	public GameObject player;

    // Store speed of camera movement
    public float speed;

    // Store rotation angle
    public float rotateAngle;

    // Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
    private Vector3 offset;

    // At the start of the game..
    void Start ()
	{
		// Create an offset by subtracting the Camera's position from the player's position
		offset = transform.position - player.transform.position;
        
        // Angle of rotation convert to radians
        //rotateAngle = rotateAngle*Mathf.PI / 180;
	}

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    void LateUpdate()
    {
        
        // Zoom in
        if (offset.y > 5 && Input.mouseScrollDelta.y > 0)
        {
            offset.y += -Input.mouseScrollDelta.y * speed;
            offset.z += Input.mouseScrollDelta.y * speed;

        }

        // Zoom out
        if (offset.y < 10 && -Input.mouseScrollDelta.y > 0)
        {
            offset.y += -Input.mouseScrollDelta.y * speed;
            offset.z += Input.mouseScrollDelta.y * speed;

        }

        // Rotate right
        if (Input.GetKeyDown("e"))
        {
            // Rotate camera counterclockwise around player
            offset = Quaternion.AngleAxis(-rotateAngle, Vector3.up) * offset;

            // Look at player object
            transform.LookAt(player.transform.position);

        }

        // Rotate left
        if (Input.GetKeyDown("q"))
        {
                
            // Rotate camera counterclockwise around player
            offset = Quaternion.AngleAxis(rotateAngle, Vector3.up) * offset;

            // Look at player object
            transform.LookAt(player.transform.position);
                
        }
    
        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount
        transform.position = player.transform.position + offset;


    }
}