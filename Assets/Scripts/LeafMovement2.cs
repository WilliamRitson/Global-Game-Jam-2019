﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMovement2 : MonoBehaviour
{

    private Vector2 originalPos;

    private void Awake()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;
        float sway = 20f * Mathf.Sin(Time.time);
        float downward = pos.y - 0.01f;
        transform.position = new Vector2(originalPos.x + sway, downward);

        if (sway < 0.01 || sway > 0.9)
        {
            transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        }
    }
}
