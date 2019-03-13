using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D_VolumeSliderFill : MonoBehaviour
{
    public Slider VolumeSlider;
    public D_AudioManager aMan;
    // Start is called before the first frame update
    void Start()
    {
        if (VolumeSlider == null)
            return;
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume", 1);
        aMan = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<D_AudioManager>();
    }

    public void SetVolumeToSlider()
    {
        if (aMan == null)
            return;
        aMan.SetVolume(VolumeSlider.value);
    }
    private void OnEnable()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        aMan = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<D_AudioManager>();
    }
}
