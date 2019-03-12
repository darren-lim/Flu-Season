using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_ChickenScript : D_EnemyAbstract
{
    public float Speed = 5f;
    public int lives = 8;
    private GameObject player;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        lives = 8;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        if (player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, Speed * Time.deltaTime);
    }

    public override void TakeDamage()
    {
        if (lives == 1)
        {
            if (dead)
                return;
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(10);
            dead = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            lives--;
        }
    }
}
