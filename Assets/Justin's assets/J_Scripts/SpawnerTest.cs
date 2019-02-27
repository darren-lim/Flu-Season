using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerTest : MonoBehaviour
{
    public GameObject enemy;               
    public float spawnTime = 3f;
    public float currentST = 0.0f;
    public Transform[] spawnPoints;
    public int waves = 0;
    public int waveUp = 5;
    public float speedUp = .75f;
    int cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        if(cnt >= waveUp && waves < 5)
        {
            waves++;
            cnt = 0;
            waveUp *= 2;
            spawnTime *= speedUp;
            InvokeRepeating("Spawn", spawnTime, spawnTime);
            currentST = spawnTime;
        }
    }

    void Spawn()
    {
        cnt++;
        // Find a random index between zero and one less than the number of spawn points.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
        Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
