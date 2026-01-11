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
        if (GambleGame.Instance != null && GambleGame.Instance.win)
        {
            if (gameObject.CompareTag("AttackDice"))
            {
                gameObject.tag = "DefenceDice";
                // 1. 색상 값을 변경하고
                _renderer.color = Color.blue;
            }
            else if (gameObject.CompareTag("GambleDice"))
            {
                gameObject.tag = "AttackDice";
                // 2. 즉시 적용합니다.
                _renderer.color = Color.red;
            }
            else if (gameObject.CompareTag("DefenceDice"))
            {
                gameObject.tag = "GambleDice";
                // 2. 즉시 적용합니다.
                _renderer.color = Color.white;
            }
        }
        else if (NewMonoBehaviourScript.Instance != null && NewMonoBehaviourScript.Instance.win)
        {
            if (gameObject.CompareTag("AttackDice"))
            {
                gameObject.tag = "DefenceDice";
                // 1. 색상 값을 변경하고
                _renderer.color = Color.blue;
            }
            else if (gameObject.CompareTag("DefenceDice"))
            {
                gameObject.tag = "GambleDice";
                // 2. 즉시 적용합니다.
                _renderer.color = Color.white;
            }
            else if (gameObject.CompareTag("GambleDice"))
            {
                gameObject.tag = "MadicDice";
                // 2. 즉시 적용합니다.
                _renderer.color = Color.green;
            }
            else if (gameObject.CompareTag("MadicDice"))
            {
                gameObject.tag = "AttackDice";
                // 2. 즉시 적용합니다.
                _renderer.color = Color.red;
            }
        }
        else
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
    
}