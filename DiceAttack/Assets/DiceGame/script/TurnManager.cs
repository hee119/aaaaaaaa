using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    public List<GameObject> monsters = new List<GameObject>();
    public PlayerLogic player;
    public StatManager playerStat; 
    public bool playerTurnend;
    public Image turnImage;
    public GameObject winObj;
    public GameObject loseObj;
    private void Awake()
    {
        // 이미 인스턴스가 존재하면 자기 자신 제거
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // 인스턴스로 등록
        Instance = this;
        // 씬 전환에도 파괴되지 않게 함
    }

    void Start()
    {
        Invoke("StartTurn", 2f);
    }


    void StartTurn()
    {
        StartCoroutine(TurnRoutine());
    }
    IEnumerator TurnRoutine()
    {
        while (true)
        {
            if(playerStat.isDead)
                break;
            if(monsters.Count < 1)
                break;
            Debug.Log(monsters.Count);
            yield return new WaitForSeconds(1f);
            turnImage.sprite = player.GetComponent<SpriteRenderer>().sprite;
            yield return player.PlayerTurnStart();
            yield return new WaitForSeconds(3f);
            for (int i = 0; i < monsters.Count; i++)
            {
                if(monsters[i] == null || !monsters[i].activeSelf)
                continue;
                
                turnImage.sprite = monsters[i].GetComponent<SpriteRenderer>().sprite;
                Debug.Log(monsters.Count);
                yield return monsters[i].GetComponent<EnemyLogic>().MonsterTurnStart();
                yield return new WaitForSeconds(1f);
            }
            playerTurnend = false;
        }
        Debug.Log(monsters.Count);
        if (monsters.Count == 0)
        {
            GameManager.Instance.winCount[SceneManager.GetActiveScene().buildIndex] = true;
            Debug.Log(GameManager.Instance.winCount[SceneManager.GetActiveScene().buildIndex]);
            Debug.Log(SceneManager.GetActiveScene().buildIndex);
            winObj.SetActive(true);
        }
        else
        {
            Debug.Log("you Lose");
            loseObj.SetActive(true);
        }
    }
}
