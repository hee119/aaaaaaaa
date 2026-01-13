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
    private bool isOne;
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
        isOne = false;
        Turn();
    }

    public void Turn()
    {
       
            for (int i = 0; i < turnImageUI.Length; i++)
            {
                if (i < turnImage.Count)
                turnImageUI[i].sprite = turnImage[i].GetComponent<SpriteRenderer>().sprite;
                else if (1 == TurnManager.Instance.monsters.Count)
                {
                    if (!isOne)
                    {
                        isOne = true;
                        turnImage.Add(playerImage);
                        turnImage.Add(TurnManager.Instance.monsters[0]);
                    }
                    turnImageUI[i].sprite = turnImage[i].GetComponent<SpriteRenderer>().sprite;
                }
            }

    }

    public void TurnUpdate()
    {
        GameObject a = turnImage[0];
        turnImage.RemoveAt(0);
        turnImage.Add(a);
    }

}
