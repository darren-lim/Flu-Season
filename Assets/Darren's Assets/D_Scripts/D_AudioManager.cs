using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class D_AudioManager : MonoBehaviour
{
    //public AudioMixer audioGroup;
    //public AudioSource music;
    public Slider slider;
    public D_Sound[] sounds;
    public List<AudioSource> sources;
    public static D_AudioManager current;

    private void Awake()
    {
        if (current == null)
            current = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);

        foreach(D_Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = PlayerPrefs.GetFloat("Volume", 1);
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = false;
            sources.Add(s.source);
        }
    }

    private void Start()
    {
        Play("Theme");
        if (slider == null)
            return;
        slider.value = PlayerPrefs.GetFloat("Volume", 1);
    }

    public void Play(string name)
    {
        //D_Sound s = Array.Find(sounds, sound => sound.name == name);
        AudioSource a = null;
        for (int i = 0; i<sounds.Length; i++)
        {
            if(sounds[i].name == name)
            {
                a = sources[i];
                break;
            }
        }
        if (a == null)
        {
            Debug.Log("Sound name not found");
            return;
        }
        a.Play();
    }

    public void SetVolumeToSlider()
    {
        if (slider == null)
            return;
        PlayerPrefs.SetFloat("Volume", slider.value);
        foreach (D_Sound s in sounds)
        {
            s.source = this.gameObject.GetComponent<AudioSource>();
            s.source.volume = PlayerPrefs.GetFloat("Volume", 1);
        }
    }
}
