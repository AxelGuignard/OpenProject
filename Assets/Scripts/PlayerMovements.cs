using MidiJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    public static int refX;
    private bool isMoving = false;

    private int nextNote;
    private Collider2D lastCollider;

    // Start is called before the first frame update
    void Start()
    {
        MidiMaster.noteOnDelegate += MidiKeyOn;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving)
            transform.position += new Vector3(0, 0.06f);

        bool k53 = Input.GetKeyDown(KeyCode.E);
        bool k55 = Input.GetKeyDown(KeyCode.R);
        bool k57 = Input.GetKeyDown(KeyCode.T);
        if (k53) hitAKey(53);
        if (k55) hitAKey(55);
        if (k57) hitAKey(57);

        Collider2D col = Physics2D.OverlapBox(transform.position + new Vector3(0, 0.2f), Vector2.one * 0.05f, 0, GenerateMap.NoteMask);
        if (col != null)
        {
            nextNote = col.GetComponent<NoteTile>().note;
            Debug.Log("next : " + nextNote);
            lastCollider = col;
        }
        else
        {
            if(nextNote != -1)
            {
                if (lastCollider != null)
                {
                    if (!lastCollider.GetComponent<NoteTile>().hasBeenHit)
                    {
                        Vector3 tmpPos = transform.position;
                        tmpPos.x = lastCollider.GetComponent<NoteTile>().note - refX;
                        lastCollider.GetComponent<NoteTile>().hasBeenHit = true;
                        transform.position = tmpPos;
                    }
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
        Collider2D col = Physics2D.OverlapBox(transform.position, Vector2.one * 0.05f, 0, GenerateMap.NoteMask);

        if(col != null)
        {
            if (col.GetComponent<NoteTile>().note == note )
            {
                GenerateMap.score++;
                Debug.Log(GenerateMap.score);
                Vector3 tmpPos = transform.position;
                tmpPos.x = col.GetComponent<NoteTile>().note - refX;
                col.GetComponent<NoteTile>().hasBeenHit = true;
                transform.position = tmpPos;
            }
        }

        isMoving = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bite");
        if (collision.collider.CompareTag("note"))
            nextNote = collision.collider.GetComponent<NoteTile>().note;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("note"))
        {
            if (!collision.collider.GetComponent<NoteTile>().hasBeenHit)
            {
                Vector3 tmpPos = transform.position;
                tmpPos.x = collision.collider.GetComponent<NoteTile>().note - refX;
                collision.collider.GetComponent<NoteTile>().hasBeenHit = true;
                transform.position = tmpPos;
            }
        }
    }
}
