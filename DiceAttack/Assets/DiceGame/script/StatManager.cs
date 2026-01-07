using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class StatManager : MonoBehaviour
{
    public int hp;
    public int attack;
    public bool isDead;
    public int defense;
    
    private IObjectPool<StatManager> statPool;

    void Start()
    {
        statPool = PoolManager.Instance.statPool;
    }
    public void Hit(int attack)
    {
        if(defense < attack)
        hp -= Mathf.Abs(attack - defense);
        else
        {
            Debug.Log("완전방어에 성공했습니다.");
        }

        defense = 0;
        if (hp <= 0 && !isDead)
        {
            isDead = true;
            Die();
        }
    }

    public void Defense(int defense)
    {
        this.defense = defense;
        Debug.Log("방어중입니다.");
    }

    public void SetStatPool(IObjectPool<StatManager> pool)
    {
        statPool = pool;
    }
    
    void Die()
    {
        statPool.Release(this);
        Debug.Log($"나주금{this}");
    }
    private void OnEnable()
    {
        isDead = false;
        hp = 100; // 또는 maxHp
    }

}