using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitHUD : MonoBehaviour
{
    private Vector2 origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float dist = transform.position.y - origin.y;
        transform.position += new Vector3(Mathf.Sin(dist*2)/20f, 0.05f);
        if (dist > 3) GameObject.Destroy(gameObject);
    }

    //public void setText(int damage, Color color)
    //{
    //    Text tmp = GetComponentInChildren<Text>();
    //    tmp.text = damage.ToString();
    //    tmp.color = color;
    //}

    public void setText(string word, Color color)
    {
        Text tmp = GetComponentInChildren<Text>();
        tmp.text = word;
        tmp.color = color;
    }
}
