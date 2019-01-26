using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 moveDirection;
    public float moveSpeed;
    public float manuverSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        // Automatic movment
        this.transform.position += (Vector3)moveDirection.normalized * Time.deltaTime * moveSpeed;

        // Player movement
        this.transform.position += (Vector3)new Vector2(Input.GetAxis("Horizontal"), 0).normalized * Time.deltaTime * manuverSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            this.takeDamage();
        }
    }

    void takeDamage()
    {
        moveSpeed = 0;
        manuverSpeed = 0;
    }
}
