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
            collider.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(1);
            //for next enemies, have health?
            health--;
            
        }

        else if (collider.CompareTag("FatEnemy") && !already)
        {
            already = true;
            health--;
        }

        else if (collider.CompareTag("Enemy3") && !already)
        {
            already = true;
            Destroy(collider.gameObject);
            health--;
        }
        if(health <= 0)
            Destroy(this.gameObject);

        already = false;
    }
}
