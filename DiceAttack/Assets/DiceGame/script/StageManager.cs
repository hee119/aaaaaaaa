using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearImage;
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
        player.transform.position = GameObject.FindWithTag("MapPlayer").transform.position;
    }

    void UpdateMap()
    {
        for (int i = 0; i < clearImage.Length; i++)
        {
            if(GameManager.Instance.winCount[i])
                clearImage[i].SetActive(true);
        }
    }
}