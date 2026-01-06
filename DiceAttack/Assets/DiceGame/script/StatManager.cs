using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class StatManager : MonoBehaviour
{
    public int hp;
    public int attack;
    public bool isDead;
    
    private IObjectPool<StatManager> statPool;

    void Start()
    {
        statPool = PoolManager.Instance.statPool;
    }
    public void Attacked(int attack)
    {
        hp -= attack;
        if (hp <= 0)
        {
            isDead = true;
            Die();
        }
    }

    public void SetStatPool(IObjectPool<StatManager> pool)
    {
        statPool = pool;
    }
    
    void Die()
    {
        if (CompareTag("Player"))
        {
            gameObject.SetActive(false);
            return;
        }

        statPool.Release(this);
        Debug.Log($"나주금{this}");
    }
    private void OnEnable()
    {
        isDead = false;
        hp = 100; // 또는 maxHp
    }

}