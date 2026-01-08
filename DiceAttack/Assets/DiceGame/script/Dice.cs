using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    private Image _renderer;

    void Awake()
    {
        _renderer = GetComponent<Image>();
        _renderer.color = Color.red; 
    }

    public void Touch()
    {
        if (gameObject.CompareTag("AttackDice"))
        {
            gameObject.tag = "DefenceDice";
            // 1. 색상 값을 변경하고
            _renderer.color = Color.blue; 
        }
        else if (gameObject.CompareTag("DefenceDice"))
        {
            gameObject.tag = "AttackDice";
            // 2. 즉시 적용합니다.
            _renderer.color = Color.red;
        }
    }
    
}