using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject followTarget;
    public float followSpeed = 2.0f;

    private Vector3 offset;
    private Rigidbody2D rb2d;

    private void Start()
    {
        offset = followTarget.transform.position - transform.position;
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 goal = followTarget.transform.position - offset;
        rb2d.velocity = (goal - transform.position).normalized * followSpeed;
    }
}
