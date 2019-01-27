using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoveDirection : MonoBehaviour
{

    public enum Direction
    {
        North,
        South,
        West,
        East,
        Random,
        None
    }

    public LayerMask targetLayer;

    public Direction direction = Direction.Random;
    public Direction secondaryDirection = Direction.Random;

    System.Random r;

    public float shoveultiplier = 1;
    public float shoveDuration = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        if (direction == Direction.Random)
        {
            r = new System.Random();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    public void OnTriggerEnter2D(Collision2D collision)
    {
        Debug.Log("trigger tar:" + targetLayer.value + " actual:" + collision.gameObject.layer);
        if (collision.gameObject.layer == targetLayer)
        {
            Debug.Log("hit!");
            Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
            rb.AddForce((getDirection(direction) + getDirection(secondaryDirection)) * 3);
        }
    }*/

    public void Shove(GameObject go)
    {
        Vector3 between = transform.position - go.transform.position;
        float scale = 1 - (between.magnitude / gameObject.GetComponent<CircleCollider2D>().radius);
        go.GetComponent<PlayerController>().Push(between.normalized * shoveultiplier * scale, shoveDuration);

        //Debug.Log(getDirection(direction) + getDirection(secondaryDirection));
        //go.GetComponent<PlayerController>().Push((getDirection(direction) + getDirection(secondaryDirection)) * shoveultiplier, shoveDuration);
    }

    private Vector2 getDirection(Direction d)
    {
        switch (d)
        {
            case Direction.Random:
                return new Vector2((float)((r.NextDouble()*2)-1), (float)((r.NextDouble ()*2)-1));
            case Direction.North:
                return Vector2.up;
            case Direction.South:
                return Vector2.down;
            case Direction.West:
                return Vector2.right;
            case Direction.East:
                return Vector2.left;
            case Direction.None:
                return Vector2.zero;
            default:
                return getDirection(Direction.Random);
        }
    }
}
