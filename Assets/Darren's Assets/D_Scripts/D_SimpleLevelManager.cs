using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_SimpleLevelManager : MonoBehaviour
{
    public static D_SimpleLevelManager current;
    public int wave = 0;
    //cannot be higher than number of spawned enemies in the spawner script.
    public int NumberOfEnemiesToSpawn = 5;
    D_EnemySpawnerScript SpawnerScript;
    List<GameObject> EnemyObjList;

    void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        SpawnerScript = GetComponent<D_EnemySpawnerScript>();
        EnemyObjList = SpawnerScript.EnemyObjects;
    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.FindGameObjectsWithTag("Enemy").Length == 0)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        wave++;
        int[] index = {0};
        /*
        if (wave > 2)
        {
            index = new int[] {0,1};
        }
        else if(wave > 5)
        {
            index = new int[] { 0, 1, 2 };
        }
        */
        EnableEnemies(index);
        //start again
        //make couroutine
    }

    void EnableEnemies(int[] EnemyIndex)
    {
        //choose enemies to spawn
        //do we want random?
        for(int i = 0; i<NumberOfEnemiesToSpawn; i++)
        {
            //need to check which enemy is which
            GameObject EnemyObj = SpawnerScript.GetPooledObject();
            if (EnemyObj == null)
                break;
            EnemyObj.SetActive(true);
        }
    }
}
