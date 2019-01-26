using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillypadMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 30) * Time.deltaTime * 0.2f);
    }
}
