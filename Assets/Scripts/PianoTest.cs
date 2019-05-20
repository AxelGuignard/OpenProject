using MidiJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PianoTest : MonoBehaviour
{
    private AudioSource[] audioSource = new AudioSource[128];
    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;
    private int pos;

    public static PianoTest instance;
    public bool init;

    // Start is called before the first frame update
    void Start()
    {
        init = true;
        instance = this;
        pos = 0;
        for(int i = 0; i < 12; i++)
        {
            audioSource[i] = gameObject.AddComponent<AudioSource>();
            audioSource[i].playOnAwake = false;
            audioSource[i].clip = clip;
            audioSource[i].outputAudioMixerGroup = audioMixerGroup;
            //audioSource[i].pitch = 0.075f * i - 3.625f;
            //audioSource[i].pitch = 0.0186f*Mathf.Exp(0.0578f*i);
        }
        MidiMaster.noteOnDelegate += MidiKeyOn;
        MidiMaster.noteOffDelegate += MidiKeyOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MidiKeyOn(MidiChannel channel, int note, float velocity)
    {
        if (velocity > 0f)
        {
            //Debug.Log("NoteOn: " + channel + "," + note + "," + velocity + "," + (0.0186f * Mathf.Exp(0.0578f * note)));
            playNote(note);
        }
        else
        {

        }
    }

    public void playNote(int note)
    {
        if (init)
        {
            audioSource[pos % 12].pitch = 0.0186f * Mathf.Exp(0.0578f * note);
            audioSource[pos % 12].Play();
            pos++;
        }
        init = true;
    }

    public void playNote2(int note)
    {
        if (init)
        {
            audioSource[0].pitch = 0.0186f * Mathf.Exp(0.0578f * note);
            audioSource[0].Play();
            pos++;
        }
        init = true;
    }

    void MidiKeyOff(MidiChannel channel, int note)
    {
        //audioSource[note].Stop();
    }
}
