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