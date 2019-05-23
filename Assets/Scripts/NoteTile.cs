using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteTile : MonoBehaviour
{
    public int note { get; set; }
    public bool hasBeenHit { get; set; }

    public Text noteText;
    public Transform anchor;

    public static string[] notes = new string[12] { "A", "A#", "B", "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#" };

    // Start is called before the first frame update
    void Start()
    {
        noteText.text = notes[(note - 21) % 12] + (note - 21) / 12;

        if (note == 49)
            noteText.text += "/A";
        if (note == 50)
            noteText.text += "/Z";
        if (note == 52)
            noteText.text += "/E";
        if (note == 53)
            noteText.text += "/R";
        if (note == 55)
            noteText.text += "/T";
        if (note == 57)
            noteText.text += "/Y";
        if (note == 59)
            noteText.text += "/U";
        if (note == 60)
            noteText.text += "/I";
        if (note == 62)
            noteText.text += "/O";
        if (note == 64)
            noteText.text += "/P";
    }

    // Update is called once per frame
    void Update()
    {
        anchor.LookAt(PlayerMovements.player.transform);
    }
}
