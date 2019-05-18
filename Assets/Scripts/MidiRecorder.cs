using MidiJack;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MidiRecorder : MonoBehaviour
{
    private List<float[]> track;
    private string fileName = "track1.csv";

    // Start is called before the first frame update
    void Start()
    {
        MidiMaster.noteOnDelegate += MidiKeyOn;
        MidiMaster.noteOffDelegate += MidiKeyOff;

        track = new List<float[]>();
    }

    // Update is called once per frame
    void Update()
    {
        bool stop = Input.GetKeyDown(KeyCode.Space);
        if (stop)
        {
            WriteTrack();
        }
    }

    void MidiKeyOn(MidiChannel channel, int note, float velocity)
    {
        if (velocity > 0f)
        {
            Debug.Log("NoteOn: " + channel + "," + note + "," + velocity + "," + (0.0186f * Mathf.Exp(0.0578f * note)));
            float time = Time.timeSinceLevelLoad;
            track.Add(new float[] { time, note });
        }
        else
        {

        }
    }

    void MidiKeyOff(MidiChannel channel, int note)
    {
        //audioSource[note].Stop();
    }

    void WriteTrack()
    {
        var sr = File.CreateText(fileName);

        for (int i = 0; i < track.Count; i++)
        {
            sr.WriteLine("{0};{1}", track[i][0], (int)track[i][1]);
        }

        sr.Close();

        track = new List<float[]>();

        Debug.Log("track saved");
    }
}
