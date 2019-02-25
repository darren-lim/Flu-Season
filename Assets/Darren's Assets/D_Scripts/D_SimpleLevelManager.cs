using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_SimpleLevelManager : MonoBehaviour
{
    public static D_SimpleLevelManager current;
    public int wave = 0;
    //cannot be higher than number of spawned enemies in the spawner script.
    public int NumberOfEnemiesToSpawn = 5;
    public int MinSpawn = 5;
    public int SpawnEnemyIndex = 0;
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
        int enemyListLen = EnemyObjList.Count;

        if (enemyListLen > SpawnEnemyIndex + 1)
        {
            if (wave == 3)
            {
                SpawnEnemyIndex++;
            }
            else if (wave == 5)
            {
                SpawnEnemyIndex++;
            }
        }
        NumberOfEnemiesToSpawn = MinSpawn += wave;
        StartCoroutine(SpawnEnemies(SpawnEnemyIndex));
        //start again
        //make couroutine
    }
    /*
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
    */
    IEnumerator SpawnEnemies(int EnemyNum)
    {
        for (int i = 0; i <= EnemyNum; i++)
        {
            int SpawnNumber = NumberOfEnemiesToSpawn; //to initialize
            if(EnemyNum != 0)
            {
                SpawnNumber = Random.Range(NumberOfEnemiesToSpawn / 2, NumberOfEnemiesToSpawn-1); //so at least SOME enemies spawn
            }
            //check last iteration of index
            if(i == EnemyNum && NumberOfEnemiesToSpawn > 0)
            {
                SpawnNumber = NumberOfEnemiesToSpawn;
            }
            for (int k = 0; k < SpawnNumber; k++)
            {
                //need to check which enemy is which
                GameObject EnemyObj = SpawnerScript.GetPooledObject(i);
                if (EnemyObj == null)
                    break;
                EnemyObj.SetActive(true);
                yield return new WaitForSeconds(Random.Range(0.2f, 0.6f));
            }
            NumberOfEnemiesToSpawn -= SpawnNumber;
        }
        yield return null;
    }
}
