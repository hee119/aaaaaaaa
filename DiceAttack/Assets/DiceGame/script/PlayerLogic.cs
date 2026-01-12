using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

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
    public TMP_Text CTC;
    public Sprite[] DiceSprites;
    public Image diceImage;
    public GameObject[] DiceObj;
    public bool isReroll;
    private Image[] DiceImages = new Image[3];
    private int rand;
    public EnemySpawn spawnCount;
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
                    if (targetMonster != null)
                    {
                        targetMonster.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    if (hit.collider.CompareTag("Monster"))
                    {
                        targetMonster = hit.collider.gameObject;
                        targetMonsterStats = targetMonster.GetComponent<StatManager>();
                        targetMonster.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
        }
    }
    public IEnumerator PlayerTurnStart()
    {
        defence = 0;
        attack = 0;
        UpdateUi();
        
        if (player.isDead) yield break;

        TurnManager.Instance.playerTurnend = false;

        count = TurnManager.Instance.monsters.Count;

        if (count == 0)
        {
            rerollCount = 0;
            TurnManager.Instance.playerTurnend = true;
            yield break;
        }

        // 새로운 턴마다 rerollCount를 초기화
        rerollCount = count;
        yield return new WaitUntil(() => rerollCount <= 0);

        TurnManager.Instance.playerTurnend = true;
    }


    public void Attack()
    {
        if(!player.isDead && !TurnManager.Instance.playerTurnend && !isReroll && TurnManager.Instance.monsters.Count != 0)
        StartCoroutine(AttackCor());
    }

    IEnumerator AttackCor()
    {
        if (rerollCount < 1 || TurnManager.Instance.monsters.Count == 0)
        {
            TurnManager.Instance.playerTurnend = true;
            yield break;
        }
        if(player.isDead || isReroll)
        yield break;
        attack = 0;
        UpdateUi();
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
                else if (dice[i].CompareTag("GambleDice"))
                {
                    yield return Reroll(i);
                    DiceImages[i].sprite = DiceSprites[rand];
                    ATK.text = $"{attack}";
                }
            }
            rerollCount--;
            isReroll = false;
            Critical();
            if (attack > firstAttack)
            {
                yield return targetMonsterStats.Hit(attack);
                Debug.Log("공격");
            }

            if (defence > firstDefence)
            {
                player.Defense(defence);
                Debug.Log("방어");
            }
        }
    }

    public IEnumerator Reroll(int diceCount)
    {
        if (rerollCount < 1 || TurnManager.Instance.monsters.Count == 0)
        {
            rerollCount = 0;
            TurnManager.Instance.playerTurnend = true;
            yield break;
        }
        if (targetMonsterStats.isDead)
        {
            yield break;
        }

        isReroll = true;
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
            else if (dice[diceCount].CompareTag("GambleDice"))
            {
                switch (rand)
                {
                    case 1: attack += 0; break;
                    case 2: attack += (10 + firstAttack) * 2; break;
                    case 3: attack += 0; break;
                    case 4: attack += (10 + firstAttack) * 2; break;
                    case 5: attack += 0; break;
                    case 6: attack += (10 + firstAttack) * 2; break;
                }
            }
            else if (dice[diceCount].CompareTag("MadicDice"))
            {
                switch (rand)
                {
                    case 1: player.hp += 1 + firstAttack; break;
                    case 2: player.hp += 2 + firstAttack; break;
                    case 3: player.hp += 3 + firstAttack; break;
                    case 4: player.hp += 4 + firstAttack; break;
                    case 5: player.hp += 5 + firstAttack; break;
                    case 6: player.hp += 6 + firstAttack; break;
                }
            }
            DiceObj[diceCount].SetActive(true);
            yield return new WaitForSeconds(1f);
            DiceObj[diceCount].SetActive(false);
        }

    void Critical()
    {
        rand = Random.Range(1, 101);
        int critical = 0;
        double b = 0;
        if (rand <= attack * 3)
        {
            b = Mathf.Abs(rand - attack) / 10;
            b = Math.Round(b, 1);
            critical = (int)(attack + b * attack);
            attack = critical;
        }
        
        CTC.text = critical.ToString();
    }

    void UpdateUi()
    {
        ATK.text = $"{attack}";
        DFS.text = $"{defence}";
    }
    }
