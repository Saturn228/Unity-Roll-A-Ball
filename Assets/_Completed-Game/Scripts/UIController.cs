using System.Collections;
using System.Collections.Generic;

using System;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    /* Public Variables */

    // Text UI game objects
    public Text countText;
    public Text winText;
    public Text playerMessageText;
    public Text timerText;
    public Text bestTimeText;

    // Player game object
    public GameObject player;
    
    // Player game object
    public GameObject filescript;

    /* Private Variables */

    // Player controller script
    PlayerController playerControllerScript;

    // File controller script
    FileController fileControllerScript;

    // Use this for initialization
    void Start () {

        // Run the SetCountText function to update the UI (see below)
        SetCountText("0");

        // Set the text property of our Win Text and Player Message Text UI to an empty string, making the 'You Win' (game over message) blank
        winText.text = "";

        // Retrieve player controller script
        playerControllerScript = player.GetComponent<PlayerController>();

        // Retrieve player controller script
        fileControllerScript = filescript.GetComponent<FileController>();

        // Tell player goals for level
        playerMessageText.text = "Collect all the gold pick ups and enter the red platform";

    }
    
    // Update is called once per frame
    void Update () {
        
        // Clear player message box
        if (Time.time > playerControllerScript.playerMessageTimer)
        {
            playerMessageText.text = "";
        }

        // Update time displayed
        if (!playerControllerScript.isFinished)
        {
            timerText.text = string.Format("Time: {0}", Time.timeSinceLevelLoad);
        }

        // Tell player they have finished level
        if (playerControllerScript.isFinished && winText.text != "You Win!")
        {
            // Set the text value of our 'winText'
            winText.text = "You Win!";

            // Check if best time is surpassed
            fileControllerScript.SaveBestTime();
        }
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    public void SetCountText(string text)
    {
        // Update the text field of our 'countText' variable
        countText.text = "Count: " + text;
    }

    // Create a standalone function that can update the 'countText' UI and check if the required amount to win has been achieved
    public void SetPlayerText(string text)
    {
        // Update the text field of our 'countText' variable
        playerMessageText.text = text;        
    }
}
