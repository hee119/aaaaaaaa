using System.Collections;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    StatManager monster;
    private int attack;
    private int firstAttack;
    private int count;
    void Awake()
    {
        firstAttack = GetComponent<StatManager>().attack;
        attack = firstAttack;
        count = TurnManager.Instance.monsters.Count;
    }
    public IEnumerator PlayerTurnStart()
    {
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < count; j++)
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

            monster.Attacked(attack);
            Debug.Log("몬스터 때찌.");
            yield return null;
            attack = firstAttack;
        }
    }
}
