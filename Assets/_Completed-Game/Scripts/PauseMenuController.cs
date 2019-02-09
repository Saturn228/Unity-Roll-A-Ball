using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

    /* Public Variables */

    // Camera object
    public new GameObject Camera;

    // Pause Menu
    public GameObject PauseMenu;

    // Pause Menu
    public GameObject OptionsMenu;
    
    // Use this for initialization
    void Start () {

        // Make cursor invisible and lock to centre of screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Hide pause and options menu
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
    }

    void Update()
    {
        // Check if player wants to pause/unpause game
        if (Input.GetKeyDown("escape"))
        {
            Pause();

        }
    }

    // Pause / unpause game
    public void Pause()
    {
        // Check if game is currently paused
        if (PauseMenu.activeSelf == false)
        {
            // Pause game physics
            Time.timeScale = 0;
            Camera.GetComponent<CameraController>().enabled = false;

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
            Camera.GetComponent<CameraController>().enabled = true;

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
            // Show options menu
            OptionsMenu.SetActive(true);

            // Hide pause menu
            PauseMenu.SetActive(false);

        }
        else
        {
            // Hide options menu
            OptionsMenu.SetActive(false);

            // Show pause menu
            PauseMenu.SetActive(true);
        }


    }
}
