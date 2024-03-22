using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public sound[] sounds;
    void Start()
    {
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
        }
    }

    public void playSound(string name)
    {
        //do not play the sound if the mute toggle is activated
        if (muteMgr.mute)
            return;

        foreach (sound s in sounds)
        {
            if (s.soundName == name)
                s.source.Play();           
        }
    }
    public void stopSound(string name)
    {
        foreach (sound s in sounds)
        {
            if (s.soundName == name)
                s.source.Stop();
        }
    }
}
