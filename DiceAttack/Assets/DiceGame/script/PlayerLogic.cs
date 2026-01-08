using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    GameObject targetMonster;
    public GameObject[] dice = new GameObject[3];
    StatManager player;
    StatManager targetMonsterStats;
    private int attack;
    private int firstDefence;
    private int defence;
    private int firstAttack;
    private int count;
    public int rerollCount;
    public TMP_Text ATK;
    public TMP_Text DFS;
    public Sprite[] DiceSprites;
    public Image diceImage;
    public GameObject DiceObj;
    public bool isReroll;
    private Image[] DiceImages = new Image[3];
    private int rand;
    void Awake()
    {
        diceImage.enabled = false;
        player = gameObject.GetComponent<StatManager>();
        DiceImages[0] = dice[0].GetComponent<Image>();
        DiceImages[1] = dice[1].GetComponent<Image>();
        DiceImages[2] = dice[2].GetComponent<Image>();
    }

    void Start()
    {
        firstAttack = GetComponent<StatManager>().attack;
        attack = 0;
        firstDefence = GetComponent<StatManager>().defense;
        defence = 0;
    }
    void Update()
    {
        
            if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
            {
                // UI 클릭 체크
                

                // 마우스 위치 → 월드 좌표
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // 해당 위치에서 충돌 감지
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); 
                // Vector2.zero를 쓰면 "해당 점에서 감지" (Point Cast처럼 동작)

                Debug.DrawRay(mousePos, Vector2.zero, Color.red, 1.0f); // 시각화용

                if (hit.collider != null)
                {
                    Debug.Log("맞춘 물체: " + hit.collider.name);
                    if (targetMonster != null)
                    {
                        targetMonster.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (hit.collider.CompareTag("Monster"))
                    {
                        targetMonster = hit.collider.gameObject;
                        targetMonsterStats = targetMonster.GetComponent<StatManager>();
                        targetMonster.transform.GetChild(0).gameObject.SetActive(true);
                        Debug.Log("선택된 몬스터: " + targetMonster.name);
                    }
                }
        }
    }
    public IEnumerator PlayerTurnStart()
    {
        count = TurnManager.Instance.monsters.Count;
        if (player.isDead || TurnManager.Instance.playerTurnend)
            yield break;
        
            rerollCount = count;
            yield return new WaitUntil(() => rerollCount <= 0);

            yield return new WaitForSeconds(1f);
            count = TurnManager.Instance.monsters.Count;
    }

    public void Attack()
    {
        if(!player.isDead && !TurnManager.Instance.playerTurnend && !isReroll)
        StartCoroutine(AttackCor());
    }

    IEnumerator AttackCor()
    {
        attack = firstAttack;
        defence = firstDefence;
        if (targetMonsterStats == null)
        {
            Debug.Log("공격대상을 선택하세요");
            yield break;
        }
        if (!targetMonsterStats.isDead)
        {
            for (int i = 0; i < dice.Length; i++)
            {
                if (dice[i].CompareTag("AttackDice"))
                {
                    yield return Reroll(i);
                    DiceImages[i].sprite = DiceSprites[rand];
                    ATK.text = $"{attack}";
                }
                else if(dice[i].CompareTag("DefenceDice"))
                {
                    yield return Reroll(i);
                    DiceImages[i].sprite = DiceSprites[rand];
                    DFS.text = $"{defence}";
                }
            }

            if (attack > firstAttack)
            {
                yield return targetMonsterStats.Hit(attack);
                Debug.Log("공격");
            }

            if (defence > firstDefence)
            {
                targetMonsterStats.Defense(defence);
                Debug.Log("방어");
            }
        }
    }

    public IEnumerator Reroll(int diceCount)
    {
        if (targetMonsterStats.isDead)
        {
            yield break;
        }
            isReroll = true;
            rerollCount--;
            rand = Random.Range(1, 7);
            if (dice[diceCount].CompareTag("AttackDice"))
            {
                switch (rand)
                {
                    case 1: attack += 1 + firstAttack; break;
                    case 2: attack += 2 + firstAttack; break;
                    case 3: attack += 3 + firstAttack; break;
                    case 4: attack += 4 + firstAttack; break;
                    case 5: attack += 5 + firstAttack; break;
                    case 6: attack += 6 + firstAttack; break;
                }
            }
            else if (dice[diceCount].CompareTag("DefenceDice"))
            {
                switch (rand)
                {
                    case 1: defence += 1 + firstDefence; break;
                    case 2: defence += 2 + firstDefence; break;
                    case 3: defence += 3 + firstDefence; break;
                    case 4: defence += 4 + firstDefence; break;
                    case 5: defence += 5 + firstDefence; break;
                    case 6: defence += 6 + firstDefence; break;
                }
            }
            DiceObj.SetActive(true);
            yield return new WaitForSeconds(1f);
            DiceObj.SetActive(false);
            diceImage.sprite = DiceSprites[rand];
            diceImage.enabled = true;
            yield return new WaitForSeconds(0.5f);
            diceImage.enabled = false;
            if (rerollCount <= 1)
            isReroll = false;
        }
    }
