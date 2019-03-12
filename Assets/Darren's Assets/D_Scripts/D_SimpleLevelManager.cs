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
    public int NumberOfEnemiesToSpawn = 10;
    public int MinSpawn = 10;
    public int AddEnemyToSpawnWave = 1; //after every num waves, a new enemy will spawn.
    public GameObject[] SpawnPointsList; //PLEASE FILL IN INSPECTOR

    public TextMeshProUGUI WaveText; // maybe create a ui manager?
    public TextMeshProUGUI GOText;

    public UnityEvent OnTakeDamage;
    public UnityEvent OnEnemyKill;
    public UnityEvent OnGameOver;
    public UnityEvent OnNewWave;
    public int EnemiesLeft;
    public int Score;
    public int playerLives;

    public GameObject Heart;
    public float SpawnHeartCounter = 10f;
    public GameObject Boss;

    [Header("Pls dont touch")]
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
        {
            Destroy(this.gameObject);
            return;
        }
        Time.timeScale = 1;
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
        SpawnHeartCounter -= Time.deltaTime;
        if (SpawnHeartCounter < 0)
        {
            SpawnHeart();
            SpawnHeartCounter = Random.Range(7f, 11f);
        }
    }

    public void NextWave()
    {
        spawning = true;
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
        wave++;
        if (wave > 10)
        {
            GameOver();
            return;
        }
        NumberOfEnemiesToSpawn = MinSpawn + (int)(wave*2);
        EnemiesLeft = NumberOfEnemiesToSpawn+1;
        if (wave == 10)
            EnemiesLeft = 2;
        EnemyKill(0);
        NewWave();
        StartCoroutine(SpawnEnemies(SpawnEnemyIndex));
    }
    //add boss wave?
    IEnumerator SpawnEnemies(int EnemyNum)
    {
        //show wave number
        WaveText.gameObject.SetActive(true);
        WaveText.text = "WAVE: " + wave.ToString();
        if (wave == 10)
            WaveText.text = "Boss Battle";
        yield return new WaitForSeconds(2);
        WaveText.gameObject.SetActive(false);
        if (wave == 10) //bosswave
        {
            //spawn boss
            int RandSpawnPoint = Random.Range(0, SpawnPointsList.Length);
            GameObject clone = Instantiate(Boss);
            clone.transform.position = SpawnPointsList[RandSpawnPoint].transform.position;
            yield break;
        }
        //start spawning
        float[] SpawnWeights;
        int TotalEnemiesLeft = NumberOfEnemiesToSpawn;
        for (int i = 0; i <= EnemyNum; i++)
        {
            int SpawnNumber = TotalEnemiesLeft; //to initialize
            //if there is more than one type of enemy
            if(wave < 6)
            {
                if (EnemyNum != 0)
                {
                    SpawnNumber = Random.Range(TotalEnemiesLeft / 2 - 2, TotalEnemiesLeft - 3); //so at least SOME enemies spawn
                }
                //check if we are at last iteration of index
                if (i == EnemyNum && TotalEnemiesLeft > 0)
                {
                    SpawnNumber = TotalEnemiesLeft;
                }
            }
            else
            {
                //All enemies have been spawned
                //add weights to spawning enemies, more towards powerful ones.
                
                if (wave == 6) //20 30 30 10 10
                {
                    SpawnWeights = new float[] { 0.2f, 0.3f, 0.3f, 0.1f, 0.1f };
                    SpawnNumber = (int)(NumberOfEnemiesToSpawn*SpawnWeights[i]);
                }
                else if(wave == 7) //10 20 30 20 10
                {
                    SpawnWeights = new float[] { 0.1f, 0.2f, 0.3f, 0.2f, 0.1f };
                    SpawnNumber = (int)(NumberOfEnemiesToSpawn * SpawnWeights[i]);
                }
                else if(wave == 8) //0 10 30 30 30
                {
                    SpawnWeights = new float[] { 0f, 0.1f, 0.3f, 0.3f, 0.3f };
                    SpawnNumber = (int)(NumberOfEnemiesToSpawn * SpawnWeights[i]);
                }
                else if(wave == 9) //0 10 20 40 30
                {
                    SpawnWeights = new float[] { 0.0f, 0.1f, 0.2f, 0.4f, 0.3f };
                    SpawnNumber = (int)(NumberOfEnemiesToSpawn * SpawnWeights[i]);
                }
                else if (wave > 10)
                {
                    GameOver(); //failsafe
                }
                if (i == EnemyNum && TotalEnemiesLeft > 0 && wave < 10)
                {
                    SpawnNumber = TotalEnemiesLeft;
                }
            }
            //finally spawn enemies
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
            TotalEnemiesLeft -= SpawnNumber;
        }
        spawning = false;
        yield return null;
    }

    public void SpawnHeart()
    {
        Instantiate(Heart);
    }

    public void EnemyKill(int addScore)
    {
        Score += addScore;
        EnemiesLeft -= 1;
        OnEnemyKill.Invoke();
    }
    public void PlayerTakeDamage()
    {
        OnTakeDamage.Invoke();
    }
    public void NewWave()
    {
        OnNewWave.Invoke();
    }
    //game over, set time to 0
    public void GameOver()
    {
        if (wave > 10)
        {
            GOText.text = "You Win";
        }
        else
        {
            GOText.text = "Game Over";
        }
        OnGameOver.Invoke();
    }
}
