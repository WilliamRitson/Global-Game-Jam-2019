﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhirlpoolMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 75) * Time.deltaTime * 2f);
    }
}
