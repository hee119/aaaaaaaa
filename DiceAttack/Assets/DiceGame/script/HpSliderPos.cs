using UnityEngine;
using UnityEngine.UI;
using TMPro; // 텍스트 제어를 위해 추가

public class HpsliSlider : MonoBehaviour
{
    private Slider slider;

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

        if (hpText != null)
        {
            hpText.text = $"{(int)hp} / {(int)maxHp}";
        }
    }
}