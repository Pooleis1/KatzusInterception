using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D rigidBody;
    private BoxCollider2D collider;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jump = 10;
    private float jumpCooldown = 0;
    private bool doubleJump = true;
    private bool facingRight = true;
    private float horizontalInput;


    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        horizontalInput = Input.GetAxis("Horizontal");
        
        if (jumpCooldown > 0.3f)
        {
            rigidBody.velocity = new Vector2(horizontalInput * speed, rigidBody.velocity.y);

            if (OnWall() && !IsGrounded())
            {
                rigidBody.gravityScale = 2f;
                rigidBody.velocity = Vector2.zero;
            }
            else
            {
                rigidBody.gravityScale = 2.5f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            jumpCooldown += Time.deltaTime;
        }

        // Wenn der Spieler den Boden berührt, wird seine Double-Jump Funktion wieder "aufgeladen"
        if (IsGrounded() && doubleJump == false)
        {
            doubleJump = true;
        }

        // Der Charakter wird auf der x-Achse gespiegelt, wenn er sich nach links bzw. nach rechts bewegt
        if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
        {
            Flip();
        }

    }
    
    private void Jump()
    {
        // Wenn der Spieler am Boden ist oder Double-Jumpen kann
        if (IsGrounded() || CanDoubleJump()) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jump);
        }
        
        // Wenn der Spieler an der Wand ist und nicht am Boden ist
        else if (OnWall() && !IsGrounded())
        {
            // Wenn der Spieler nicht nach links oder rechts lenkt
            if (horizontalInput == 0)
            {
                rigidBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 0);
            }
            else
                rigidBody.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 5, 10);

            jumpCooldown = 0;
        }
    }

    private bool IsGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        print(raycastHit.collider);
        return raycastHit.collider != null;


        /*
        Vector2 position = rigidBody.position;
        Vector2 direction = Vector2.down;
        float distance = 1.0f;
    
        Debug.DrawRay(position, direction, Color.green);
        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
        if (hit.collider != null) {
            return true;
        }

        return false;
        */
    }

    private bool CanDoubleJump()
    {
        if (doubleJump) {
            doubleJump = false;
            return true;
        }

        return false;
    }

    private bool OnWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    private void Flip()    
    {
        facingRight = !facingRight;
  
        Vector3 spriteScale = transform.localScale;
        spriteScale.x *= -1;
        transform.localScale = spriteScale;
        
    }


}