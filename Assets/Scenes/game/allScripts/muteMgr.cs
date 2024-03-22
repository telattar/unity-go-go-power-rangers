using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class muteMgr : MonoBehaviour
{
    public static bool mute;

    private void Start()
    {
        
    }

    public void toggleMute(bool muted)
    {
        if (muted)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            mute = true;
        }
        else
        {
            gameObject.GetComponent<AudioSource>().Play();
            mute = false;
        }
    }

}
