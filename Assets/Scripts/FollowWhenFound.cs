using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWhenFound : MonoBehaviour
{
    private GameObject followTarget;
    public float followSpeed = 3.5f;
    private Vector3 offset;
    private Rigidbody2D rb2d;
    private AudioSource souce;
    public AudioClip findSFX;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        souce = gameObject.AddComponent<AudioSource>();
        souce.clip = findSFX;
        souce.playOnAwake = false;

    }

    void Update()
    {
        if (followTarget != null)
        {
            Vector3 goal = followTarget.transform.position - offset;
            if (Vector3.Distance(goal, transform.position) > 3)
            {
                rb2d.velocity = (goal - transform.position).normalized * followSpeed;
            } else
            {
                rb2d.velocity = Vector3.zero;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (followTarget == null && collision.gameObject.CompareTag("Player"))
        {
            PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
            if (pc.mate)
            {
                return;
            }
            this.followTarget = collision.gameObject;
            this.offset = new Vector3(0, 0);
            pc.mate = gameObject;
            souce.Play();
        }
    }
}
