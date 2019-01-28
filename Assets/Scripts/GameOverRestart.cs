using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverRestart : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            GameObject.Destroy(GameObject.FindGameObjectWithTag("Stats"));
            SceneManager.LoadScene("Level 1");
        }
    }
}
