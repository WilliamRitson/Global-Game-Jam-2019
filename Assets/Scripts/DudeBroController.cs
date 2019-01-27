using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeBroController : MonoBehaviour
{
    public GameObject target;
    public float followSpeed = 4.0f;

    private Vector3 offset;
    private Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 goal = target.transform.position;
        print(goal);
        print(transform.position - goal);
        rb2d.velocity = (goal - transform.position).normalized * followSpeed;
    }
}
