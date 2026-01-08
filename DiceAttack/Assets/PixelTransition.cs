using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PixelTransition : MonoBehaviour
{
    public static PixelTransition Instance; // 어디서든 부를 수 있게 싱글톤 설정
    public Image fadeImage;
    public int steps = 10;
    public float duration = 1.0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject); // 캔버스 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 씬 이동을 포함한 전체 전환 실행 함수
    public void transitionToScene(string sceneName)
    {
        StartCoroutine(FadeSequence(sceneName));
    }

    IEnumerator FadeSequence(string sceneName)
    {
        float interval = duration / steps;

        // 1. 화면 채우기 (Fade In)
        for (int i = 0; i <= steps; i++)
        {
            fadeImage.fillAmount = (float)i / steps;
            yield return new WaitForSeconds(interval);
        }

        // 2. 씬 이동
        SceneManager.LoadScene(sceneName);

        // 3. 다시 화면 비우기 (Fade Out)
        for (int i = steps; i >= 0; i--)
        {
            fadeImage.fillAmount = (float)i / steps;
            yield return new WaitForSeconds(interval);
        }

        // ★ [추가] 전환 효과가 완전히 끝났으므로 UI 전체를 끕니다.
        // transform.root는 최상위 부모(보통 Canvas)를 의미합니다.
        this.transform.root.gameObject.SetActive(false);
    }
}