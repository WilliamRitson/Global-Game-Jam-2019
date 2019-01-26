using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafMovement : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = transform.position;

        float sway = Mathf.Sin(Time.time * 3f);
        transform.position = new Vector3(pos.x, sway);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            //count = count + 1;
            //SetCountText();
        }
    }
}
