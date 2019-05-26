using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : Menu
{
    public Slider musicSlider;
    public Slider soundSlider;
    public Button backButton;

    public Menu parent;

    public AudioMixer audioMixer;

    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(OnClickBack);

        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        soundSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnClickBack()
    {
        Toggle();
        parent.Toggle();
    }

    private void SetMusicVolume(float v)
    {
        if (v < -18) v = -100;
        audioMixer.SetFloat("MusicVolume", v);
    }

    private void SetSFXVolume(float v)
    {
        if (v < -18) v = -100;
        audioMixer.SetFloat("SoundsVolume", v);
    }
}
