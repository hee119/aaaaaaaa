using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    GameObject playerObj;
    private StatManager playerStats;
    private int attack;
    private int defense;
    private int shareStats;
    private StatManager MonsterStats;
    enum Action { Attack, Depense }

    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("플레이어가 없습니다.");
            return;
        }
        playerStats = playerObj.GetComponent<StatManager>();
        MonsterStats = GetComponent<StatManager>();
        attack = MonsterStats.attack;
        defense = MonsterStats.defense;
        
    }
    void OnEnable()
    {if (!TurnManager.Instance.monsters.Contains(gameObject))
        {
            TurnManager.Instance.monsters.Add(gameObject);
        }
    }

    public IEnumerator MonsterTurnStart()
    {
        Debug.Log(1);
        if (MonsterStats.isDead)
        {
            TurnManager.Instance.monsters.Remove(gameObject);
            yield break;
        }

        if (!MonsterStats.isDead && TurnManager.Instance.playerTurnend)
        {
            shareStats = 0;
            int action = Random.Range(0, 2);
            for (int i = 0; i < 3; i++)
            {
                int rand = Random.Range(0, 6);
                switch (rand)
                {
                    case 0: shareStats += 1; break;
                    case 1: shareStats += 2; break;
                    case 2: shareStats += 3; break;
                    case 3: shareStats += 4; break;
                    case 4: shareStats += 5; break;
                    case 5: shareStats += 6; break;
                }
            }
            
            if ((int)Action.Attack == action)
            {
                StartCoroutine(playerStats.Hit(attack + shareStats));
                Debug.Log("몬스터 공격.");
                yield return null;
                attack = MonsterStats.attack;
            }
            else if ((int)Action.Depense == action)
            {
                MonsterStats.Defense(defense + shareStats);
                yield return null;
                defense = MonsterStats.defense;
            }
            
        }
    }
}
