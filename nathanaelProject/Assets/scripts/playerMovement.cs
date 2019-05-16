using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;

    public float moveSpeed = 1f;
    public float jumpForce = 10f;
    private Rigidbody2D rb;
    private bool facingRight = true;
    private float canJump = 0f;
    private float move;
    private float jump;
    private Transform groundCheck;
    const float groundRadius = .2f;
    public bool isGrounded;
    // Start is called before the first frame update
    //Updated 5/13/19 by Nathanael Graybill
    void Start()
    {

    }

    //Awake is called before the game starts
    //Created 5/13/19 by Nathanael Graybill
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate.
    //Created 5/13/19 by Nathanael Graybill
    //Last Updated 5/16/19 by Nathanael Graybill
    void FixedUpdate()
    {
        isGrounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }
        Move();
    }
    //function for handling the movement of the player
    //Created 5/15/19 by Nathanael Graybill
    //Last Updated 5/16/19 by Nathanael Graybill
    void Move()
    {
        move = Input.GetAxis("Horizontal");
        jump = Input.GetAxis("Jump");
        //moves the player
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // If the input is moving the player right and the player is facing left...
        if (move > 0 && !facingRight)
        {
            // ... flip the player.
            Flip();
        }
        // Otherwise if the input is moving the player left and the player is facing right...
        else if (move < 0 && facingRight)
        {
            // ... flip the player.
            Flip();
        }

        // If the player should jump...
        if (jump > 0 && isGrounded)
        {
            isGrounded = false;
            // Add a vertical force to the player.
            rb.AddForce(new Vector2(0f, jumpForce));
            canJump = Time.time + 1.5f;    // whatever time a jump takes
        }
    }

    //Function to flip players position
    //Created 5/13/19 by Nathanael Graybill
    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;
        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
