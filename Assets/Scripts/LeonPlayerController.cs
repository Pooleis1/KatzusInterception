using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeonPlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Rigidbody2D rigidBody;
    private CapsuleCollider2D collider;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jump = 5;
    private float jumpCooldown = 0;
    private bool doubleJump = true;
    private bool facingRight = true;


    private void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    void Update() {
        if (jumpCooldown > 0.2f) {
            rigidBody.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rigidBody.velocity.y);
            if (OnWall() && !IsGrounded()) {
                rigidBody.gravityScale = 8;
                rigidBody.velocity = Vector2.zero;
            }
            else {
                rigidBody.gravityScale = 2.5f;
            }
            if (Input.GetKeyDown(KeyCode.Space)) {
                Jump();
            }
        }
        else {
            jumpCooldown += Time.deltaTime;
        }

        if (IsGrounded() && doubleJump == false)
        {
            doubleJump = true;
        }

        if (Input.GetAxis("Horizontal") > 0 && !facingRight)
        {
            Flip();
        }
        else if (Input.GetAxis("Horizontal") < 0 && facingRight)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (IsGrounded() || CanDoubleJump()) {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jump);
        }
        else if (OnWall() && !IsGrounded()) {
            jumpCooldown = 0;
            rigidBody.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * speed, jump * 2);
        }
    }

    public bool IsGrounded() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    public bool CanDoubleJump()
    {
        if (doubleJump) {
            doubleJump = false;
            return true;
        }

        return false;
    }

    public bool OnWall() {
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    
    protected void Flip()    
    {
        facingRight = !facingRight;
  
        Vector3 spriteScale = transform.localScale;
        spriteScale.x *= -1;
        transform.localScale = spriteScale;
        
    }


}