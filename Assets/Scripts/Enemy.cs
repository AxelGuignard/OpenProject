using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator _animator;

    private bool isDying;
    public List<Transform> childs;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void recurseChilds(Transform t)
    {
        foreach(Transform tmp in t)
        {
            childs.Add(tmp);
            recurseChilds(tmp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDying)
        {
            //transform.position += new Vector3(0, -0.05f, 0);
            foreach(Transform t in childs)
            {
                Vector3 tmpDir = t.position - transform.position;
                tmpDir = tmpDir / tmpDir.magnitude;
                t.position += tmpDir * 0.1f;
            }
        }
    }

    public void Die()
    {
        _animator.SetTrigger("Die");
        GameObject.Destroy(gameObject, 2f);
        isDying = true;
    }
}
