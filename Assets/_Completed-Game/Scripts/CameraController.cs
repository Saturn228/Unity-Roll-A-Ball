using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// store a public reference to the Player game object, so we can refer to it's Transform
	public GameObject player;
    public float speed;
    public float rotateAngle;

    // Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
    private Vector3 offset;
    private Vector3 rotateValue;
    private float radius;
    private float oldx;

    // At the start of the game..
    void Start ()
	{
		// Create an offset by subtracting the Camera's position from the player's position
		offset = transform.position - player.transform.position;
        
        // Angle of rotation convert to radians
        rotateAngle = rotateAngle*Mathf.PI / 180;
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

        if (true)
        {
            // Rotate right
            if (Input.GetKeyDown("e"))
            {
                //offset = Quaternion.AngleAxis(rotateAngle, Vector3.up) * offset;


                // Convert point of rotation to origin
                offset += player.transform.position;
                oldx = offset.x;

                // Apply camera transform rotation
                offset.x = (offset.x * Mathf.Cos(rotateAngle) + offset.z * Mathf.Sin(rotateAngle)) / (Mathf.Pow(Mathf.Sin(rotateAngle), 2) + Mathf.Pow(Mathf.Cos(rotateAngle), 2));
                offset.z = (offset.x * Mathf.Cos(rotateAngle) - oldx) / Mathf.Sin(rotateAngle);

                // Revert point of rotation from origin
                offset -= player.transform.position;

                transform.LookAt(player.transform.position);

                /*
                // Rotate camera angle
                rotateValue = new Vector3(0, rotateAngle*180/Mathf.PI, 0);
                transform.eulerAngles = transform.eulerAngles - rotateValue;*/

            }

            // Rotate left
            if (Input.GetKeyDown("q"))
            {

                offset = Quaternion.AngleAxis(-rotateAngle, Vector3.up) * offset;

                transform.LookAt(player.transform.position);



                /*
                            offset.x = transform.position.x * Mathf.Cos(rotateAngle) - transform.position.z * Mathf.Sin(rotateAngle);
                            offset.z = transform.position.x * Mathf.Cos(rotateAngle) + transform.position.z * Mathf.Sin(rotateAngle);

                            // Rotate camera angle
                            rotateValue = new Vector3(0, - rotateAngle * 180 / Mathf.PI, 0);
                            transform.eulerAngles = transform.eulerAngles - rotateValue;
                            */
            }





        }


      
        // Set the position of the Camera (the game object this script is attached to)
        // to the player's position, plus the offset amount
        transform.position = player.transform.position + offset;


    }
}