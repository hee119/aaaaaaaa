using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private bool isMapOpen = false;

    void Update()
    {
        // M키를 누르면 맵을 켜고 끔
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!isMapOpen) OpenMap();
            else CloseMap();
        }
    }

    public void OpenMap()
    {
        isMapOpen = true;
        // 핵심: Additive 모드로 로드 (전투 씬 유지)
        SceneManager.LoadScene("MapScene", LoadSceneMode.Additive);

        // 전투 일시정지 (선택 사항)
        Time.timeScale = 0f;
    }

    public void CloseMap()
    {
        isMapOpen = false;
        // 맵 씬만 제거
        SceneManager.UnloadSceneAsync("MapScene");

        // 전투 재개
        Time.timeScale = 1f;
    }
}