using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    private Rigidbody2D rb2d;
    private ParticleSystem featherParticles;
    private ParticleSystem bubbleParticles;
    public Vector2 moveDirection;
    public float moveSpeed;
    public float moveAcceleration;
    public float maximumMoveSpeed;
    public float manuverSpeed;

    // Juming
    private float lastJumpTime = 0;
    public float jumpAcceleration;
    public float jumpDelay;

    // Life
    public int life = 3;

    // Player Sound Effects
    public AudioClip jumpSFX;
    public AudioClip hurtSFX;
    public AudioClip dieSFX;
    private AudioSource audioJump;
    private AudioSource audioHurt;
    private AudioSource audioDie;

    private Vector2 pushVelocity = new Vector2();
    private float pushDuration = 0;

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        newAudio.clip = clip; 
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        newAudio.volume = vol; 
        return newAudio; 
     }

    public void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        featherParticles = GameObject.FindGameObjectWithTag("FeatherParticles").GetComponent<ParticleSystem>();
        bubbleParticles = GameObject.FindGameObjectWithTag("BubbleParticles").GetComponent<ParticleSystem>();
        audioJump = AddAudio(jumpSFX, false, false, 1.0f);
        audioHurt = AddAudio(hurtSFX, false, false, 1.0f);
        audioDie = AddAudio(dieSFX, false, false, 1.0f);
        /*
         * if ( water level ) {
         *     bubbleParticles.Play();
         * }
         */
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        CheckJump();    
    }

    void CheckJump()
    {
        lastJumpTime -= Time.deltaTime;
        pushDuration -= Time.deltaTime;
        if (pushDuration < 0)
        {
            pushVelocity = new Vector2();
        }

        if (Input.GetButton("Fire1") && lastJumpTime <= 0)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpAcceleration);
            lastJumpTime = jumpDelay;
            audioJump.Play();
            GetComponentInChildren<Animator>().SetTrigger("FlapWing");
        }
    }

    void MoveCharacter()
    {
        float verticalSpeed = this.rb2d.velocity.y + moveAcceleration * Time.deltaTime * -1;
        float horizontalSpeed = Input.GetAxis("Horizontal") * manuverSpeed;

        float prevVel = rb2d.velocity.x;

        rb2d.velocity = new Vector2(horizontalSpeed + pushVelocity.x, verticalSpeed + pushVelocity.y);

        if (rb2d.velocity.x > 0 && rb2d.velocity.x != 0 && prevVel != 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if (prevVel != 0 && rb2d.velocity.x != 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision.collider);
    }

    private void CheckCollision(Collider2D collision)
    {
        print(collision);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            this.takeDamage();
        }
        if (collision.gameObject.CompareTag("BreakableObstacle"))
        {
            collision.gameObject.GetComponent<BreakFall>().Fall();
            this.takeDamage();
        }
        if (collision.gameObject.CompareTag("ShoveObject"))
        {
            collision.gameObject.GetComponent<ShoveDirection>().Shove(gameObject);
        }
    }

    void takeDamage()
    {
        audioHurt.Play();
        featherParticles.Play();
        this.life -= 1;
        if (life <= 0)
        {
            die();
        }
    }

    void die()
    {
        MusicManager.Instance.StopMusic();
        audioDie.Play();
        rb2d.simulated = false;
    }

    public void Push(Vector2 p, float t)
    {
        pushVelocity = p;
        pushDuration = t;
    }
}
