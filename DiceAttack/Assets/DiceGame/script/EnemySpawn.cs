using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawn : MonoBehaviour
{
    public static int spawnCount;
    public GameObject spawner;

    public static int SpawnCount()
    {
        spawnCount = Random.Range(1, 3);
        return spawnCount;
    }

    public void Spawn()
    {
            for (int i = 0; i < spawnCount; i++)
            {
                TurnManager.Instance.monsters[i].transform.position = spawner.transform.GetChild(i).position;
            }
    }
}
