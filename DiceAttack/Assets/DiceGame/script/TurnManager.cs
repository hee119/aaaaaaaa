using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    public List<GameObject> monsters = new List<GameObject>();
    public PlayerLogic player;
    public StatManager playerStat; 
    public bool playerTurnend;
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
        DontDestroyOnLoad(gameObject);
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
        while (monsters.Count != 0)
        {
            if(playerStat.isDead)
                break;
            Debug.Log(monsters.Count);
            for (int i = 0; i < monsters.Count; i++)
            {
                yield return player.PlayerTurnStart();
            }
            playerTurnend = true;
            yield return new WaitForSeconds(4f);
            for (int i = 0; i < monsters.Count; i++)
            {
                Debug.Log(monsters.Count);
                yield return monsters[i].GetComponent<EnemyLogic>().MonsterTurnStart();
                yield return new WaitForSeconds(1f);
            }
            playerTurnend = false;
        }
    }
}
