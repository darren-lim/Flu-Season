using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mines : MonoBehaviour
{
    [SerializeField] protected float speed = 15.0f;
    private Vector3 dir;
    private int health = 10;
    
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
    private bool already = false;
    void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (collider.CompareTag("Enemy") && !already)
        {
            already = true;
            //for next enemies, have health?
            FindObjectOfType<D_AudioManager>().Play("EnemyDeath");

            health--;
            
        }

        already = false;
    }
}
