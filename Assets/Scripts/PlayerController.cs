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


    private CapsuleCollider2D collider;
    [SerializeField] private LayerMask groundLayer;
    

    public GameObject map;
    private bool keyIsDown;
    
    
    
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starten");
        keyIsDown = false;

        transform = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //GetAxis gibt einen wert von -1,0,1 zurück bei drücken von den a,nichts,d
        float h = Input.GetAxis("Horizontal");
        
        //bewegt den Spieler um die X Achse vorwärts. -1*movespeed,0,1*movespeed
        transform.velocity = new Vector2(h * movementSpeed, transform.velocity.y);


        if (isGrounded())
        {
            float v = Input.GetAxis("Vertical");
            transform.velocity = new Vector2(transform.velocity.x, v*jumpForce);
            // isGrounded = false;
        }


        /**if ()
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

    /**
     * Schaut ob er in der Luft ist oder nicht
     * @return Ob er sich Am Boden befindet oder nicht
     */
    private bool IsGrounded()
    {
        //Sagt das er nur !null ist wenn er den spezifischen groundLayer unten berührt
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(collider.bounds.center, collider.bounds.size,0, 0, Vector2.down, 0.1f, groundLayer);
        print(raycastHit.collider);
        //gibt null zurück der Collider nicht den ground berührt
        return raycastHit.collider != null;
    }

}
