using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggEnd : MonoBehaviour
{

    Animator a;
    // Start is called before the first frame update
    void Start()
    {
        a = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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
