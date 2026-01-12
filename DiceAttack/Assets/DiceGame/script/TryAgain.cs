using UnityEngine;
using UnityEngine.SceneManagement;

public class TryAgain : MonoBehaviour
{ 
    public void Again()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // 현재 씬 다시 로드
        SceneManager.LoadScene(currentScene);
    }
}
