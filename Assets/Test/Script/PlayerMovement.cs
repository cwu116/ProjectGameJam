using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocity;
    public float jumpForce;

    protected Rigidbody2D rigid;
    protected float vertical;
    protected float horizontal;

    protected bool isJump;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        Move();
        Jump();
    }

    public void GetMovementInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    public void Move()
    {

        rigid.velocity = new Vector2(horizontal * Time.deltaTime * velocity, 0);
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigid.AddForce(Vector2.up * jumpForce);
            isJump = false;
        }
    }
}
