using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerFall : MonoBehaviour
{

    Rigidbody2D rb;

    public LayerMask targetLayer;
    

    public float fallGravity = 1;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 100, targetLayer);

        if (hit.collider != null)
        {
            Fall();
        }

    }

    public void Fall() {
        rb.gravityScale = fallGravity;
    }

}
