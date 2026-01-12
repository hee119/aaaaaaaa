using UnityEngine;

public class PositionTracker : MonoBehaviour
{
    public static PositionTracker instance;
    public Vector3 lastPos; // 마지막 위치 저장용

    void Awake()
    {
        // 싱글톤: 게임 전체에서 이 메모장은 딱 하나만 존재하게 함
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬이 바뀌어도 파괴하지 마!
        }
        else { Destroy(gameObject); }
    }

    // 매 프레임마다 플레이어의 위치를 메모장에 적습니다.
    public void UpdatePosition(Vector3 currentPos)
    {
        // Z값은 아까 말한대로 슬라임보다 앞인 -1f 정도로 강제 고정해서 저장합시다.
        lastPos = new Vector3(currentPos.x, currentPos.y, -1f);
    }
}