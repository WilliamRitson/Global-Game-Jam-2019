using System.Collections;
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
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 position = transform.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, playerLayer);

        if (hit.collider != null)
        {
            Debug.Log("boop");
            rb.gravityScale = 0.75f;
        }
    }
}
