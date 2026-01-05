using UnityEngine;

public class HpScript : MonoBehaviour
{
    private int hp = StatManager.Instance.hp;
    public int attack;
    bool isAttacked = false;
    bool isDead = false;

    // Update is called once per frame
    void Update()
    {
        if (isAttacked)
        {
            Attacked(attack);
        }

        if (hp <= 0)
        {
            isDead = true;
        }
    }

    void Attacked(int attack)
    {
        hp -= attack;
    }
}
