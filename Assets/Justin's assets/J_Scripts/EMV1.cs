using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMV1 : MonoBehaviour
{
    private GameObject player;
    public float speed = 2f;
    
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player");
        transform.position = Vector2.MoveTowards(transform.position, player.GetComponent<Transform>().position, speed * Time.deltaTime);
    }
}
