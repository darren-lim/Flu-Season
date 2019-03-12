using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnemyBullet : MonoBehaviour
{
    [SerializeField] protected float speed = 8.0f;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }
    }
}
