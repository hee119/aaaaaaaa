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
            Debug.Log("<color=green>�Ŵ��� �ϼ�����</color>");
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
            Debug.Log("<color=cyan>������ ��ġ: </color>" + savedPosition);
        }
        else if (player == null)
        {
            Debug.LogError("<color=red>�������� �� ¦ �� ¦.... �� ��𰬾�</color>");
        }
    }

    public void SaveCurrentPosition()
    {
        GameObject player = GameObject.FindWithTag("MapPlayer");
        if (player != null)
        {
            savedPosition = player.transform.position;
            hasSavedPos = true;
            Debug.Log("<color=yellow>��ġ ��� �Ϸ�!</color>");
        }
    }
}