using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class D_EnemySpawnerScript : MonoBehaviour
{
    public static D_EnemySpawnerScript current;
    //List of different enemies to spawn. CANNOT BE NULL
    //Put all of the enemies here from the inspector, or can add later in code
    public List<GameObject> EnemyObjects;
    //pool of enemies to store in
    List<GameObject> pooledEnemyObjects;
    //multiply by list length to store that many enemies
    public int EachEnemyPoolAmount = 10;

    // Start is called before the first frame update
    void Awake()
    {
        if (current == null)
            current = this;
        else
            Destroy(this.gameObject);
    }

    private void Start()
    {
        //Initialize a set amount of each enemy
        if(EnemyObjects.Count == 0)
        {
            Debug.LogError("EnemyObjects list is empty. Fill with enemies.");
        }
        pooledEnemyObjects = new List<GameObject>();
        for (int i = 0; i < EnemyObjects.Count; i++)
        {
            for (int k = 0; k < EachEnemyPoolAmount; k++)
            {
                GameObject obj = (GameObject)Instantiate(EnemyObjects[i]);
                obj.SetActive(false);
                pooledEnemyObjects.Add(obj);
            }
        }
        //TotalEnemyCount = EnemyObjects.Count * EnemyPoolAmount;
    }
    //gets an object from the list pooledObjects.
    public GameObject GetPooledObject(int index)
    {
        for (int i = 0; i < pooledEnemyObjects.Count; ++i)
        {
            if (!pooledEnemyObjects[i].activeInHierarchy && pooledEnemyObjects[i].name == string.Format("{0}(Clone)", EnemyObjects[index].name))
            {
                return pooledEnemyObjects[i];
            }
        }
        return null;
    }
}
