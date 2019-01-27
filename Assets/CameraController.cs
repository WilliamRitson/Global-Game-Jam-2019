using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject followTarget;

    private Vector3 offset;

    private void Start()
    {
        offset = gameObject.transform.position - followTarget.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = followTarget.transform.position + offset;
    }
}
