using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockStars : MonoBehaviour
{
    public static int frames = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (frames < 0) frames = 30;
        transform.Rotate(0, 2f, 0);
        if (--frames < 0) gameObject.SetActive(false);
    }
}
