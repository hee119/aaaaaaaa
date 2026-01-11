using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    private Vector3 savedPosition;
    private bool hasSavedPos = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
            Debug.Log("<color=green>매니저 완성ㅇㅇ</color>");
        }
        else { Destroy(gameObject); }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("FixPosition", 0.2f);
    }

    void FixPosition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null && hasSavedPos)
        {
            player.transform.position = new Vector3(savedPosition.x, savedPosition.y, -1f);
            Debug.Log("<color=cyan>개구리 위치: </color>" + savedPosition);
        }
        else if (player == null)
        {
            Debug.LogError("<color=red>개구리는 폴 짝 폴 짝.... 또 어디갔어</color>");
        }
    }

    public void SaveCurrentPosition()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            savedPosition = player.transform.position;
            hasSavedPos = true;
            Debug.Log("<color=yellow>위치 기억 완료!</color>");
        }
    }
}