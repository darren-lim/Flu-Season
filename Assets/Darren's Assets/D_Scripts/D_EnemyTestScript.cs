using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnemyTestScript : MonoBehaviour
{
    public int Speed = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Reposition();
    }

    void Reposition()
    {
        float randY = Random.Range(-6f, 6f);
        this.transform.position = new Vector3(-8, randY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(Time.deltaTime * Speed, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //subtract health whatever
        if(collision.gameObject.tag == "Wall")
            this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            this.Reposition();
    }
}
