using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnemyTestScript : MonoBehaviour
{
    public int Speed = 3;
    public string Type;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        //Reposition();
    }

    void Reposition()
    {
        float randY = Random.Range(-5f, 5f);
        this.transform.position = new Vector3(-8, randY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        if (player != null)
            transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
