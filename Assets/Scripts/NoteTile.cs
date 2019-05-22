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

        if (note == 53)
            noteText.text += "/E";
        if (note == 55)
            noteText.text += "/R";
        if (note == 57)
            noteText.text += "/T";
    }

    // Update is called once per frame
    void Update()
    {
        anchor.LookAt(PlayerMovements.player.transform);
    }
}
