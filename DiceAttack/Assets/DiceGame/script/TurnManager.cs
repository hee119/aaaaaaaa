using System.Collections;
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
    
    int playerTurn = 0;
    int MonsterTurn = 0;

    IEnumerator PlayerTurn()
    {
        if (playerTurn == 0)
        {
            playerTurn = 1;
        }
        yield return new WaitForSeconds(0.5f);
    }
}
