using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class MidiManager : MonoBehaviour
{
    public static int[] notesStates = new int[128];

    // Start is called before the first frame update
    void Start()
    {   
        for(int i = 0; i < 128; i++)
            notesStates[i] = 0;

        MidiMaster.noteOnDelegate += MidiKeyOn;
        MidiMaster.noteOffDelegate += MidiKeyOff;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MidiKeyOn(MidiChannel channel, int note, float velocity)
    {
        Debug.Log("NoteOn: " + channel + "," + note + "," + velocity);
        //notesStates[note] = velocity;
    }

    void MidiKeyOff(MidiChannel channel, int note)
    {
        notesStates[note] = 0;
    }

    
}
