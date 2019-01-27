﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    PlayerController pc;

    Transform canvas;
    Transform healthUI;
    Transform livesUI;

    List<GameObject> hearts = new List<GameObject>();
    List<GameObject> eggs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        healthUI = canvas.GetChild(0);
        livesUI = canvas.GetChild(1);

        GameObject go;
        for (int i = 0; i < pc.hearts; i++)
        {
            go = GameObject.Instantiate(Resources.Load("Prefabs/Heart"), healthUI) as GameObject;
            go.transform.localPosition = new Vector2(-15 + (i * 35) + 50, 15 - 50);
            hearts.Add(go);
        }

        for (int i = 0; i < pc.eggs; i++)
        {
            go = GameObject.Instantiate(Resources.Load("Prefabs/Egg"), livesUI) as GameObject;
            go.transform.localPosition = new Vector2(-15 + (i * 35) + 50, 15 - 50);
            eggs.Add(go);
        }

    }

    public void LoseHealth()
    {
        GameObject go = hearts[hearts.Count - 1];
        hearts.Remove(go);
        GameObject.Destroy(go);
    }

    public void LoseLife()
    {
        GameObject go = eggs[eggs.Count - 1];
        eggs.Remove(go);
        GameObject.Destroy(go);
    }

    public void ResetHealth()
    {
        GameObject go;
        for (int i = 0; i < pc.hearts; i++)
        {
            go = GameObject.Instantiate(Resources.Load("Prefabs/Heart"), healthUI) as GameObject;
            go.transform.localPosition = new Vector2(-15 + (i * 35) + 50, 15 - 50);
            hearts.Add(go);
        }
    }

    public void ResetLives()
    {
        GameObject go;
        for (int i = 0; i < pc.hearts; i++)
        {
            go = GameObject.Instantiate(Resources.Load("Prefabs/Eggs"), healthUI) as GameObject;
            go.transform.localPosition = new Vector2(-15 + (i * 35) + 50, 15 - 50);
            hearts.Add(go);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
