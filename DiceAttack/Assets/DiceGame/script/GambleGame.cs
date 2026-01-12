using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GambleGame : MonoBehaviour
{
    public static GambleGame Instance { get; private set; }
    public bool win = false;
    public bool lose = false;
    public int wins = 0;
    public int loses = 0;
    public GameObject winImage;
    public GameObject loseImage;
    public GameObject diceRerollImage;
    public GameObject even;
    public GameObject odd;
    public Image dice;
    public Sprite[] dices;
    public HpAndTrophy hpAndTrophy;
    public GameObject explanation;

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
        DontDestroyOnLoad(gameObject);
    }
    public void Even()
    {
        even.SetActive(false);
        odd.SetActive(false);
        int rand = Random.Range(1, 7);
        switch (rand)
        {
            case 1:
                loses++;
                break;
            case 2:
                wins++;
                break;
            case 3:
                loses++;
                break;
            case 4:
                wins++;
                break;
            case 5:
                loses++;
                break;
            case 6:
                wins++;
                break;
        }
        StartCoroutine(a(rand));
    }
    public void Odd()
    {
        even.SetActive(false);
        odd.SetActive(false);
        int rand = Random.Range(1, 7);
        switch (rand)
        {
            case 1:
                wins++;
                break;
            case 2:
                loses++;
                break;
            case 3:
                wins++;
                break;
            case 4:
                loses++;
                break;
            case 5:
                wins++;
                break;
            case 6:
                loses++;
                break;
        }

        StartCoroutine(a(rand));
        
    }

    IEnumerator a(int rand)
    {
        diceRerollImage.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        diceRerollImage.SetActive(false);
        dice.sprite = dices[rand - 1];
        dice.enabled = true;
        yield return new WaitForSeconds(0.4f);
        dice.enabled = false;
        even.SetActive(true);
        odd.SetActive(true);
        hpAndTrophy.Hp();
        hpAndTrophy.Trophy();
        if (loses == 3)
        {
            loseImage.SetActive(true);
            lose = true;
        }
        else if (wins == 3)
        {
            GameManager.Instance.winCount++;
            winImage.SetActive(true);
            win = true;
        }
    }

    public void Explanation()
    {
        explanation.SetActive(true);
    }
}
