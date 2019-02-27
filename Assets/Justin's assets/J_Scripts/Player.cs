using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //movement
    private Rigidbody2D m_rb;
    [SerializeField] protected float speed = 4f;


    // Start is called before the first frame update
    void Start()
    {
        m_rb = this.GetComponent<Rigidbody2D>();
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
        Vector2 currentVelocity = m_rb.velocity;

        m_rb.velocity = new Vector2(moveX * speed, moveY * speed);


    }
}