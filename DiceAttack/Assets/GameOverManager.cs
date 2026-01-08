using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // 텍스트 매시 프로를 쓰신다면 추가

public class GameOverManager : MonoBehaviour
{
    public Image background;      // 검은색 배경 이미지
    public Text TextMeshProUGUI;    // 'GAME OVER' 텍스트 (TextMeshPro라면 TextMeshProUGUI로 변경)
    public float fadeDuration = 2.0f; // 어두워지는 총 시간

    void Start()
    {
        // 처음엔 보이지 않게 초기화
        background.color = new Color(0, 0, 0, 0);
        TextMeshProUGUI.color = new Color(TextMeshProUGUI.color.r, TextMeshProUGUI.color.g, TextMeshProUGUI.color.b, 0);
    }

    // 테스트용: G 키를 누르면 게임오버 실행
    void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.gKey.wasPressedThisFrame)
        {
            StartGameOver();
        }
    }

    public void StartGameOver()
    {
        StartCoroutine(GameOverSequence());
    }

    IEnumerator GameOverSequence()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            // 1. 배경을 점점 검게 (최대 0.8 정도까지만 하면 뒤가 살짝 보여서 멋있습니다)
            background.color = new Color(0, 0, 0, alpha * 0.8f);

            // 2. 글자를 점점 선명하게
            TextMeshProUGUI.color = new Color(TextMeshProUGUI.color.r, TextMeshProUGUI.color.g, TextMeshProUGUI.color.b, alpha);

            yield return null;
        }
    }
}