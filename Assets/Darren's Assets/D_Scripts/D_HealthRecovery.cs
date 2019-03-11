using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_HealthRecovery : MonoBehaviour
{
    public float DisableCounter = 8f;
    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(-12f, 12f), Random.Range(-12f, 12f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        DisableCounter -= Time.deltaTime;
        if(DisableCounter < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int pLives = collision.gameObject.GetComponent<D_PlayerTestScript>().lives;
            if(pLives < 5)
            {
                collision.gameObject.GetComponent<D_PlayerTestScript>().lives++;
                FindObjectOfType<D_AudioManager>().Play("Heal");
                Destroy(this.gameObject);
            }
        }
    }
}
