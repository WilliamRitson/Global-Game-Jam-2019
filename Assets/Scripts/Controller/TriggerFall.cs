﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerFall : MonoBehaviour
{

    Rigidbody2D rb;

    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Rigidbody2D>();
        if (playerLayer == null)
        {
            playerLayer = GameObject.FindGameObjectWithTag("Player").layer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log("boop");
            rb.gravityScale = 0.75f;
        }
    }
}
