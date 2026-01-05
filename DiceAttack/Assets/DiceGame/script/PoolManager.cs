using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [Header("풀로 관리할 오브젝트")]
    public GameObject objectPrefab; // 풀로 사용할 프리팹
    public int poolSize = 100;        // 최대 풀 개수

    private List<GameObject> pool = new List<GameObject>();

    private void Awake()
    {
        // 싱글톤 등록
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // 풀 초기화
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectPrefab);
            obj.SetActive(false); // 처음엔 비활성화
            pool.Add(obj);
        }
    }

    // 사용 가능한 오브젝트 가져오기
    public GameObject GetObject()
    {
        foreach (var obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }

        // 모두 사용 중이면 새로 생성 (선택사항)
        GameObject newObj = Instantiate(objectPrefab);
        pool.Add(newObj);
        return newObj;
    }

    // 오브젝트 되돌리기
    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
    }
}