using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    // 맵으로 이동할 때 사용했던 함수
    public void LoadMapScene()
    {
        ExecuteTransition("Map");
    }

    // ★ [추가] 전투 씬으로 다시 돌아갈 때 사용하는 함수
    public void LoadBattleScene()
    {
        ExecuteTransition("Fight"); // 실제 전투 씬 이름으로 적어주세요
    }

    // 중복 코드를 줄이기 위한 공통 실행 함수
    private void ExecuteTransition(string sceneName)
    {
        if (PixelTransition.Instance != null)
        {
            // 꺼져있을 수 있는 UI를 다시 켜고 전환 시작
            PixelTransition.Instance.transform.root.gameObject.SetActive(true);
            PixelTransition.Instance.transitionToScene(sceneName);
        }
        else
        {
            Debug.LogError("PixelTransition 인스턴스를 찾을 수 없습니다!");
        }
    }
}