using UnityEngine;
using System.Collections;
using System;

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

    // Store default camera zoom distance
    private float CameraDefaultZoom;

    // Store Input.mouseScrollDelta.y data
    private float ScrollData;

    // At the start of the game..
    void Start ()
	{
		// Create an offset by subtracting the Camera's position from the player's position and set that as desired starting radius
		offset = transform.position - player.transform.position;

        // Initialize radius value and default zoom distance
        radius = offset.magnitude;
        CameraDefaultZoom = radius;

        // Initialize to no scroll motion detected
        ScrollData = 0;

        // Make cursor invisible and lock to centre of screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
        // Angle of rotation convert to radians
        //rotateAngle = rotateAngle*Mathf.PI / 180;
    }

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    void LateUpdate()
    {
        // Assign scroll activity to ScrollData
        ScrollData = Input.mouseScrollDelta.y;

        // Zoom in or out
        if ((offset.y > 5 && ScrollData > 0) || (offset.y < 10 && -ScrollData > 0))
        {
            // Apply zoom to camera
            Zoom(ref offset, ref radius, ScrollData);

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

        // Reset camera to default zoom distance
        if (Input.GetMouseButtonDown(2))
        {
            // Zoom mouse until default zoom distance is reached
            while (Math.Abs(radius - CameraDefaultZoom) > 0.1)
            {
                
                // Apply zoom to camera with step size moving towards default zoom distance
                Zoom(ref offset, ref radius, (radius - CameraDefaultZoom) * speed * Time.deltaTime);
                
            }

        }

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

        // Reset ScrollData to default
        ScrollData = 0;
    }

    // Apply zoom to camera
    void Zoom(ref Vector3 offset, ref float radius, float ScrollData)
    {
        
        // Create scroll delta vector
        Vector3 scrollvector = new Vector3(0, -ScrollData * speed, ScrollData * speed);

        // Update camera offset
        offset += scrollvector;

        // Update desired radius value
        if (ScrollData > 0)
            radius += -scrollvector.magnitude;
        else
            radius += scrollvector.magnitude;
        
    }
    
}