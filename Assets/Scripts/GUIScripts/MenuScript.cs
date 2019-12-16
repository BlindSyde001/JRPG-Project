using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    //VARIABLES
    public static bool GameIsPaused = false;
    public GameObject optionsMenu;
    public GameObject battleMenu;
    public Button itemButton;

    private CameraControls cameraRig;

    //UPDATES
    private void Awake()
    {
        cameraRig = FindObjectOfType<CameraControls>();
    }
    private void LateUpdate()
    {
        Menu();
    }

    #region Menu Stuff
    void Menu()
    {
       bool menuPressed = Input.GetButtonDown("Menu Key");

        if (menuPressed)
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    //METHODS
    void Resume()
    {
        Debug.Log("resume");
        optionsMenu.SetActive(false);
        Time.timeScale = 1;
        cameraRig.GetComponent<CameraControls>().enabled = true;
        GameIsPaused = false;
    }
    void Pause()
    {
        Debug.Log("pause");
        optionsMenu.SetActive(true);
        cameraRig.GetComponent<CameraControls>().enabled = false;
        Time.timeScale = 0;
        GameIsPaused = true;
        itemButton.Select();

    }
    #endregion
}
