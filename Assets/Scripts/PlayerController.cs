using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //Alle Public Attribute werden automatisch Seralizied
    public float movementSpeed = 7f;
    //Mit dem SerializeField kann man auch private Attribute Seralizied
    [SerializeField] private float jumpForce = 4f;
    
    private Rigidbody2D transform;
    

    public GameObject map;
    private bool keyIsDown;



    private bool isGrounded = true;
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starten");
        keyIsDown = false;

        transform = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetAxis gibt einen wert von -1,0,1 zurück bei drücken von den a,nichts,d
        float h = Input.GetAxis("Horizontal");
        
        
        //bewegt den Spieler um die X Achse vorwärts. -1*movespeed,0,1*movespeed
        transform.velocity = new Vector2(h * movementSpeed, transform.velocity.y);

        if (isGrounded)
        {
            float v = Input.GetAxis("Vertical");
            transform.velocity = new Vector2(transform.velocity.x, v*jumpForce);
            // isGrounded = false;
        }
        
        /*RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, collider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;

        if ()
        {
            isGrounded = true;
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
    
}
