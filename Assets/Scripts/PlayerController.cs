using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MovementType
{
    Falling, Swimming
}

public class PlayerController : MonoBehaviour
{
    // Movement
    private Rigidbody2D rb2d;
    private ParticleSystem featherParticles;
    private ParticleSystem bubbleParticles;
    private HUDController hudC;
    public Vector2 moveDirection;
    public float moveSpeed;
    public float moveAcceleration;
    public float maximumMoveSpeed;
    public float manuverSpeed;
    public MovementType movementType = MovementType.Falling;

    // Juming
    private float lastJumpTime = 0;
    public float jumpAcceleration;
    public float jumpDelay;


    // Fast Swim
    public float fastSwimMultiplier = 1.75f;

    // Life
    private int hearts = 3;

    public int Hearts
    {
        get
        {
            return hearts;
        }
        set
        {
            hearts = value;
        }
    }

    public int startHearts = 3;

    private int eggs = 3;
    public int StartEggs = 3;

    public int Eggs
    {
        get
        {
            return eggs;
        }
        set
        {
            eggs = value;
        }
    }

    // Player Sound Effects
    public AudioClip jumpSFX;
    public AudioClip hurtSFX;
    public AudioClip dieSFX;
    private AudioSource audioJump;
    private AudioSource audioHurt;
    private AudioSource audioDie;

    private Vector2 pushVelocity = new Vector2();
    private float pushDuration = 0;

    private bool dead = false;


    public List<GameObject> hideOnDeath;
    public List<GameObject> showOnDeath;

    public float fertility = 0;
    public float stamina = 0;
    public float manuverability = 0;

    private Stats stats;

    public Stats Stat
    {
        get
        {
            return stats;
        }
    }

    public GameObject mate;

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
        hudC = GameObject.FindGameObjectWithTag("Canvas").GetComponent<HUDController>();
        audioJump = AddAudio(jumpSFX, false, false, 1.0f);
        audioHurt = AddAudio(hurtSFX, false, false, 1.0f);
        audioDie = AddAudio(dieSFX, false, false, 1.0f);
        /*
         * if ( water level ) {
         *     bubbleParticles.Play();
         * }
         */
        stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        stats.SetYouStats(this);
        Stat.SetHeartsEggs(this);
        if (hearts < 0 && eggs < 0)
        {
            Debug.Log("blah");
            hearts = startHearts;
            eggs = StartEggs;
            stats.LoadHeartsEggs(this);
            hudC.SetPC(this);
            hudC.ResetHealth();
            hudC.ResetLives();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movementType == MovementType.Falling)
        {
            FallingMotion();
        }
        else if (movementType == MovementType.Swimming)
        {
            SwimmingMotion();
            if (dead)
            {
                transform.localScale /= (float)(1.00 + 0.075 * Time.deltaTime);
                var renderers = GetComponentsInChildren<SpriteRenderer>();
                foreach (var rendeer in renderers)
                {
                    var col = rendeer.material.color;
                    col.a -= (0.05f * Time.deltaTime);
                    rendeer.material.color = col;
                }
            }
        }
        CheckAction();


    }

    void CheckAction()
    {
        lastJumpTime -= Time.deltaTime;
        pushDuration -= Time.deltaTime;
        if (pushDuration < 0)
        {
            pushVelocity = new Vector2();
        }

        if (Input.GetButton("Fire1") && lastJumpTime <= 0)
        {
            lastJumpTime = jumpDelay;
            audioJump.Play();
            GetComponentInChildren<Animator>().SetTrigger("FlapWing");
            if (movementType == MovementType.Falling)
            {
                Flap();
            }
            else if (movementType == MovementType.Swimming)
            {
                SwimFast();
            }
        }
    }

    private void SwimFast()
    {
        manuverSpeed *= fastSwimMultiplier;
        StartCoroutine("EndFast");
    }

    private IEnumerator EndFast()
    {
        yield return new WaitForSeconds(jumpDelay / 3);
        manuverSpeed /= fastSwimMultiplier;
    }


    private void Flap()
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpAcceleration);
    }

    void FallingMotion()
    {
        float verticalSpeed = this.rb2d.velocity.y + moveAcceleration * Time.deltaTime * -1;
        float horizontalSpeed = Input.GetAxis("Horizontal") * manuverSpeed;

        float prevVel = rb2d.velocity.x;

        rb2d.velocity = new Vector2(horizontalSpeed + pushVelocity.x, verticalSpeed + pushVelocity.y);

        if (rb2d.velocity.x > 0 && rb2d.velocity.x != 0 && prevVel != 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (prevVel != 0 && rb2d.velocity.x != 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void SwimmingMotion()
    {
        Vector2 playerMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * manuverSpeed;
        rb2d.velocity = playerMovement + pushVelocity + new Vector2(moveSpeed, 0);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ShoveObject"))
        {
            collision.gameObject.GetComponent<ShoveDirection>().Shove(gameObject);
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
            BreakFall fall = collision.gameObject.GetComponent<BreakFall>();
            if (fall)
            {
                fall.Fall();
            }
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
        hudC.LoseHealth();
        this.hearts -= 1;
        if (hearts <= 0)
        {
            die();
        }
    }

    public void die()
    {
        MusicManager.Instance.StopMusic();
        audioDie.Play();
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
        manuverSpeed = 0;
        rb2d.angularVelocity += 30;

        foreach (var toShow in showOnDeath)
        {
            toShow.GetComponent<SpriteRenderer>().enabled = true;
        }

        foreach (var toHide in hideOnDeath)
        {
            toHide.GetComponent<SpriteRenderer>().enabled = false;
        }

        StartCoroutine("EndLevel");
        dead = true;

    }


    private IEnumerator EndLevel()
    {
        yield return new WaitForSeconds(5.0f);
        eggs--;
        hudC.LoseLife();
        hearts = startHearts;
        stats.LoadHeartsEggs(this);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Push(Vector2 p, float t)
    {
        pushVelocity = p;
        pushDuration = t;
    }

}
