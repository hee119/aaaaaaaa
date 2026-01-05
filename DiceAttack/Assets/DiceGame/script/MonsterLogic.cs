using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterLogic : MonoBehaviour
{
    StatManager player;
    private int attack;
    private int firstAttack;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<StatManager>();
        if (player == null)
        {
            Debug.LogError("플레이어가 없습니다.");
        }

        firstAttack = GetComponent<StatManager>().attack;
        attack = firstAttack;
    }
    void OnEnable()
    {
        TurnManager.Instance.monsters.Add(this);
    }

    void OnDisable()
    {
        TurnManager.Instance.monsters.Remove(this);
    }

    public IEnumerator MonsterTurnStart()
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
        player.Attacked(attack);
        Debug.Log("플레이어 때찌.");
        yield return null;
        attack = firstAttack;
    }
}
