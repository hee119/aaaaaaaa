using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리를 위한 네임스페이스

public class BattleResult : MonoBehaviour
{
    public void GoToMap()
    {
        // "Map"이라는 이름의 씬으로 이동
        SceneManager.LoadScene("Map");
    }
}