using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy3 : D_EnemyAbstract
{
    public int Speed = 3;
    int lives = 3;
    public string Type;
    private GameObject player;
    private float wait_seconds = 2.0f;
    private float start_time = 0.0f;
    //Renderer m_ObjectRenderer;
    SpriteRenderer rend;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time + wait_seconds;
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        lives = 3;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        if (player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, Speed * Time.deltaTime);

        if (start_time <= Time.time)
        {
            if (rend.enabled == false)
            {
                rend.enabled = true;
            }
            else // Make it invisible
            {
                rend.enabled = false;
            }

            start_time = Time.time + wait_seconds;
        }
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            if (lives == 1)
            {
                GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(5);
                this.gameObject.SetActive(false);
            }
            else
            {
                lives--;
            }

        }
    }
    */
    public override void TakeDamage()
    {
        if (lives == 1)
        {
            if (dead)
                return;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(5);
            dead = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            lives--;
        }
    }
}