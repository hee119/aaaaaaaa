using TMPro;
using UnityEngine;

public class RandomStatGive : MonoBehaviour
{
    public TextMeshProUGUI explane;
    private StatType statType;
    private int stat = 0;

    enum StatType
    {
        Hp,
        Ak,
        Df
    }

    void Start()
    {
        int rand = Random.Range(0, 3);
        statType = (StatType)rand;

        stat = Random.Range(1, 10);

        Explane(stat);
    }

    void Explane(int stat)
    {
        switch (statType)
        {
            case StatType.Hp: 
                explane.text = $"HP {stat} high";
                break;
            case StatType.Ak:
                explane.text = $"Attack {stat} high";
                break;
            case StatType.Df:
                explane.text = $"Defense {stat} high";
                break;
        }
    }

    public void click()
    {
        switch (statType)
        {
            case StatType.Hp:
                GameManager.Instance.Hp += stat;
                break;
            case StatType.Ak:
                GameManager.Instance.Ak += stat;
                break;
            case StatType.Df:
                GameManager.Instance.Df += stat;
                break;
        }

    }
}