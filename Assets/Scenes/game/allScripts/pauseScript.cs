using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseScript : MonoBehaviour
{
    public GameObject pausePanel;
    public static bool paused;

    public void pauseResume()
    {
        playerScript.resumeClicked = true;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void pauseBackToMainMenu()
    {
        //load the title screen
        SceneManager.LoadScene(0);
    }
    public void pauseRestart()
    {
        //load the game scene
        SceneManager.LoadScene(1);
    }
}
