using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody2D rb;
    public float speed = 7f;
    private Vector2 moveVector;
    private bool flipRight = true;
    public Animator anim;

    public bool faceRight = true;
    public float jumpForce = 7f;
    private bool jumpControl;
    public float jumpControlTime = 0.7f;
    private int jumpIteration = 0;
    private float jumpTime = 0;
    public int jumpValueIteration = 60;
    
    
    public bool onGround;

    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    

    // Update is called once per frame
    void Update()
    {
        Walk();
        Reflect();
        Jump();
        checkingGroup();
    }



   public void Walk()
   {
       moveVector.x = Input.GetAxis("Horizontal"); 
       rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
       anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
   }

   public void Reflect()
   {
       if ((moveVector.x < 0 && !faceRight) || (moveVector.x > 0 && faceRight))
       {
           transform.localScale *= new Vector2(-1, 1);
           faceRight = !faceRight;

       }
   }

   public void Jump()
   {

       if (Input.GetKey(KeyCode.Space))
       {
           if (onGround)
           {
               jumpControl = true;
           }
       }
       else
       {
           jumpControl = false;
       }

       if (jumpControl)
       {
           if ((jumpTime += Time.deltaTime) < jumpControlTime)
           {
               rb.AddForce(Vector2.up * jumpForce / (jumpTime * 10) * Time.deltaTime);
           }
           
       }
       else
       {
           jumpTime = 0;
       }
   }

   public void checkingGroup()
   {
       onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, Ground);
       anim.SetBool("onGround", onGround);
   }


   
   
}
