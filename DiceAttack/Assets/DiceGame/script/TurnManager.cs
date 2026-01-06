using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
    private List<GameObject> enemyLogic = new List<GameObject>();
    public List<GameObject> monsters = new List<GameObject>();
    public PlayerLogic player;
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
        for (int i = 0; i < monsters.Count; i++)
        {
            enemyLogic.Add(monsters[i]);
            if (enemyLogic[i] == null)
            {
                Debug.LogError("없다고");
            }
        }
        while (monsters.Count != 0)
        {
            StartCoroutine(player.PlayerTurnStart());
            for (int i = 0; i < monsters.Count; i++)
            {
                yield return enemyLogic[i].GetComponent<EnemyLogic>().MonsterTurnStart();
            }
        }
    }
}
