using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class D_EnemyAbstract : MonoBehaviour
{
    protected int Speed;
    protected float Damage;
    protected float Health;
    protected string EnemyType;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void Move();
    public abstract int DealDamage();

    //reposition the enemy randomly on the left side of the map
    void Reposition()
    {
        float randY = Random.Range(-5f, 6f);
        this.transform.position = new Vector3(-8, randY, 0);
    }
}
