using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Enemy3 : MonoBehaviour
{
    public int Speed = 3;
    public string Type;
    private GameObject player;
    private float wait_seconds = 2.0f;
    private float start_time = 0.0f;
    //Renderer m_ObjectRenderer;
    SpriteRenderer rend;

    // Start is called before the first frame update
    void Start()
    {
        start_time = Time.time + wait_seconds;
        rend = GetComponent<SpriteRenderer>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
    }
}
