using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static int SpawnCount = Random.Range(0, 2);

    public static int Spawn()
    {
        SpawnCount = Random.Range(0, 2);
        return SpawnCount;
    }
}
