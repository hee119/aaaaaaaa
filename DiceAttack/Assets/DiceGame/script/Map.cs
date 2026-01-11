using UnityEngine;
using UnityEngine.SceneManagement;

public class Map : MonoBehaviour
{
    // [배틀 씬에 있는 Map 버튼용]
    public void OpenMap()
    {
        // 1. 이미 맵이 열려있으면 중복 로딩 안 함
        if (SceneManager.GetSceneByName("Map").isLoaded) return;

        // 2. Additive로 불러와야 전투 씬이 뒤에 남아있습니다!
        SceneManager.LoadScene("Map", LoadSceneMode.Additive);

        // 3. 전투 멈춤
        Time.timeScale = 0f;
    }

    // [맵 씬에 있는 돌아가기 버튼용] - 새로 추가
    public void CloseMap()
    {
        SceneManager.UnloadSceneAsync("Map");
        Time.timeScale = 1f; // 전투 재개
    }
}