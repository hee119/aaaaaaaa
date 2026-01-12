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
        // 현재 어떤 게임 모드(인스턴스)가 활성화되어 있는지 먼저 확인합니다.
        bool isGambleWin = GambleGame.Instance != null && GambleGame.Instance.win;
        bool isMonoWin = NewMonoBehaviourScript.Instance != null && NewMonoBehaviourScript.Instance.win;

        // 1. 두 모드 모두 승리했을 때 (가장 구체적인 조건이 맨 위로 와야 합니다)
        if (isGambleWin && isMonoWin)
        {
            CycleTags(new string[] { "AttackDice", "DefenceDice", "GambleDice", "MadicDice" },
                new Color[] { Color.red, Color.blue, Color.white, Color.green });
        }
        // 2. GambleGame만 승리했을 때
        else if (isGambleWin)
        {
            CycleTags(new string[] { "AttackDice", "DefenceDice", "GambleDice" },
                new Color[] { Color.red, Color.blue, Color.white });
        }
        // 3. NewMonoBehaviour만 승리했을 때
        else if (isMonoWin)
        {
            CycleTags(new string[] { "AttackDice", "DefenceDice", "MadicDice" },
                new Color[] { Color.red, Color.blue, Color.green });
        }
        // 4. 둘 다 아닐 때 (기본 상태)
        else
        {
            CycleTags(new string[] { "AttackDice", "DefenceDice" },
                new Color[] { Color.red, Color.blue });
        }
    }

// 태그 순환 로직을 별도 메서드로 분리하여 중복을 제거합니다.
    private void CycleTags(string[] tags, Color[] colors)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (gameObject.CompareTag(tags[i]))
            {
                // 다음 인덱스로 이동 (마지막이면 0으로 돌아감)
                int nextIndex = (i + 1) % tags.Length;
                gameObject.tag = tags[nextIndex];
                _renderer.color = colors[nextIndex];
                return; // 변경 후 즉시 종료 (중복 실행 방지)
            }
        }
    }
    
}