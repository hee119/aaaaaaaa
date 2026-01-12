using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearText;
    public GameObject[] playerTrans;
    public GameObject player;
    public int stage;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("MapPlayer");
    }

    void Start()
    {
        UpdateMap();
        stage = GameManager.Instance.nowScene;
        player.transform.position = GameObject.FindWithTag("생성된위치태그").transform.position;
    }

    void UpdateMap()
    {
        for (int i = 0; i < clearText.Length; i++)
        {
            if(GameManager.Instance.winCount[i])
            clearText[i].SetActive(true);
        }
    }
}