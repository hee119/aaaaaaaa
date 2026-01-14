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
    private DynamicTextData critTextData;

    void Awake()
    {
        diceImage.enabled = false;
        player = gameObject.GetComponent<StatManager>();
        DiceImages[0] = dice[0].GetComponent<Image>();
        DiceImages[1] = dice[1].GetComponent<Image>();
        DiceImages[2] = dice[2].GetComponent<Image>();

        // Resources 에서 원본 에셋만 로드 (DontSave 적용 금지)
        critTextData = Resources.Load<DynamicTextData>("Crit Data 1");
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
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            Debug.DrawRay(mousePos, Vector2.zero, Color.red, 1.0f); // 시각화용

            if (hit.collider != null)
            {
                if (targetMonster != null)
                    targetMonster.transform.GetChild(0).gameObject.SetActive(false);

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

        rerollCount = count;
        yield return new WaitUntil(() => rerollCount <= 0);

        TurnManager.Instance.playerTurnend = true;
    }

    public void Attack()
    {
        if (!player.isDead && !TurnManager.Instance.playerTurnend && !isReroll &&
            TurnManager.Instance.monsters.Count != 0)
        {
            attack = 0;
            player.attack = 0;
            StartCoroutine(AttackCor());
        }
    }

    IEnumerator AttackCor()
    {
        if (rerollCount < 1 || TurnManager.Instance.monsters.Count == 0)
        {
            TurnManager.Instance.playerTurnend = true;
            yield break;
        }

        if (player.isDead || isReroll)
            yield break;

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
                yield return Reroll(i);
            }

            rerollCount--;
            isReroll = false;

            Critical();

            if (attack > firstAttack)
            {
                yield return targetMonsterStats.Hit(attack);
            }

            if (defence > firstDefence)
            {
                player.Defense(defence);
            }
        }

        if (!targetMonsterStats.isDead)
        {
            TurnImage.Instance.TurnUpdate();
            TurnImage.Instance.Turn();
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
            yield break;

        isReroll = true;
        rand = Random.Range(1, 7);

        // 주사위 타입별 처리
        if (dice[diceCount].CompareTag("AttackDice"))
        {
            attack += rand + firstAttack;
            DiceImages[diceCount].sprite = DiceSprites[rand];
            ATK.text = $"{attack}";
        }
        else if (dice[diceCount].CompareTag("DefenceDice"))
        {
            defence += rand + firstDefence;
            DiceImages[diceCount].sprite = DiceSprites[rand];
            DFS.text = $"{defence}";
        }
        else if (dice[diceCount].CompareTag("GambleDice"))
        {
            if (rand % 2 == 0)
                attack += (10 + firstAttack) * 2;
            DiceImages[diceCount].sprite = DiceSprites[rand];
            ATK.text = $"{attack}";
        }
        else if (dice[diceCount].CompareTag("MadicDice"))
        {
            player.hp += rand;

            DiceObj[diceCount].SetActive(true);
            yield return new WaitForSeconds(1f);
            DiceObj[diceCount].SetActive(false);

            player.hp = Mathf.Clamp(player.hp, 0, player.maxHp);

            // 런타임 복사본 생성 후 DontSave 적용
            DynamicTextData runtimeCritData = ScriptableObject.Instantiate(critTextData);
            runtimeCritData.hideFlags = HideFlags.DontSave;

            DynamicTextManager.CreateText2D(
                transform.position + Vector3.up,
                $"{rand}",
                runtimeCritData
            );

            if (player.HpSlider != null)
                player.HpSlider.HpBar(player.hp, player.maxHp);
        }

        DiceObj[diceCount].SetActive(true);
        yield return new WaitForSeconds(1f);
        DiceObj[diceCount].SetActive(false);
    }

    void Critical()
    {
        rand = Random.Range(1, 101); // 1 ~ 100
        int critical = 0;
        double b = 0;

        if (rand <= attack * 3)
        {
            b = Mathf.Abs(rand - attack) / 10.0;
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
