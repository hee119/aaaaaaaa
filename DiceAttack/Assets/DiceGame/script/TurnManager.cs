using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Instance { get; private set; }
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
    
    public List<MonsterLogic> monsters;
    public PlayerLogic player;

    IEnumerator TurnRoutine()
    {
        StartCoroutine(player.PlayerTurnStart());
        foreach (MonsterLogic monster in monsters)
        {
            StartCoroutine(monster.MonsterTurnStart());
        }
        yield return new WaitForSeconds(0.5f);
    }
}
