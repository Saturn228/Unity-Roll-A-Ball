using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class FileController : MonoBehaviour {

    // Player game object
    public GameObject Canvas;

    /* Private Variables */

    // UI controller script
    UIController UIControllerScript;

    // Path to best time text file and variable to store name of levels
    string FileName;
    string DirectoryPath;
    string CurrentName;

    // Current Level
    Scene CurrentScene;

    // Use this for initialization
    void Start () {

        // Retrieve player controller script
        UIControllerScript = Canvas.GetComponent<UIController>();

        // Set path to best time text file
        DirectoryPath = "Assets/Resources/";
        FileName = "besttime.txt";

        // Get current level
        CurrentScene = SceneManager.GetActiveScene();

        // Set the text for best time for the level to no time
        UIControllerScript.bestTimeText.text = "Best Time: --.-----";
        
        // Try to open best time text file
        try
        {
            using (StreamReader reader = new StreamReader(DirectoryPath + FileName))
            {

                // Find the current level within best time text file
                while ((CurrentName = reader.ReadLine()) != null)
                {

                    // If level is found within best time text file
                    if (isSameScene())
                    {
                        // Display best time for level
                        UIControllerScript.bestTimeText.text = "Best " + reader.ReadLine();
                        break;
                    }
                }
            }
        }

        // File does not exist so best time text stays at no time
        catch (FileNotFoundException)
        {
            // Do nothing
        }

        // Directory does not exist so 
        catch (DirectoryNotFoundException)
        {
            Directory.CreateDirectory(DirectoryPath);
        }
    }
    
    public void SaveBestTime()
    {
        // Try to open best time text file
        try
        {
            using (StreamReader reader = new StreamReader(DirectoryPath + FileName))
            {

                // Create variable to store contents of best time text file
                string BestTimeContentsBefore = "";

                // Find the current level within best time text file
                do
                {
                    // Read next line in file
                    CurrentName = reader.ReadLine();

                    // Add currently read line to best time contents
                    BestTimeContentsBefore += CurrentName;

                    // If level is found within best time text file
                    if (isSameScene())
                    {
                        // Save previous best time for current level
                        string pastbesttime = reader.ReadLine();

                        // Convert time strings to doubles
                        double pastTime, newTime;

                        string[] temp1 = pastbesttime.Split(' ');
                        string[] temp2 = UIControllerScript.timerText.text.Split(' ');

                        double.TryParse(temp1[1], out pastTime);
                        double.TryParse(temp2[1], out newTime);

                        // Check if new best time was achieved
                        if (pastTime > newTime)
                        {
                            // Save all content in best time text file after new time
                            string BestTimeContentsAfter = reader.ReadToEnd();

                            // Close reader to allow writing to best time text file
                            reader.Close();

                            // Write updated besttime.txt file
                            StreamWriter writer = new StreamWriter(DirectoryPath + FileName);

                            writer.WriteLine(BestTimeContentsBefore);
                            writer.WriteLine(UIControllerScript.timerText.text);
                            writer.WriteLine(BestTimeContentsAfter);
                            writer.Close();
                        }
                        break;
                    }
                } while ((CurrentName = reader.ReadLine()) != null);

                // If current level is not found
                if (CurrentName == null)
                {
                    // Close read stream for best time text file
                    reader.Close();

                    // Write current time and level to the besttime.txt file
                    StreamWriter writer = new StreamWriter(DirectoryPath + FileName);

                    writer.WriteLine(BestTimeContentsBefore);
                    writer.WriteLine("Level: " + CurrentScene.name);
                    writer.WriteLine(UIControllerScript.timerText.text);
                    writer.Close();
                }
            }
        }

        // File does not exist so new best time text file to be created
        catch (FileNotFoundException)
        {
            StreamWriter writer = new StreamWriter(DirectoryPath + FileName);
            
            // Write name of level and best time to file
            writer.WriteLine("Level: " + CurrentScene.name);
            writer.WriteLine(UIControllerScript.timerText.text);
            writer.Close();
        }
    }

    // Check if current scene is same as selected scene
    bool isSameScene()
    {
        return CurrentName == "Level: " + CurrentScene.name;        
    }
}


