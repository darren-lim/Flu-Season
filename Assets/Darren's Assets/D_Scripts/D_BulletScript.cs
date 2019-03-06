using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_BulletScript : MonoBehaviour
{
    [SerializeField] protected float speed = 15.0f;
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
        if (collider.CompareTag("Enemy"))
        {
            collider.gameObject.SetActive(false);
            GameObject.FindGameObjectWithTag("LevelManager").GetComponent<D_SimpleLevelManager>().EnemyKill(1);
            //for next enemies, have health?
            Destroy(this.gameObject);
        }

        else if (collider.CompareTag("FatEnemy"))
        {
            Destroy(this.gameObject);
        }

        else if (collider.CompareTag("Enemy3"))
        {
            Destroy(collider.gameObject);
            Destroy(this.gameObject);
        }

    }
}
