using UnityEngine;

public class UIOnButtonClick : MonoBehaviour
{
    // 켜고 싶은 UI (SelectStat)를 여기에 드래그해서 연결
    public GameObject targetUI;

    // 이 함수를 버튼의 OnClick 이벤트에 연결하세요
    public void OpenTargetUI()
    {
        if (targetUI != null)
        {
            targetUI.SetActive(true);
            Debug.Log($"{targetUI.name} UI가 활성화되었습니다.");
        }
        else
        {
            Debug.LogError("연결된 UI 오브젝트가 없습니다!");
        }
    }
}