using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy2 : D_EnemyAbstract
{
    public float Speed = .01f;
    private int e_lives = 3;
    private GameObject player;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        e_lives = 3;
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
            if (e_lives == 1)
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(3);
                this.gameObject.SetActive(false);
            }
            else
            {
                e_lives--;
            }
            
        }
    }*/

    public override void TakeDamage()
    {
        if (e_lives == 1)
        {
            if (dead)
                return;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(3);
            dead = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            e_lives--;
        }
    }
}
