using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int winCount = 0;
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

    private void Start()
    {
        
    }

    
}
