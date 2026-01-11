using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewMonoBehaviourScript : MonoBehaviour
{
    int diceCount = 0;
    bool win = false;
    public int loses = 0;
    public GameObject winImage;
    public GameObject loseImage;
    public HpAndTrophy hpAndTrophy;
    int rerollCount = 0;
    public GameObject diceRerollImage;
    public Sprite[] dices;
    public Image dice;
    public TMP_Text text;
    Image img;
    public GameObject c;

    void Awake()
    {
         img = GetComponent<Image>();
    }
    public void Reroll()
    {
            int rand = Random.Range(1, 7);
            StartCoroutine(a(rand));
    }

    IEnumerator a(int rand)
    {
        c.SetActive(true);
        img.enabled = false;
        diceRerollImage.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        diceRerollImage.SetActive(false);
        dice.sprite = dices[rand - 1];
        dice.enabled = true;
        yield return new WaitForSeconds(0.4f);
        dice.enabled = false;
        img.enabled = true;
        c.SetActive(false);
        diceCount += rand;
        text.text = diceCount.ToString();
        rerollCount++;
        if (rerollCount >= 3)
        {
            if (diceCount >= 14)
            {
                win = true;
                winImage.SetActive(true);
            }
            else
            {
                loses++;
                hpAndTrophy.Hhp();
            }

            if (loses >= 3)
            {
                loseImage.SetActive(true);
            }
            rerollCount = 0;
            diceCount = 0;
            text.text = diceCount.ToString();
        }
    }
}
