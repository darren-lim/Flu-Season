using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnemyTestScript : D_EnemyAbstract
{
    public int Speed = 3;
    public string Type;
    private GameObject player;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        dead = false;
    }


    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        if (player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, Speed * Time.deltaTime);
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(1);
            this.gameObject.SetActive(false);
        }
    }*/
    public override void TakeDamage()
    {
        if (dead)
            return;
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(1);
        dead = true;
        this.gameObject.SetActive(false);
    }
}
