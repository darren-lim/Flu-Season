using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class D_VolumeSliderFill : MonoBehaviour
{
    public Slider VolumeSlider;

    private void OnEnable()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
    }
}
