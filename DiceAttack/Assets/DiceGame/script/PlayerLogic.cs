using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    List<GameObject> monster;
    StatManager player;
    private int attack;
    private int firstAttack;
    private int count;
    void Awake()
    {
        count = TurnManager.Instance.monsters.Count;
        firstAttack = GetComponent<StatManager>().attack;
        attack = firstAttack;
        player = gameObject.GetComponent<StatManager>();
        monster = TurnManager.Instance.monsters;
    }
    
    public IEnumerator PlayerTurnStart()
    {
        count = TurnManager.Instance.monsters.Count;

        if (player.isDead)
            StopAllCoroutines();
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < 3; j++)
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

            if (!monster[i].GetComponent<StatManager>().isDead)
            {
                monster[i].GetComponent<StatManager>().Attacked(attack);
                Debug.Log("몬스터 때찌.");
            }

            yield return new WaitForSeconds(1f);
            attack = firstAttack;
            count = TurnManager.Instance.monsters.Count;
        }
    }
}
