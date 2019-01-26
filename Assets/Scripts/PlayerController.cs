using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
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
        audioJump = AddAudio(jumpSFX, false, false, 1.0f);
        audioHurt = AddAudio(hurtSFX, false, false, 1.0f);
        audioDie = AddAudio(dieSFX, false, false, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckJump();
        MoveCharacter();
    }
    
    void CheckJump()
    {
        lastJumpTime -= Time.deltaTime;

        if (Input.GetButton("Fire1") && lastJumpTime <= 0)
        {
            moveSpeed -= jumpAcceleration;
            lastJumpTime = jumpDelay;
            audioJump.Play();
        }
    }

    void MoveCharacter()
    {
        // Automatic movment
        this.transform.position += (Vector3)moveDirection.normalized * Time.deltaTime * moveSpeed;

        // Player movement
        this.transform.position += (Vector3)new Vector2(Input.GetAxis("Horizontal"), 0).normalized * Time.deltaTime * manuverSpeed;

        // Automatic movement acceleration
        moveSpeed = Mathf.Min(moveSpeed + moveAcceleration * Time.deltaTime, maximumMoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
        moveAcceleration = 0;
        jumpAcceleration = 0;
        moveSpeed = 0;
        manuverSpeed = 0;
    }
}
