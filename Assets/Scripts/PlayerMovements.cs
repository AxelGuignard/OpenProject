using MidiJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public static int refX;
    private bool isMoving = false;

    private int nextNote;
    private Collider lastCollider;

    public static PlayerMovements player;

    // Start is called before the first frame update
    void Start()
    {
        MidiMaster.noteOnDelegate += MidiKeyOn;
        player = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving)
            transform.position += transform.forward * 0.06f;

        bool k53 = Input.GetKeyDown(KeyCode.E);
        bool k55 = Input.GetKeyDown(KeyCode.R);
        bool k57 = Input.GetKeyDown(KeyCode.T);
        if (k53) hitAKey(53);
        if (k55) hitAKey(55);
        if (k57) hitAKey(57);

        Collider[] cols = Physics.OverlapBox(transform.position + transform.forward * 0.5f, Vector3.one * 0.05f, new Quaternion(), GenerateMap.NoteMask);
        if (cols.Length > 0)
        {
            Collider col = cols[0];
            //Debug.Log(col.name);
            nextNote = col.GetComponentInParent<NoteTile>().note;
            //Debug.Log("next : " + nextNote);
            lastCollider = col;
        }
        else
        {
            if(nextNote != -1)
            {
                if (lastCollider != null)
                {

                    int tmpNote = lastCollider.GetComponentInParent<NoteTile>().note;
                    if (GenerateMap.noteToEvent[tmpNote] == EventType.RightTurn)
                    {
                        transform.Rotate(0, 90, 0);
                    }
                    else if (GenerateMap.noteToEvent[tmpNote] == EventType.LeftTurn)
                    {
                        transform.Rotate(0, -90, 0);
                    }
                    lastCollider.GetComponentInParent<NoteTile>().hasBeenHit = true;
                    lastCollider.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
            {

            }
            nextNote = -1;
        }

    }

    void MidiKeyOn(MidiChannel channel, int note, float velocity)
    {
        if (velocity > 0f)
        {
            hitAKey(note);
        }
        else
        {

        }
    }

    void hitAKey(int note)
    {
        Collider[] cols = Physics.OverlapBox(transform.position + transform.forward * 0.5f, Vector3.one * 0.05f, new Quaternion(), GenerateMap.NoteMask);
        if (cols.Length > 0)
        {
            Collider col = cols[0];
            if (col.GetComponentInParent<NoteTile>().note == note && !col.GetComponentInParent<NoteTile>().hasBeenHit)
            {
                int tmpNote = lastCollider.GetComponentInParent<NoteTile>().note;
                lastCollider.GetComponentInParent<NoteTile>().hasBeenHit = true;
                GenerateMap.score++;
                HUD.SetScore(GenerateMap.score);
            }
            lastCollider.GetComponentInParent<NoteTile>().hasBeenHit = true;
        }

        isMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("bite");
        if (collision.collider.CompareTag("note"))
            nextNote = collision.collider.GetComponentInParent<NoteTile>().note;
    }

    /*private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("note"))
        {
            if (!collision.collider.GetComponentInParent<NoteTile>().hasBeenHit)
            {
                Vector3 tmpPos = transform.position;
                tmpPos.x = collision.collider.GetComponentInParent<NoteTile>().note - refX;
                collision.collider.GetComponentInParent<NoteTile>().hasBeenHit = true;
                transform.position = tmpPos;
            }
        }
    }*/
}
