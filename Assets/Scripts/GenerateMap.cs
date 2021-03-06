﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum EventType
{
    Monster,
    Loot,
    RightTurn,
    LeftTurn,
    Jump
}

public class GenerateMap : MonoBehaviour
{
    public GameObject NoteBlock;
    public GameObject Block;
    public GameObject Wall;
    public GameObject Fence;
    public GameObject Chest;
    public GameObject Enemy;

    private List<int[]> track;
    private int BPM;
    public LayerMask noteMask;

    public static LayerMask NoteMask;

    public static int score = 0;
    public static int bestCombo = 0;
    private static int _combo = 0;
    public static int combo { get
        {
            return _combo;
        }
        set
        {
            if (value > bestCombo) bestCombo = value;
            _combo = value;
        }
    }

    public static int notesPlayed = 0;
    public static int notesToPlay = 0;

    public static Dictionary<int, EventType> noteToEvent;

    private Vector3 lastPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        Debug.Log(StaticData.Level);

        NoteMask = noteMask;
        switch (StaticData.Level)
        {
            case "AU CLAIR DE LA LUNE":
                track = LoadTrack("good_track1_clean_full_event.csv");
                break;
            case "THE LOST WOODS":
                track = LoadTrack("good_track2_clean_full_event.csv");
                break;
            default:
                throw new Exception("No track selected");
        }
        

        //generateMap();
        generateMap2();
    }

    private void generateMap2()
    {
        int refX = track[0][1];
        int oldPosX = refX;
        int oldTime = 0;
        int rotation = 4000000;

        combo = 0;
        bestCombo = 0;
        notesPlayed = 0;
        notesToPlay = track.Count;

        PlayerMovements.refX = refX;

        for(int j = 0; j < 4; j++)
        {
            Vector3 pos = lastPos + new Vector3(0, 0, 1) * (j);
            GameObject tmpBlock = GameObject.Instantiate(Block, pos, new Quaternion());
        }
        lastPos = new Vector3(0, 0, 1) * 4;

        for (int i = 0; i < track.Count; i++)
        {
            int tmpEvent = track[i][1];

            if (noteToEvent[tmpEvent] == EventType.LeftTurn) rotation--;
            if (noteToEvent[tmpEvent] == EventType.RightTurn) rotation++;

            Vector3 tmpDir = new Vector3();
            Vector3 tmpDirWalls = new Vector3();
            if (rotation % 4 == 0)
            {
                tmpDir = new Vector3(0, 0, 1);
                tmpDirWalls = new Vector3(-1, 0, 0);
            }
            else if (rotation % 4 == 1)
            {
                tmpDir = new Vector3(1, 0, 0);
                tmpDirWalls = new Vector3(0, 0, 1);
            }
            else if (rotation % 4 == 2)
            {
                tmpDir = new Vector3(0, 0, -1);
                tmpDirWalls = new Vector3(1, 0, 0);
            }
            else if (rotation % 4 == 3)
            {
                tmpDir = new Vector3(-1, 0, 0);
                tmpDirWalls = new Vector3(0, 0, -1);
            }

            int time = track[i][0];
            int duration = 4 * (time - oldTime);

            // event first
            Vector3 startPos = lastPos;
            GameObject startTmp = GameObject.Instantiate(NoteBlock, startPos, new Quaternion());
            startTmp.GetComponent<NoteTile>().note = track[i][1];
            if (noteToEvent[tmpEvent] == EventType.Loot || noteToEvent[tmpEvent] == EventType.Monster || noteToEvent[tmpEvent] == EventType.Jump)
            {
                GameObject tmpWall1 = GameObject.Instantiate(Wall, startPos + tmpDirWalls + new Vector3(0, 1f, 0), new Quaternion());
                GameObject tmpWall2 = GameObject.Instantiate(Wall, startPos - tmpDirWalls + new Vector3(0, 1f, 0), new Quaternion());

                if (noteToEvent[tmpEvent] == EventType.Loot)
                {
                    GameObject tmpChest = GameObject.Instantiate(Chest, startPos + Vector3.up * 0.5f, new Quaternion());
                    tmpChest.transform.Rotate(0, -90f + (rotation % 4) * 90f, 0);
                    tmpChest.transform.SetParent(startTmp.transform.GetChild(0));
                }
                else if (noteToEvent[tmpEvent] == EventType.Monster)
                {
                    GameObject tmpEnemy = GameObject.Instantiate(Enemy, startPos + Vector3.up * 0.5f, new Quaternion());
                    tmpEnemy.transform.Rotate(0, (rotation % 4) * 90f, 0);
                    tmpEnemy.transform.SetParent(startTmp.transform.GetChild(0));
                }
                else if (noteToEvent[tmpEvent] == EventType.Jump)
                {
                    GameObject tmpFence = GameObject.Instantiate(Fence, startPos + Vector3.up * 0.5f, new Quaternion());
                    tmpFence.transform.Rotate(0,(rotation % 4) * 90f, 0);
                    tmpFence.transform.SetParent(startTmp.transform.GetChild(0));
                }
            }

            else if (noteToEvent[tmpEvent] == EventType.RightTurn)
            {
                GameObject tmpWall1 = GameObject.Instantiate(Wall, startPos - tmpDir + new Vector3(0, 1f, 0), new Quaternion());
                GameObject tmpWall2 = GameObject.Instantiate(Wall, startPos + tmpDirWalls + new Vector3(0, 1f, 0), new Quaternion());
            }
            else if (noteToEvent[tmpEvent] == EventType.LeftTurn)
            {
                GameObject tmpWall1 = GameObject.Instantiate(Wall, startPos - tmpDir + new Vector3(0, 1f, 0), new Quaternion());
                GameObject tmpWall2 = GameObject.Instantiate(Wall, startPos - tmpDirWalls + new Vector3(0, 1f, 0), new Quaternion());
            }

            for (int j = 0; j < duration-1; j++)
            {
                Vector3 pos = lastPos + tmpDir * (j+1);
                GameObject tmpBlock = GameObject.Instantiate(Block, pos, new Quaternion());
                GameObject tmpWall1 = GameObject.Instantiate(Wall, pos + tmpDirWalls + new Vector3(0,1f,0), new Quaternion());
                GameObject tmpWall2 = GameObject.Instantiate(Wall, pos - tmpDirWalls + new Vector3(0, 1f, 0), new Quaternion());
            }

            lastPos = lastPos + tmpDir * duration;
            oldTime = time;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<int[]> LoadTrack(string trackFile)
    {
        noteToEvent = new Dictionary<int, EventType>();

        string line = "";
        StreamReader reader = new StreamReader(trackFile);
        List<int[]> ret = new List<int[]>();

        string[] firstLine = reader.ReadLine().Split(';');

        BPM = int.Parse(firstLine[0]);
        int nbDiffNotes = int.Parse(firstLine[1]);
        Time.fixedDeltaTime = 1.0F / ((BPM / 60f) * 20f);

        for(int i = 0; i < nbDiffNotes; i++)
        {
            string[] tmpNoteInfos = reader.ReadLine().Split(';');
            int tmpNote = int.Parse(tmpNoteInfos[0]);
            string tmpVal = tmpNoteInfos[1];
            noteToEvent.Add(tmpNote, (EventType)Enum.Parse(typeof(EventType), tmpVal));
        }

        while (line != null)
        {
            line = reader.ReadLine();
            if (line == null) break;

            string[] l = line.Split(';');

            ret.Add(new int[] { int.Parse(l[0]), int.Parse(l[1])});
        }

        return ret;
    }
}
