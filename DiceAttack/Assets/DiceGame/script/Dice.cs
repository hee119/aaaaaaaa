using UnityEngine;

public class Dice : MonoBehaviour
{
    private SpriteRenderer _renderer;

    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.material.color = Color.red; 
    }

    public void Touch()
    {
        if (gameObject.CompareTag("AttackDice"))
        {
            gameObject.tag = "DefenceDice";
            // 1. 색상 값을 변경하고
            _renderer.material.color = Color.blue; 
        }
        else if (gameObject.CompareTag("DefenceDice"))
        {
            gameObject.tag = "AttackDice";
            // 2. 즉시 적용합니다.
            _renderer.material.color = Color.red;
        }
    }
}