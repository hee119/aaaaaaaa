using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // 클릭 감지를 위해 추가

// IPointerClickHandler를 상속받으면 마우스 클릭을 감지할 수 있습니다.
public class Dice : MonoBehaviour, IPointerClickHandler
{
    private Image _renderer;

    void Awake()
    {
        _renderer = GetComponent<Image>();
        // 시작할 때 기본 색상 설정
        _renderer.color = Color.red;
    }

    // 유니티 시스템이 클릭을 감지하면 자동으로 호출합니다.
    public void OnPointerClick(PointerEventData eventData)
    {
        Touch();
    }

    public void Touch()
    {
        // 1. 인스턴스 체크 (게임 매니저들이 없으면 작동 안 함)
        if (GambleGame.Instance == null) return;

        // 2. 로직 단순화: 현재 태그에 따라 다음 상태로 전환
        string currentTag = gameObject.tag;

        if (GambleGame.Instance.win)
        {
            SwitchTagAndColor(currentTag);
        }
        else
        {
            // win이 아닐 때의 로직 (간단한 교체 예시)
            if (currentTag == "AttackDice") UpdateState("DefenceDice", Color.blue);
            else if (currentTag == "DefenceDice") UpdateState("AttackDice", Color.red);

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

    private void SwitchTagAndColor(string currentTag)
    {
        switch (currentTag)
        {
            case "AttackDice":
                UpdateState("DefenceDice", Color.blue);
                break;
            case "DefenceDice":
                UpdateState("GambleDice", Color.white);
                break;
            case "GambleDice":
                // 팀원 코드에 있던 MadicDice(MagicDice 오타인듯?) 로직 반영
                UpdateState("MadicDice", Color.green);
                break;
            case "MadicDice":
                UpdateState("AttackDice", Color.red);
                break;
        }
    }

    private void UpdateState(string newTag, Color newColor)
    {
        gameObject.tag = newTag;
        _renderer.color = newColor;
        Debug.Log($"태그 변경됨: {newTag}"); // 작동 확인용
    }
}