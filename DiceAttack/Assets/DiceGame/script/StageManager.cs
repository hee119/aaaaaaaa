using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearImage;
    public GameObject player;
    public Transform[] stage;
    public Button[] stageButtons;

    [System.Serializable]
    public struct StageConnection
    {
        public int[] reachableStages;
    }
    public StageConnection[] connections;

    // 인스펙터에서 물음표 스테이지 번호(3, 4번 등)를 넣어주세요
    public int[] eventStageIndexes;

    void Start()
    {
        UpdateMap();
        player = GameObject.FindGameObjectWithTag("MapPlayer");
        if (player != null && stage.Length > GameManager.Instance.nowScene)
            player.transform.position = stage[GameManager.Instance.nowScene].position;
    }

    public void UpdateMap()
    {
        if (SceneManager.GetActiveScene().name != "Map") return;

        int currentPos = GameManager.Instance.nowScene;

        for (int i = 0; i < stageButtons.Length; i++)
        {
            bool isCleared = GameManager.Instance.winCount[i];
            bool isCurrent = (i == currentPos);
            bool isReachable = false;

            // 현재 위치에서 갈 수 있는 곳인지 확인
            foreach (int reachableId in connections[currentPos].reachableStages)
            {
                if (i == reachableId) { isReachable = true; break; }
            }

            bool isEvent = false;
            foreach (int idx in eventStageIndexes) if (i == idx) isEvent = true;

            // --- 버그 수정 로직 ---
            stageButtons[i].interactable = true;

            if (isCurrent) // 1. 현재 내가 서 있는 곳
            {
                SetButtonVisual(i, 1.0f, false); // 밝게 표시하되 클릭은 방지
                if (isCleared) clearImage[i].SetActive(true);
            }
            else if (isReachable) // 2. 현재 위치에서 갈 수 있는 다음 칸들 (해금)
            {
                if (isEvent && isCleared)
                {
                    // 이미 깬 이벤트 스테이지는 해금 리스트에 있어도 어둡게 하고 클릭 막음
                    SetButtonVisual(i, 0.4f, false);
                    clearImage[i].SetActive(true);
                }
                else
                {
                    // 일반 스테이지나 아직 안 깬 이벤트 스테이지는 밝게 해금
                    SetButtonVisual(i, 1.0f, true);
                    clearImage[i].SetActive(isCleared);
                }
            }
            else // 3. 그 외 (이미 지나온 길이나 아직 못 가는 먼 길)
            {
                float alpha = (isCleared && !isEvent) ? 1.0f : 0.4f; // 일반 클리어 지역은 밝게, 나머지는 어둡게
                SetButtonVisual(i, alpha, false);
                clearImage[i].SetActive(isCleared);
            }
        }
    }

    // 시각적 설정 (밝기 및 클릭 감지)
    void SetButtonVisual(int index, float brightness, bool canClick)
    {
        // 1. 색상을 직접 변경 (Disabled Color를 거치지 않음)
        stageButtons[index].image.color = new Color(brightness, brightness, brightness, 1.0f);

        // 2. 마우스 클릭 감지 여부만 껐다 켬
        stageButtons[index].image.raycastTarget = canClick;
    }
}