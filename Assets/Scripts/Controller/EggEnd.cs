using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggEnd : MonoBehaviour
{
    public GameObject introText;

    Animator a;
    // Start is called before the first frame update
    void Start()
    {
        a = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (a.GetCurrentAnimatorStateInfo(0).IsName("IdleHatch"))
        {
            if (Input.GetButton("Fire1"))
            {
                introText.SetActive(false);
                a.SetTrigger("FirstClick");
            }
        }

        if (a.GetCurrentAnimatorStateInfo(0).IsName("SpawnPlayer"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerController>().enabled = true;
            foreach (Transform tr in player.GetComponentInChildren<Transform>())
            {
                tr.gameObject.SetActive(true);
            }
            GameObject.Destroy(gameObject);
        }
    }
}
