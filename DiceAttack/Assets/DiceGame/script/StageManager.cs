using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject[] clearText;

    void Start()
    {
        UpdateMap();
    }

    void UpdateMap()
    {
        int count = GameManager.Instance.winCount;

        for (int i = 0; i < clearText.Length; i++)
        {
            clearText[i].SetActive(i < count);
        }
    }
}