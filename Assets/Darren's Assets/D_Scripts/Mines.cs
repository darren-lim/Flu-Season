using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{
    //[SerializeField] protected float speed = 15.0f;
    //private Vector3 dir;
    private int health = 3;
    float timer = 6f;
    
    // Start is called before the first frame update
    void Start()
    {
        health = 3;

    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }

    }
    private bool already = false;
    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Enemy") && !already)
        {
            already = true;
            //for next enemies, have health?
            FindObjectOfType<D_AudioManager>().Play("EnemyDeath");
            collider.GetComponent<D_EnemyAbstract>().TakeDamage();
            health--;
            if (health < 0)
            {
                Destroy(gameObject);
            }
        }

        already = false;
    }
}
