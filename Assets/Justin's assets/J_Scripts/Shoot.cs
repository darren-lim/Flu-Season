using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet;
    private GameObject clone;
    private float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
     if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) /*&& clone == null*/)
        {
            //shoot bullet (spawn or tp?)
            Pew();
        }
    }

    void Pew()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)((worldMousePos - transform.position));
        direction.Normalize();

        clone = Instantiate(bullet, transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);

        clone.GetComponent<Rigidbody2D>().velocity = direction * speed;

    }
}
