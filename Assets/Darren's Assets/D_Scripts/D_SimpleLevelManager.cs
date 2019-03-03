using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class D_SimpleLevelManager : MonoBehaviour
{
    public static D_SimpleLevelManager current;
    public int wave = 0;
    //cannot be higher than number of spawned enemies in the spawner script.
    public int NumberOfEnemiesToSpawn = 5;
    public int AddEnemyToSpawnWave = 4; //after every num waves, a new enemy will spawn.
    public GameObject[] SpawnPointsList; //PLEASE FILL IN INSPECTOR

    public TextMeshProUGUI WaveText; // maybe create a ui manager?

    public UnityEvent OnTakeDamage;
    public UnityEvent OnEnemyKill;
    public int Score;
    public int playerLives;

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
        SpawnPointsList = GameObject.FindGameObjectsWithTag("SpawnPoint");
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player") == null)
        {
            GameOver();
        }
        //if there are no enemies on the field, spawn next wave
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
        //from the list of enemies, if there is more than one, we add the next
        //enemy onto the field after a set amount of waves.
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
                {
                    EnemyObj = SpawnerScript.PoolMoreEnemies(i);
                }
                EnemyObj.SetActive(true);
                int RandSpawnPoint = Random.Range(0, SpawnPointsList.Length);
                EnemyObj.transform.position = SpawnPointsList[RandSpawnPoint].transform.position;
                yield return new WaitForSeconds(Random.Range(0.2f, 0.6f));
            }
            NumberOfEnemiesToSpawn -= SpawnNumber;
        }
        spawning = false;
        yield return null;
    }

    public void EnemyKill(int addScore)
    {
        Score += addScore;
        OnEnemyKill.Invoke();
    }
    public void PlayerTakeDamage()
    {
        OnTakeDamage.Invoke();
    }
    //game over, set time to 0
    public void GameOver()
    {
        Time.timeScale = 0;
    }
}
