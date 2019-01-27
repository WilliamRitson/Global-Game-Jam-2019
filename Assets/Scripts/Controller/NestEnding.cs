using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NestEnding : MonoBehaviour
{
    Goal g;

    Animator a;

    // Start is called before the first frame update
    void Start()
    {
        g = GameObject.FindGameObjectWithTag("Goal").GetComponent<Goal>();
        a = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a.GetCurrentAnimatorStateInfo(0).IsName("IdleEnd"))
        {
            SceneManager.LoadScene("Level 1");
        }
    }

    public void TriggerAnim()
    {
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().enabled = false;
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Player"));
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Dude"));
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Mate"));
        a.SetTrigger("Level3Win");
    }

}
