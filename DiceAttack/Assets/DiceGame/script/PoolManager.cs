using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    public GameObject enemy;
    
    private IObjectPool<StatManager> statPool;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        statPool = new ObjectPool<StatManager>(createEnemy, OnGetEnemy, OnReleasEnemy, OnDestroyEnemy, maxSize: 20);
        StartCoroutine(GetEnemy());
    }

    IEnumerator GetEnemy()
    {
        while (true)
        {
            var enemy = statPool.Get();
            yield return new WaitForSeconds(1f);
        }
    }

    private StatManager createEnemy()
    {
        StatManager _enemy = Instantiate(enemy).GetComponent<StatManager>();
        _enemy.SetStatPool(statPool);
        return _enemy;
    }

    private void OnGetEnemy(StatManager enemy)
    {
        enemy.gameObject.SetActive(true);
    }
    private void OnReleasEnemy(StatManager enemy)
    {
        enemy.gameObject.SetActive(false);
    }
    private void OnDestroyEnemy(StatManager enemy)
    {
        Destroy(enemy.gameObject);
    }
}