using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // 코루틴 사용을 위해 추가

public class HpsliSlider : MonoBehaviour
{
    private Slider slider;
    public TextMeshProUGUI hpText;

    [Header("애니메이션 설정")]
    public float fillSpeed = 0.5f; // 바가 차오르는/줄어드는 속도
    private Coroutine currentLerpCoroutine;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    // 체력을 업데이트할 때 호출하는 함수
    public void HpBar(float hp, float maxHp)
    {
        float targetValue = hp / maxHp;

        // 1. 텍스트는 즉시 업데이트
        if (hpText != null)
        {
            hpText.text = $"{(int)hp} / {(int)maxHp}";
        }

        // 2. 슬라이더 바 부드럽게 이동 (기존 실행 중인 애니메이션이 있다면 멈춤)
        if (currentLerpCoroutine != null)
        {
            StopCoroutine(currentLerpCoroutine);
        }
        currentLerpCoroutine = StartCoroutine(SmoothUpdateSlider(targetValue));
    }

    private IEnumerator SmoothUpdateSlider(float target)
    {
        float startValue = slider.value;
        float elapsed = 0f;

        // fillSpeed 시간 동안 부드럽게 이동
        while (elapsed < fillSpeed)
        {
            elapsed += Time.deltaTime;
            // Lerp를 사용하여 부드럽게 보간
            slider.value = Mathf.Lerp(startValue, target, elapsed / fillSpeed);
            yield return null;
        }

        slider.value = target;
    }

    // 오브젝트가 풀로 돌아갈 때(OnDisable) 애니메이션 멈춤 처리
    private void OnDisable()
    {
        if (currentLerpCoroutine != null)
        {
            StopCoroutine(currentLerpCoroutine);
        }
    }
}