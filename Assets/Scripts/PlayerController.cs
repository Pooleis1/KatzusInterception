using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Alle Public Attribute werden automatisch Seralizied
    public float movementSpeed;
    //Mit dem SerializeField kann man auch private Attribute Seralizied
    [SerializeField]
    private float JumpForce;

    public GameObject map;
    private bool keyIsDown;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starten");
        keyIsDown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m")&&keyIsDown==false)
        {
            Instantiate(map,new Vector3(0,0,0), Quaternion.identity);

            keyIsDown = true;
        }

        if (Input.GetKeyDown("m") && keyIsDown == true)
        {
            DestroyImmediate(map, true);
            keyIsDown = false;
        }
    }
}
