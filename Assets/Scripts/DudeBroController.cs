using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeBroController : MonoBehaviour
{
    public GameObject target;
    public GameObject nest;
    public GameObject player;
    public float followSpeed = 4.0f;

    private Rigidbody2D rb2d;

    private bool pulling = false;
    private Vector3 pullOffset;
    private AudioSource damageAudio;

    private ParticleSystem ps;

    private void Start()
    {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
        damageAudio = gameObject.GetComponent<AudioSource>();
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        if (!pulling)
        {
            rb2d.velocity = (target.transform.position - transform.position).normalized * followSpeed;
        } else
        {
            target.transform.position = gameObject.transform.position + pullOffset;
            rb2d.velocity = (nest.transform.position - transform.position).normalized * followSpeed;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.collider.gameObject.tag;
        if (tag == "Player")
        {
            damageAudio.Play();
            ps.Play();
            StartNewAttack();
        } else if (collision.collider.gameObject == target)
        {
            pulling = true;
            pullOffset = target.transform.position - transform.position;
        } else if (collision.collider.gameObject == nest)
        {
            if (pulling)
            {
                player.GetComponent<PlayerController>().die();
            }
        }
    }

    private void StartNewAttack()
    {
        pulling = false;
        var offset = 15 + Random.value * 20;
        var offsetDir = Random.value >= 0.5 ? 1 : -1;
        transform.position = new Vector2(transform.position.x + offset * offsetDir,  8);
    }
}
