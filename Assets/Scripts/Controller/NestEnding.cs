using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestEnding : MonoBehaviour
{
    Goal g;

    Animator a;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Goal").GetComponent<Goal>();
        a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a.GetCurrentAnimatorStateInfo(0).IsName("IdleEnd"))
        {
            g.End();
        }
    }

    public void TriggerAnim()
    {
        a.SetTrigger("Level3Win");
    }

}
