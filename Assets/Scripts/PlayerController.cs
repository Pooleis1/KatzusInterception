using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    //Alle Public Attribute werden automatisch Seralizied
    public float movementSpeed = 7f;
    //Mit dem SerializeField kann man auch private Attribute Seralizied
    [SerializeField] private float jumpForce = 3f;
    
    private Rigidbody2D transform;
    
    private CapsuleCollider2D collider;
    [SerializeField] private LayerMask groundLayer;

    private bool doubleJump = true;
    


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starten");
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

        if (Input.GetKeyDown("w")) // w drücken
        {
            Jump();
        }
        
        // Wenn der Spieler den Boden berührt, wird seine Double-Jump Funktion wieder "aufgeladen"
        if (IsGrounded() && doubleJump == false)
        {
            doubleJump = true;
        }


    }

    /**
     * Lässt den Spieler Jumpen
     * Double Jumpen
     * normal Jumpen
     * Wall Jumps
     */
    private void Jump()
    {
        // Wenn der Spieler am Boden ist/Double Jumpen kann
        if (IsGrounded())
        {
            transform.velocity += new Vector2(0, jumpForce); //fügt y jumpForce hinzu
        }
        
        if (CanDoubleJump())
        {
            transform.velocity += new Vector2(0, jumpForce);
        }
            //Wenn der Spieler an einer Wand ist
        /*else
        {
            if()
            {
                
            }
        }*/


    }

    /**
     * Schaut ob er in der Luft ist oder nicht
     * @return Ob er sich Am Boden befindet oder nicht
     */
    private bool IsGrounded()
    {
        //Sagt das er nur !null ist wenn er den spezifischen groundLayer unten berührt
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(collider.bounds.center, collider.bounds.size, CapsuleDirection2D.Vertical, 0, Vector2.down, 0.1f, groundLayer);
        print(raycastHit.collider);
        //gibt null zurück der Collider nicht den ground berührt
        return raycastHit.collider != null;
    }
    
    /**
     * Überprüft ob er doubleJumpen kann stellt es aber aus
     * @return ob er doubleJumpen kann oder nicht
     */
    private bool CanDoubleJump()
    {
        if (doubleJump) {
            doubleJump = false;
            return true;
        }

        return false;
    }

}
