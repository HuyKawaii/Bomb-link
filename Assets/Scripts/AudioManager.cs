using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public Sound[] sounds;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            AudioManager.instance = this;
            foreach (Sound sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();

                sound.source.volume = sound.volume;
                sound.source.clip = sound.clip;
                sound.source.name = sound.name;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);

        if(sound != null)
            sound.source.Play();
    }
}
