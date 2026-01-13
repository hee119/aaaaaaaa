using UnityEngine;
using UnityEngine.UI;
using TMPro; // 텍스트 제어를 위해 추가

public class HpsliSlider : MonoBehaviour
{
    private Slider slider;

    // 인스펙터 창에서 생성한 HpText를 여기에 드래그해서 넣어주세요.
    public TextMeshProUGUI hpText;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // 체력을 업데이트할 때 호출하는 함수
    public void HpBar(float hp, float maxHp)
    {
        // 1. 슬라이더 바 위치 조절
        slider.value = hp / maxHp;

        // 2. 텍스트 업데이트 (예: "90 / 100")
        // 소수점이 보기 싫다면 (int)로 형변환을 해줍니다.
        if (hpText != null)
        {
            hpText.text = $"{(int)hp} / {(int)maxHp}";
        }
    }
}