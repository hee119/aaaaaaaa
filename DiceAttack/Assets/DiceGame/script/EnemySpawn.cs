using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public static int SpawnCount = Random.Range(0, 3);

    public static int Spawn()
    {
        SpawnCount = Random.Range(0, 3);
        return SpawnCount;
    }
}
