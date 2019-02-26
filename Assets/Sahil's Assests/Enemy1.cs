using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed;

    void Start()
    {
        speed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(Time.deltaTime * speed, 0, 0));
    }

    private void OnEnable()
    {
        Reposition();
    }

    private void Reposition()
    {
        float randY = Random.Range(-5f, 5f);
        this.transform.position = new Vector3(-8, randY, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
            this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            this.Reposition();

        else if (collision.gameObject.tag == "Vaccine")
        {
            Destroy(this.gameObject);
        }

    }
}

