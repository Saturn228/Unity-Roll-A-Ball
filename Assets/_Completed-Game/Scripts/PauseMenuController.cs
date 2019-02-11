using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

    /* Public Variables */

    // Camera object
    public GameObject Camera;

    // Pause Menu
    public GameObject PauseMenu;

    // Options Menu
    public GameObject OptionsMenu;

    // Controls Menu
    public GameObject ControlsMenu;

    // Video Menu
    public GameObject VideoMenu;

    // Audio Menu
    public GameObject AudioMenu;

    // Audio Mixer
    public AudioMixer AudioMixer;

    // Resolution dropdown menu
    public Dropdown ResolutionDropDown;
    
    /* Private Variables */

    // Stores supported resolutions for selected monitor
    private Resolution[] resolutions;

    // Audio sources
    private AudioSource[] audioSourceList;

    // Use this for initialization
    void Start () {

        // Retrieve audiosources in game
        audioSourceList = GetComponents<AudioSource>();

        // Retrieve list of supported resolutions
        resolutions = Screen.resolutions;

        // Clear resolution dropdown list
        ResolutionDropDown.ClearOptions();

        // Create empty list of options
        List<string> options = new List<string>();

        // Create variable to store current resolution
        int ResolutionIndex = 0;

        // Fill options list
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + "x" + resolutions[i].height);

            // Store index of current resolution in dropdown list
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                ResolutionIndex = i;
            
        }
        
        // Add supported resolutions to dropdown list and set current resolution as displayed setting
        ResolutionDropDown.AddOptions(options);
        ResolutionDropDown.value = ResolutionIndex;
        ResolutionDropDown.RefreshShownValue();

        // Make cursor invisible and lock to centre of screen
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Hide all menus
        PauseMenu.SetActive(false);
        OptionsMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        VideoMenu.SetActive(false);
        AudioMenu.SetActive(false);
    }

    void Update()
    {
        // Check if player wants to pause/unpause game
        if (Input.GetKeyDown("escape"))
        {
            PauseOpen();

        }
    }

    // Pause / unpause game
    public void PauseOpen()
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

            // Turn pause menu music on and background music off
            audioSourceList[0].Play();
            audioSourceList[1].Pause(); 
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

            // Turn pause menu music off and background music on
            audioSourceList[0].Pause();
            audioSourceList[1].Play();
        }
        
    }

    // Open / close options menu
    public void OptionsOpen()
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
    
    // Open / close controls menu
    public void ControlsOpen()
    {
        // Check if game is currently paused
        if (ControlsMenu.activeSelf == false)
        {
            // Show options menu
            ControlsMenu.SetActive(true);

            // Hide pause menu
            OptionsMenu.SetActive(false);

        }
        else
        {
            // Hide options menu
            ControlsMenu.SetActive(false);

            // Show pause menu
            OptionsMenu.SetActive(true);
        }


    }

    // Open / close video menu
    public void VideoOpen()
    {
        // Check if game is currently paused
        if (VideoMenu.activeSelf == false)
        {
            // Show options menu
            VideoMenu.SetActive(true);

            // Hide pause menu
            OptionsMenu.SetActive(false);

        }
        else
        {
            // Hide options menu
            VideoMenu.SetActive(false);

            // Show pause menu
            OptionsMenu.SetActive(true);
        }


    }
    
    // Set screen resolution
    public void VideoSetResolution(int ResolutionIndex)
    {
        // Set screen resolution
        Screen.SetResolution(resolutions[ResolutionIndex].width, resolutions[ResolutionIndex].height, Screen.fullScreen);

    }

    // Set Anti-Aliasing
    public void VideoSetAA(int AAIndex)
    {
        // Set AA according to selected setting
        switch (AAIndex)
        {
            case 0:
                QualitySettings.antiAliasing = 0;
                break;

            case 1:
                QualitySettings.antiAliasing = 1;
                break;

            case 2:
                QualitySettings.antiAliasing = 2;
                break;

            case 3:
                QualitySettings.antiAliasing = 4;
                break;

            case 4:
                QualitySettings.antiAliasing = 8;
                break;

            case 5:
                QualitySettings.antiAliasing = 16;
                break;
                
            default:
                QualitySettings.antiAliasing = 0;
                break;
        }
        
    }

    // Toggle fullscreen mode
    public void VideoSetFullscreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    // Toggle V-Sync
    public void VideoSetVsync(bool isVsync)
    {
        if(isVsync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
        
    }

    // Open / close audio menu
    public void AudioOpen()
    {
        // Check if game is currently paused
        if (AudioMenu.activeSelf == false)
        {
            // Show options menu
            AudioMenu.SetActive(true);

            // Hide pause menu
            OptionsMenu.SetActive(false);

        }
        else
        {
            // Hide options menu
            AudioMenu.SetActive(false);

            // Show pause menu
            OptionsMenu.SetActive(true);
        }


    }

    // Set master volume
    public void AudioMaster(float volume)
    {
        AudioMixer.SetFloat("MasterVolume", volume);

    }
    
    // Set music volume
    public void AudioMusic(float volume)
    {
        AudioMixer.SetFloat("MusicVolume", volume);

    }
    
    // Set sound effects volume
    public void AudioSoundEffects(float volume)
    {
        AudioMixer.SetFloat("SoundEffectsVolume", volume);

    }
    
    // Restart current level
    public void Restart()
    {
        SceneManager.LoadSceneAsync("SampleScene", LoadSceneMode.Single);

        // Unpause game physics
        Time.timeScale = 1;
        Camera.GetComponent<CameraController>().enabled = true;

    }

    // Quit game
    public void Quit()
    {
        Application.Quit();

    }

}
