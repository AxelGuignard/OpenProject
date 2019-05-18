using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GenerateMap : MonoBehaviour
{
    public GameObject startTile;
    public GameObject tile;
    private List<int[]> track;
    private int BPM;
    public LayerMask noteMask;

    public static LayerMask NoteMask;

    public static int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        NoteMask = noteMask;
        track = LoadTrack("good_track1_clean_full.csv");

        int refX = track[0][1];
        int oldPosX = refX;
        int oldDuration = 0;

        PlayerMovements.refX = refX;

        for(int i = 0; i < track.Count; i++)
        {
            int posX = track[i][1];
            int dir = (int)Mathf.Sign(posX - oldPosX);

            Vector3 startPos = new Vector3(oldPosX - refX, oldDuration * 2);
            GameObject startTmp = GameObject.Instantiate(startTile, startPos, new Quaternion());
            startTmp.GetComponent<NoteTile>().note = track[i][1];

            for (int j = 0; j < Mathf.Abs(posX - oldPosX); j++)
            {
                Vector3 pos = new Vector3(oldPosX - refX + j * dir + dir, oldDuration * 2);
                GameObject tmp = GameObject.Instantiate(tile, pos, new Quaternion());
            }
            oldPosX = posX;

            int duration = track[i][0] + 1;

            for (int j = 1; j < (duration - oldDuration)*2; j++)
            {
                Vector3 pos = new Vector3(oldPosX - refX, oldDuration * 2 + j);
                GameObject tmp = GameObject.Instantiate(tile, pos, new Quaternion());
            }
            oldDuration = duration;

            //return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    List<int[]> LoadTrack(string trackFile)
    {
        string line = "";
        StreamReader reader = new StreamReader(trackFile);
        List<int[]> ret = new List<int[]>();

        BPM = int.Parse(reader.ReadLine());
        Time.fixedDeltaTime = 1.0F / BPM;

        while (line != null)
        {
            line = reader.ReadLine();
            if (line == null) break;

            string[] l = line.Split(';');

            ret.Add(new int[] { int.Parse(l[0]), int.Parse(l[1]) });
        }

        return ret;
    }
}
