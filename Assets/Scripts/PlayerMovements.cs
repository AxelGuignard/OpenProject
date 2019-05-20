﻿using MidiJack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovements : MonoBehaviour
{
    public static int refX;
    private bool isMoving = false;

    private int nextNote;
    private Collider lastCollider;

    public static PlayerMovements player;

    private Animator _animator;

    public GameObject hitHud;

    // Start is called before the first frame update
    void Start()
    {
        MidiMaster.noteOnDelegate += MidiKeyOn;
        player = this;
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool quit = Input.GetKeyDown(KeyCode.Escape);
        if (quit)
        {
            SceneManager.LoadScene(GameScene.MainMenu.ToString());
        }

        bool k53 = Input.GetKeyDown(KeyCode.E);
        bool k55 = Input.GetKeyDown(KeyCode.R);
        bool k57 = Input.GetKeyDown(KeyCode.T);
        if (k53)
        {
            if (isMoving) PianoTest.instance.playNote2(53);
            hitAKey(53);
        }
        else if (k55)
        {
            if (isMoving) PianoTest.instance.playNote2(55);
            hitAKey(55);
        }
        else if (k57)
        {
            if (isMoving) PianoTest.instance.playNote2(57);
            hitAKey(57);
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
            transform.position += transform.forward * 0.06f;

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
                    if (!lastCollider.GetComponentInParent<NoteTile>().hasBeenHit)
                    {
                        LoseCombo();
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
            NoteTile sNote = col.GetComponentInParent<NoteTile>();
            if (sNote.note == note && !sNote.hasBeenHit)
            {
                int tmpNote = sNote.note;
                sNote.hasBeenHit = true;
                GenerateMap.score++;
                HUD.SetScore(GenerateMap.score);

                col.transform.parent.GetChild(1).GetComponent<MeshRenderer>().enabled = false;

                if (GenerateMap.noteToEvent[tmpNote] == EventType.Loot)
                {
                    col.GetComponentInChildren<Animator>().SetTrigger("Open");
                }
                if(GenerateMap.noteToEvent[tmpNote] == EventType.Monster)
                {
                    col.GetComponentInChildren<Enemy>().Die();
                }

                AddToCombo();
            }
            else if(sNote.note != note && !sNote.hasBeenHit)
            {
                LoseCombo();
            }
            lastCollider.GetComponentInParent<NoteTile>().hasBeenHit = true;
        }
        if (!isMoving) HUD.HideStartMessage();
        isMoving = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("note"))
            nextNote = collision.collider.GetComponentInParent<NoteTile>().note;
    }

    private void AddToCombo()
    {
        GenerateMap.combo++;
        HUD.SetCombo(GenerateMap.combo);
        //DO THINGS
    }

    private void LoseCombo()
    {
        if (GenerateMap.combo != 0)
        {
            GenerateMap.combo = 0;
            HUD.SetCombo(GenerateMap.combo);
            HUD.playLoseComboSound();
            GameObject tmp = GameObject.Instantiate(hitHud, transform.position, transform.rotation);
            tmp.transform.SetParent(transform);
            // DO THINGS
        }
    }
}
