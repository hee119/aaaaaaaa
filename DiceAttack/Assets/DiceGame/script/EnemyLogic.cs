using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    GameObject playerObj;
    private GameObject fightIcon;
    GameObject defenseIcon;
    private StatManager playerStats;
    private int attack;
    private int defense;
    private int shareStats;
    private StatManager MonsterStats;

    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("플레이어가 없습니다.");
            return;
        }
        fightIcon = transform.Find("fightIcon").gameObject;
        defenseIcon = transform.Find("defenseIcon").gameObject;
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
        if (MonsterStats.isDead)
        {
            TurnManager.Instance.monsters.Remove(gameObject);
            yield break;
        }

        if (!MonsterStats.isDead && TurnManager.Instance.playerTurnend)
        {
            shareStats = 0;
            int action = Random.Range(0, 10);
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
            
            if (action >= 3)
            {
                fightIcon.SetActive(true);
                attack += shareStats;
                Debug.Log(attack);
                StartCoroutine(playerStats.Hit(attack));
                Debug.Log("몬스터 공격.");
                yield return null;
                attack = MonsterStats.attack;
                yield return new WaitForSeconds(1.5f);
                fightIcon.SetActive(false);
            }
            else
            {
                defenseIcon.SetActive(true);
                MonsterStats.Defense(defense + shareStats);
                yield return null;
                defense = MonsterStats.defense;
            }
            
        }
    }
}
