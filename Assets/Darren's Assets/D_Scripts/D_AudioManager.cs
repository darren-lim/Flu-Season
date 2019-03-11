using UnityEngine;
using System;
using UnityEngine.Audio;
using UnityEngine.UI;

public class D_AudioManager : MonoBehaviour
{
    //public AudioMixer audioGroup;
    //public AudioSource music;
    public Slider slider;
    public D_Sound[] sounds;
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
        }
    }

    private void Start()
    {
        Play("Theme");
    }

    public void Play(string name)
    {
        D_Sound s = Array.Find(sounds, D_Sound => D_Sound.name == name);
        if (s == null)
        {
            Debug.Log("Sound name not found");
            return;
        }
        s.source.Play();
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
