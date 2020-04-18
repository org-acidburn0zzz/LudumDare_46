//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Player_Animator : MonoBehaviour
//{
//    [SerializeField] Sprite playerJump, playerFall;

//    Animator anim;
//    SpriteRenderer sr;
//    Rigidbody2D rb;

//    private void Start()
//    {
//        sr = GetComponent<SpriteRenderer>();
//        anim = GetComponent<Animator>();
//        rb = GetComponent<Rigidbody2D>();
//    }

//    private void Update()
//    {
//        if (Input.GetAxis("Horizontal") != 0)
//        {
//            anim.SetBool("isWalking", true);
//        } else
//        {
//            anim.SetBool("isWalking", false);
//        }

//        if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
//        {
//            anim.SetBool("isJumping", true);
//        }
//        else if (rb.velocity.y < 0)
//        {
//            anim.SetBool("isJumping", false);
//            anim.SetBool("isFalling", true);
//        }
//    }
//}
