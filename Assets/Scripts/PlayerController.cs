using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed;
    public float moveAcceleration;
    public float maximumMoveSpeed;
    public float manuverSpeed;
    public float jumpAcceleration;
    public float jumpDelay;
    public int life = 3;

    private float lastJumpTime = 0;


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
            print("Jump!");
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
    }

    void takeDamage()
    {
        this.life -= 1;
        if (life <= 0)
        {
            die();
        }
    }

    void die()
    {
        print("Oh, noz, I died");
    }
}
