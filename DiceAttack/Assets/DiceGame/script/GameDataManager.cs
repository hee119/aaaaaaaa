using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    public static GameDataManager instance;
    public Vector3 savedPosition; // 여기가 플레이어 위치 저장하는 칸!

    void Awake()
    {
        // 메모장이 하나만 있게 확인하는 작업이에요
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 바뀌어도 이 메모장은 버리지 마!
        }
        else
        {
            Destroy(gameObject);
        }
    }
}