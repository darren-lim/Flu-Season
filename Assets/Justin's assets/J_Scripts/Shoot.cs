using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    public GameObject bullet;
    private float offset = 0.5f;
    private float speed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
     if ((/*Input.GetKeyDown(KeyCode.Space) || */Input.GetKeyDown(KeyCode.Mouse0)) /*&& clone == null*/)
        {
            //shoot bullet (spawn or tp?)
            Pew();
        }
    }

    void Pew()
    {
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (Vector2)((worldMousePos - this.transform.position));
        direction.Normalize();

        GameObject clone = Instantiate(bullet, this.transform.position + (Vector3)(direction * 0.5f), Quaternion.identity);

        float rotation_z = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        clone.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotation_z + offset);

        clone.GetComponent<Rigidbody2D>().velocity = direction * speed;

    }
}
