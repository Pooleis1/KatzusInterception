using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //Alle Public Attribute werden automatisch Seralizied
    public float moveSpeed = 3f;
    //Mit dem SerializeField kann man auch private Attribute Seralizied
    [SerializeField] private float jumpForce = 4f;
    
    private Rigidbody2D rb;
    

    public GameObject map;
    private bool keyIsDown;

    private bool canDoubleJump = true;
    private CapsuleCollider2D collider;

    private LayerMask platformLayer = 8;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starten");
        keyIsDown = false;

        //Wir nehmen ein Component von unserm Game und dieses Gameobject wird zu einem Rigidbody formatiert
        rb = gameObject.GetComponent<Rigidbody2D>();

        collider = gameObject.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetAxis gibt einen wert von -1,0,1 zurück bei drücken von den a,nichts,d
        float h = Input.GetAxis("Horizontal");
 
        //bewegt den Spieler um die X Achse vorwärts. -1*movespeed,0,1*movespeed
        rb.velocity = new Vector2(h * moveSpeed, rb.velocity.y);


        /*if (IsGrounded())
        {
            canDoubleJump = true;
        }*/

        if (IsGrounded())
        {
            float v = Input.GetAxis("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, v*jumpForce);
        } /*else
        {
            if (canDoubleJump)
            {
                float v = Input.GetAxis("Vertical");
                rb.velocity = new Vector2(rb.velocity.x, v*jumpForce);
                canDoubleJump = false;
            }
        }*/



        /*if (Input.GetKeyDown("m")&&keyIsDown==false)
        {
            Instantiate(map,new Vector3(0,0,0), Quaternion.identity);

            keyIsDown = true;
        }

        if (Input.GetKeyDown("m") && keyIsDown == true)
        {
            DestroyImmediate(map, true);
            keyIsDown = false;
        }*/
        

    }

    private bool IsGrounded()
    {

        RaycastHit2D raycastHit = Physics2D.CapsuleCast(collider.bounds.center, collider.bounds.size, 0,0f, Vector2.down, 0.1f,platformLayer);
        Debug.Log(raycastHit.collider);
        return raycastHit.collider != null;
    }
}
