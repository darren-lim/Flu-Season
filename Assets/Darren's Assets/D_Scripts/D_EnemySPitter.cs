using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnemySPitter : D_EnemyAbstract
{
    public float Speed = 3f;
    private GameObject player;
    public GameObject EBullet;
    public float BulletSpeed = 8f;
    bool spitting = false;
    bool dead = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        spitting = false;
        dead = false;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        if (player != null)
        {
            if (!spitting && Vector3.Distance(this.transform.position, player.transform.position) < 5f)
                StartCoroutine(Spit());
            else if(!spitting)
                transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, Speed * Time.deltaTime);
        }
    }

    IEnumerator Spit()
    {
        spitting = true;
        yield return new WaitForSeconds(.8f);
        Vector2 direction = player.transform.position - this.transform.position;
        direction.Normalize();

        GameObject clone = Instantiate(EBullet, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);
        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        clone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z);
        clone.GetComponent<Rigidbody2D>().velocity = direction * BulletSpeed;
        spitting = false;
        yield return null;
    }
    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(2);
            this.gameObject.SetActive(false);
        }
    }*/

    public override void TakeDamage()
    {
        if (dead)
            return;
        GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(2);
        dead = true;
        this.gameObject.SetActive(false);
    }
}
