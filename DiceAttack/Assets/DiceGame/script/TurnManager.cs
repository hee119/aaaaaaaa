using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    public List<GameObject> monsters = new List<GameObject>();
    public PlayerLogic player;
    public bool turnend;
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
        StartCoroutine (TurnRoutine());
    }

    
    IEnumerator TurnRoutine()
    {
        while (monsters.Count != 0)
        {
            yield return player.PlayerTurnStart();
            for (int i = 0; i < monsters.Count; i++)
            {
                yield return monsters[i].GetComponent<EnemyLogic>().MonsterTurnStart();
                yield return new WaitForSeconds(1f);
            }
        }
    }
}
