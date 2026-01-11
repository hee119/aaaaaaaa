using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public int spawnCount;
    public GameObject spawner;
    private string sceneName;

    void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
    }
    public int SpawnCount()
    {
        if (sceneName == "6")
        {
            spawnCount = 1;
            return spawnCount;
        }
        else
        {
            spawnCount = Random.Range(1, 4);
            return spawnCount;
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            TurnManager.Instance.monsters[i].transform.position = spawner.transform.GetChild(i).position;
        }
    }
}