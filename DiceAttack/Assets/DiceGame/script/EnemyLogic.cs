using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    GameObject playerObj;
    private StatManager playerStats;
    private PlayerLogic playerLogic;
    private int attack;
    private StatManager MonsterStats;

    void Awake()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj == null)
        {
            Debug.LogError("플레이어가 없습니다.");
            return;
        }
        playerStats = playerObj.GetComponent<StatManager>();
        playerLogic = playerObj.GetComponent<PlayerLogic>();
        MonsterStats = GetComponent<StatManager>();
        attack = MonsterStats.attack;
    }
    void OnEnable()
    {
        TurnManager.Instance.monsters.Add(gameObject);
    }

    public IEnumerator MonsterTurnStart()
    {
        if (MonsterStats.isDead)
            TurnManager.Instance.monsters.Remove(gameObject);

        if (!MonsterStats.isDead && playerLogic.turnend || !playerStats.isDead && playerLogic.turnend)
        {
            for (int i = 0; i < 3; i++)
            {
                int rand = Random.Range(0, 6);
                switch (rand)
                {
                    case 0: attack += 1; break;
                    case 1: attack += 2; break;
                    case 2: attack += 3; break;
                    case 3: attack += 4; break;
                    case 4: attack += 5; break;
                    case 5: attack += 6; break;
                }
            }

            playerStats.Hit(attack);
            Debug.Log("플레이어 때찌.");
            yield return null;
            attack = MonsterStats.attack;
        }
    }
}
