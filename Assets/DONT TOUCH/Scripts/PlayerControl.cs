using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed;
    public float jumpSpeed;
    private Rigidbody2D rb;
    private float xMove;
    private bool jumpFlag;
    private SpriteRenderer sr;
    private int jumpCount;
    public int jumpCountMax = 2;




    public float speed; // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
         xMove = Input.GetAxisRaw("Horizontal");


        transform.Translate(xMove * speed * Time.deltaTime, 0, 0);


        if (xMove != 0)
        {
            if (xMove > 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < jumpCountMax)
        {
            jumpFlag = true;
            jumpCount++;
        }

        if (GroundCheck())
        {
            jumpCount = 1;
        }

    }


    private void FixedUpdate()
    {
        rb.velocity = new Vector2(xMove * moveSpeed * Time.deltaTime, rb.velocity.y);
        if (jumpFlag)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpFlag = false;
        }
    }


    private bool GroundCheck()
    {
        Collider2D col = GetComponent<Collider2D>();
        return Physics2D.Raycast(transform.position, Vector2.down, col.bounds.extents.y + 0.1f, LayerMask.GetMask("Ground"));
    }

}