using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //movement
    private Rigidbody2D rb;
    private Collider2D cd;          //private float cdcd = 3f;

    [SerializeField] protected float speed = 4f;


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //WHY DIDN'T YOU DODGE
            Dodge();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Kick hospital bed
            KickBed();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Move();

    }
    void Move()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
    
        Vector2 mvmt = new Vector2(moveX, moveY);
        Vector2 currentVelocity = rb.velocity;

        rb.velocity = new Vector2(moveX * speed, moveY * speed);


    }
    void Dodge()
    {


    }
    void KickBed()
    {

    }
    
}