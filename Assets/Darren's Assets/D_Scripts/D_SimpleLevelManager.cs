using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class D_SimpleLevelManager : MonoBehaviour
{
    public static D_SimpleLevelManager current;
    public int wave = 0;
    //cannot be higher than number of spawned enemies in the spawner script.
    public int NumberOfEnemiesToSpawn = 5;
    public int AddEnemyToSpawnWave = 4; //after every num waves, a new enemy will spawn.

    public TextMeshProUGUI WaveText; // maybe create a ui manager?

    [Header("Pls dont touch")]
    public int MinSpawn = 5;
    public int SpawnEnemyIndex = 0;
    public int LastNewEnemyWave = 0;

    D_EnemySpawnerScript SpawnerScript;
    List<GameObject> EnemyObjList;
    bool spawning;

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
        WaveText.gameObject.SetActive(false);
        spawning = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !spawning)
        {
            NextWave();
        }
    }

    public void NextWave()
    {
        spawning = true;
        wave++;
        int enemyListLen = EnemyObjList.Count;

        if (enemyListLen > SpawnEnemyIndex + 1)
        {
            if (wave == LastNewEnemyWave + AddEnemyToSpawnWave)
            {
                SpawnEnemyIndex++;
                LastNewEnemyWave = wave;
            }
        }
        NumberOfEnemiesToSpawn = MinSpawn + (int)(wave*1.5);
        StartCoroutine(SpawnEnemies(SpawnEnemyIndex));
    }
    //add boss wave?
    IEnumerator SpawnEnemies(int EnemyNum)
    {
        //show wave number
        WaveText.gameObject.SetActive(true);
        WaveText.text = "WAVE: " + wave.ToString();
        yield return new WaitForSeconds(2);
        WaveText.gameObject.SetActive(false);
        //start spawning
        for (int i = 0; i <= EnemyNum; i++)
        {
            int SpawnNumber = NumberOfEnemiesToSpawn; //to initialize
            //if there is more than one type of enemy
            if(EnemyNum != 0)
            {
                SpawnNumber = Random.Range(NumberOfEnemiesToSpawn / 2, NumberOfEnemiesToSpawn-1); //so at least SOME enemies spawn
            }
            //check if we are at last iteration of index
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
        spawning = false;
        yield return null;
    }
}
