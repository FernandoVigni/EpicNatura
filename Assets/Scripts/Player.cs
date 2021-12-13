using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    public int run = 1;
    public float MovHor;
    public float JumpSpeed = 1;
    public bool isGrounded;
    public bool isMoving;
    public bool isJumping;
    public bool isBend;
    public bool isRunning;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            run = 2;
            isRunning = true;
        }
        else
        { 
            run = 1;
            isRunning = false;
        }

        MovHor = Input.GetAxisRaw("Horizontal");
        if (MovHor != 0)
            isMoving = true;
        else
            isMoving = false;

        if (Input.GetKeyDown(KeyCode.W))
            jump();

        if(Input.GetKey(KeyCode.S))
            isBend = true;
        else
            isBend = false;

        rb.velocity = new Vector2(MovHor * run, rb.velocity.y);
        flip(MovHor);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isJumping", isJumping);
        anim.SetBool("isBend", isBend);
        anim.SetBool("isRunning", isRunning);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isJumping = false;
            isGrounded = true;
    }

    private void flip(float _xValue)
    {
        Vector3 theScale = transform.localScale;

        if (_xValue < 0)
            theScale.x = Mathf.Abs(theScale.x) * -1;
        else
        if (_xValue > 0)
            theScale.x = Mathf.Abs(theScale.x);

        transform.localScale = theScale;
    }

    public void jump()
    {
        if (isGrounded) 
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpSpeed);
            isJumping = true;
            isGrounded = false;
        }
    }
}
