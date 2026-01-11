using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearText;
    public GameObject[] playerTrans;
    public GameObject player;
    public int stage;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stage = GameManager.Instance.nowScene;
    }

    void Start()
    {
        UpdateMap();
        player.transform.position = playerTrans[stage].transform.position;
    }

    void UpdateMap()
    {
        int count = GameManager.Instance.winCount;

        for (int i = 0; i < clearText.Length; i++)
        {
            clearText[i].SetActive(i < count);
        }
    }
}