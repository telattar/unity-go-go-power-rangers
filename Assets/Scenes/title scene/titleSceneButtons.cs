using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class titleScreen : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject musicComponent;

    public void quit()
    {
        Application.Quit();
    }

    public void play()
    {
        SceneManager.LoadScene(1);
    }
    
    public void options()
    {
        optionsPanel.SetActive(true);
        //optionsPanel.GetComponentInChildren<Toggle>().isOn = muteMgr.mute ? true : false;
    }
    public void x()
    {
        optionsPanel.SetActive(false);
    }

}
