using TMPro;
using UnityEngine;

public class RandomStatGive : MonoBehaviour
{
    public TextMeshPro typeText;
    public TextMeshPro statText;
    enum StatType
    {
        Hp,
        Ak,
        Df
    }

    void Awake()
    {
        
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int rand = Random.Range(0, 3);
        StatType statType = (StatType)rand;
        typeText.text = statType.ToString();
        int stat = Random.Range(1, 10);
        statText.text = stat.ToString();
    }
}
