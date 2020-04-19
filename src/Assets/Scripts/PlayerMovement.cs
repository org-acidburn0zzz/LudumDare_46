using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] [Range(0, 10f)] float moveSpeed, jumpMultiplier, fallMultiplier, lowJumpMultiplier;

    float xMov;
    bool onGround;

    private const string GROUND_TAG = "Ground";

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        
    }

    private void Update()
    {
        GetMoveInput();
        FaceDirection();
        WalkAnimation();
        Jump();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void WalkAnimation()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void FaceDirection()
    {
        if (xMov < 0)
        {
            sr.flipX = true;
            transform.Find("Sword").transform.rotation = Quaternion.Euler(0, 180, 0);
            transform.Find("ShootingPoint").transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if (xMov > 0)
        {
            sr.flipX = false;
            transform.Find("Sword").transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.Find("ShootingPoint").transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && onGround)
        {
            rb.AddForce(new Vector2(0, jumpMultiplier), ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
        }
        if (rb.velocity.y < 0)
        {
            anim.SetBool("isJumping", false);
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }

        if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void GetMoveInput()
    {
        xMov = Input.GetAxis("Horizontal") * moveSpeed;
    }

    void Move()
    {
        //transform.position = new Vector2(transform.position.x + xMov, transform.position.y);
        Vector2 movement = new Vector2(xMov, rb.velocity.y);
       // rb.AddForce(movement);
        rb.velocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            onGround = true;
            anim.SetBool("isFalling", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GROUND_TAG))
        {
            onGround = false;
            anim.SetBool("isFalling", true);
        }
    }

    public bool PlayerIsOnGround()
    {
        return onGround;
    }
}
