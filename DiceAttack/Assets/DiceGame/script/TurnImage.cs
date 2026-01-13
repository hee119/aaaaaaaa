using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnImage : MonoBehaviour
{
    public static TurnImage Instance { get; private set; }
    public List<GameObject> turnImage = new List<GameObject>();
    public GameObject playerImage;
    public Image[] turnImageUI = new Image[3];
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
    }
    private IEnumerator Start()
    {
        yield return new WaitUntil(() => TurnManager.Instance.monsters.Count > 0);
        for (int i = 0; i < TurnManager.Instance.monsters.Count; i++)
        {
            turnImage.Add(playerImage);
        }
        for (int i = 0; i < TurnManager.Instance.monsters.Count; i++)
        {
            turnImage.Add(TurnManager.Instance.monsters[i]);
        }

        Turn();
    }

    void Turn()
    {
       
            GameObject[] preview = turnImage.ToArray();
            for (int i = 0; i < turnImageUI.Length; i++)
            {
                if (i < preview.Length)
                turnImageUI[i].sprite = preview[i].GetComponent<SpriteRenderer>().sprite;
                else if (i >= preview.Length)
                    turnImageUI[i].enabled = false;  

            }

    }

    public void TurnUpdate()
    {
        GameObject a = turnImage[0];
        turnImage.RemoveAt(0);
        turnImage.Add(a);
        Turn();
    }

}
