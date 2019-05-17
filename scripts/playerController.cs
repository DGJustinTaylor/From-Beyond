using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //private SpriteRenderer m_spriteRenderer; Currently Unused
    private Rigidbody2D m_rigidBody;
    private float movement;
    public float speed = 5f;
    public float crouch;
    public float crouchSpeed = 2.5f;
    public float jumpHeight = 5f;
    public bool isCrouching = false;
    public bool isGrounded = false;
    public bool facingRight = true;

    void Awake()
    {
        //m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Crouch();
        movement = Input.GetAxis("Horizontal"); //grabs left or right movement from user input with values between -1 and 1
        m_rigidBody.velocity = new Vector2(movement * speed, m_rigidBody.velocity.y); //Grabs input from movement, multiplied by speed and stores it in velocity to move the player

        crouch = Input.GetAxis("Crouch");


        if (movement > 0 && facingRight) //changes player orientation when facing right if they choose to go left
        {
            FlipX(); 
        }
        else if (movement < 0 && !facingRight) //changes player orientation when facing right if they choose to go right
        {
            FlipX();
        }

        if(Input.GetKey(KeyCode.LeftControl)) //Crouching
        {
            isCrouching = !isCrouching;
        }
        else if(!Input.GetKey(KeyCode.LeftControl)) //Standing up from a crouched position
        {

        }
    }

    void FlipX()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        facingRight = !facingRight;
    }

    void Jump()
    {
        if (isGrounded == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W)))
        {
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpHeight), ForceMode2D.Impulse);
        }
    }

    void Crouch()
    {
        if(crouch!= 0 && isGrounded)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }
    }
}