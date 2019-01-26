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
        this.transform.position += (Vector3) moveDirection.normalized * Time.deltaTime  * moveSpeed;
        this.transform.position += (Vector3) new Vector2(Input.GetAxis("Horizontal"), 0).normalized * Time.deltaTime * manuverSpeed;
    }
}
