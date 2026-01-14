using UnityEngine;
using UnityEngine.UI; // ��ư ��� ���� �߰�
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearImage;
    public GameObject player;
    public Transform[] stage;

    // --- �߰��� �κ� ---
    public Button[] stageButtons; // �ν����Ϳ��� �� �������� ��ư���� �Ҵ��ϼ���.

    [System.Serializable]
    public struct StageConnection
    {
        public int[] reachableStages; // �� ������������ �� �� �ִ� ���� �������� ��ȣ��
    }
    public StageConnection[] connections; // �� ���������� ���� ����
    // ------------------

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            UpdateMap();
            player = GameObject.FindGameObjectWithTag("MapPlayer");
            player.transform.position = stage[GameManager.Instance.nowScene].position;
        }
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

            // ���� ��ġ���� �� �� �ִ� ������ Ȯ��
            foreach (int reachableId in connections[currentPos].reachableStages)
            {
                if (i == reachableId) { isReachable = true; break; }
            }

            // --- ���⼭���� �ٽ� ���� ---

            if (isCleared || isCurrent)
            {
                // 1. �̹� ���ų� ���� �ִ� ĭ: "�Ͼ�� ���̵� Ŭ���� �� ��"
                stageButtons[i].interactable = true;         // �Ͼ�� ���� (Normal Color)
                stageButtons[i].image.raycastTarget = false; // ���콺 Ŭ�� ����

                if (isCleared) clearImage[i].SetActive(true);
            }
            else if (isReachable)
            {
                // 2. �� �� �ִ� ���� ĭ: "�Ͼ�� Ŭ���� ��"
                stageButtons[i].interactable = true;         // �Ͼ��
                stageButtons[i].image.raycastTarget = true;  // Ŭ�� ����
                clearImage[i].SetActive(false);
            }
            else
            {
                // 3. ���� �� ���� �� ĭ: "ȸ���̰� Ŭ���� �� ��"
                stageButtons[i].interactable = false;        // ȸ�� ó�� (Disabled Color)
                stageButtons[i].image.raycastTarget = false; // Ŭ�� �Ұ�
                clearImage[i].SetActive(false);
            }
        }
    }
}