using UnityEngine;
using UnityEngine.UI; // 버튼 제어를 위해 추가
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearImage;
    public GameObject player;
    public Transform[] stage;

    // --- 추가된 부분 ---
    public Button[] stageButtons; // 인스펙터에서 각 스테이지 버튼들을 할당하세요.

    [System.Serializable]
    public struct StageConnection
    {
        public int[] reachableStages; // 각 스테이지에서 갈 수 있는 다음 스테이지 번호들
    }
    public StageConnection[] connections; // 각 스테이지별 연결 정보
    // ------------------

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        UpdateMap();
        player = GameObject.FindGameObjectWithTag("MapPlayer");
        player.transform.position = stage[GameManager.Instance.nowScene].position;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
    }

    void UpdateMap()
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

            // --- 여기서부터 핵심 로직 ---

            if (isCleared || isCurrent)
            {
                // 1. 이미 깼거나 현재 있는 칸: "하얗게 보이되 클릭은 안 됨"
                stageButtons[i].interactable = true;         // 하얀색 유지 (Normal Color)
                stageButtons[i].image.raycastTarget = false; // 마우스 클릭 무시

                if (isCleared) clearImage[i].SetActive(true);
            }
            else if (isReachable)
            {
                // 2. 갈 수 있는 다음 칸: "하얗고 클릭도 됨"
                stageButtons[i].interactable = true;         // 하얀색
                stageButtons[i].image.raycastTarget = true;  // 클릭 가능
                clearImage[i].SetActive(false);
            }
            else
            {
                // 3. 아직 못 가는 먼 칸: "회색이고 클릭도 안 됨"
                stageButtons[i].interactable = false;        // 회색 처리 (Disabled Color)
                stageButtons[i].image.raycastTarget = false; // 클릭 불가
                clearImage[i].SetActive(false);
            }
        }
    }
}