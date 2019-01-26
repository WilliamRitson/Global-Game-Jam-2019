using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TriggerFall : MonoBehaviour
{

    Rigidbody2D rb;

    Ray ray;
    RaycastHit2D[] hits;

    // Start is called before the first frame update
    void Start()
    {
        rb.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        ray = new Ray(gameObject.transform.position, Vector3.down);
        hits = Physics2D.GetRayIntersectionAll(ray);
        if (hits.Select (n => CheckHitPlayer(n)).Contains(true))
        {
            rb.gravityScale = 0.75f;
        }
    }

    private bool CheckHitPlayer(RaycastHit2D hit)
    {
        if (hit.collider.gameObject.layer == 9)
        {
            return true;
        }
        return false;
    }
}
