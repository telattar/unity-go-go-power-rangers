using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScreen : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text finalScore;
    
    public void setup(int score)
    {
        gameObject.SetActive(true);
        finalScore.text = "Your final score is: " + score;
    }

    public void mainMenu()
    {
        //load the title screen
        SceneManager.LoadScene(0);
    }

    public void restart()
    {
        //load the game scene
        SceneManager.LoadScene(1);

    }
}
