﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleBird : MonoBehaviour
{

    private Transform body;
    private Transform[] eyes = new Transform[2];
    private Transform[] wings = new Transform[2];
    

    private static System.Random r = new System.Random();

    private float bScale = 0;
    private float eScale = 0;
    private float wScale = 0;

    // Start is called before the first frame update
    void Start()
    {

        GetComponents();
        RandomizeScales();
        ScaleAll();


    }

    private void RandomizeScales()
    {
        bScale = (float)r.NextDouble() * 3 + 1;
        eScale = (float)(r.NextDouble() * 2) + (float)(bScale * ((r.NextDouble() * 1.5) + 0.5));
        wScale = (float)(r.NextDouble() * 1.75) + (float)(bScale * ((r.NextDouble() * 1.75) + 0.5));
        Debug.Log(eScale);
    }

    private void ScaleAll()
    {
        ScaleObject(body, bScale, ((bScale - 1) / 2) + 1);

        ScaleObject(eyes[0], eScale, eScale);
        ScaleObject(eyes[1], eScale, eScale);
        Vector3 ePos = eyes[0].localPosition;
        ePos.x += ePos.x * ((bScale - 1) / 3);
        eyes[0].localPosition = ePos;
        ePos = eyes[1].localPosition;
        ePos.x += ePos.x * ((bScale - 1) / 3);
        eyes[1].localPosition = ePos;

        ScaleObject(wings[0], ((wScale - 1) / 2) + 1, wScale);
        ScaleObject(wings[1], ((wScale - 1) / 2) + 1, wScale);
        Vector3 wPos = wings[0].localPosition;
        wPos.y += wPos.y * (bScale * ((bScale)/3));
        wPos.x += wPos.x * (bScale * ((bScale) / 4.5f)) * ((wScale + 1) / 11);
        wings[0].localPosition = wPos;
        wPos = wings[1].localPosition;
        wPos.y += wPos.y * (bScale * ((bScale) / 3));
        wPos.x += wPos.x * (bScale * ((bScale) / 4.5f)) * ((wScale + 1) / 11);
        wings[1].localPosition = wPos;

    }

    private void ScaleObject(Transform tr, float amtV, float amtH)
    {
        Vector3 newScale = tr.localScale;
        newScale.x *= amtH;
        newScale.y *= amtV;
        tr.localScale = newScale;
    }

    private void GetComponents()
    {
        body = transform.GetChild(2);
        eyes[0] = transform.GetChild(0);
        eyes[1] = transform.GetChild(1);
        wings[0] = transform.GetChild(3);
        wings[1] = transform.GetChild(4);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}