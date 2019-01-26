using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakFall : MonoBehaviour
{

    private Rigidbody2D rb;

    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fall()
    {
        rb.gravityScale = 1.5f;
    }

}
