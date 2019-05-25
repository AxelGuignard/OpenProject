using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMover : MonoBehaviour
{
    private Vector3 moveDir;
    private Vector3 rotDir;
    private int moveCounter = 0;

    private List<Vector3> rotList;
    private List<Vector3> moveList;

    // Start is called before the first frame update
    void Start()
    {
        rotList = new List<Vector3>();
        moveList = new List<Vector3>();
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCounter <= 0)
        {
            moveCounter = 80;
            if (Random.Range(0f, 1f) > 0.5f || rotList.Count == 0)
            {
                rotDir = new Vector3(Random.Range(-1f, 1f) / 50f, Random.Range(-1f, 1f) / 50f, Random.Range(-1f, 1f) / 50f);
                moveDir = new Vector3(Random.Range(-1f, 1f) / 500f, Random.Range(-1f, 1f) / 50000f, Random.Range(-1f, 1f) / 500f);
                rotList.Add(rotDir);
                moveList.Add(moveDir);
            }
            else
            {
                rotDir = -rotList[0];
                moveDir = -moveList[0];
                rotList.RemoveAt(0);
                moveList.RemoveAt(0);
            }
        }
        transform.Rotate(rotDir);
        transform.position += moveDir;
        moveCounter--;
    }
}
