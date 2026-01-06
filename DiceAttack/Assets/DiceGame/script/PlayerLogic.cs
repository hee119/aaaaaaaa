using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLogic : MonoBehaviour
{
    GameObject targetMonster;
    StatManager player;
    StatManager targetMonsterStats;
    private int attack;
    private int firstAttack;
    private int count;
    public bool turnend;
    void Awake()
    {
        player = gameObject.GetComponent<StatManager>();
    }

    void Start()
    {
        firstAttack = GetComponent<StatManager>().attack;
        attack = firstAttack;
    }
    void Update()
    {
        
            if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 클릭
            {
                // UI 클릭 체크
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    Debug.Log("UI(버튼 등)를 클릭해서 몬스터 클릭이 씹혔습니다.");
                    return;
                }

                // 마우스 위치 → 월드 좌표
                Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // 해당 위치에서 충돌 감지
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero); 
                // Vector2.zero를 쓰면 "해당 점에서 감지" (Point Cast처럼 동작)

                Debug.DrawRay(mousePos, Vector2.zero, Color.red, 1.0f); // 시각화용

                if (hit.collider != null)
                {
                    Debug.Log("맞춘 물체: " + hit.collider.name);

                    if (hit.collider.CompareTag("Monster"))
                    {
                        targetMonster = hit.collider.gameObject;
                        targetMonsterStats = targetMonster.GetComponent<StatManager>();
                        Debug.Log("선택된 몬스터: " + targetMonster.name);
                    }
                }
        }
    }
    public IEnumerator PlayerTurnStart()
    {
        count = TurnManager.Instance.monsters.Count;
        TurnManager.Instance.turnend = true;
        turnend = false;
        if (player.isDead)
            StopAllCoroutines();
        for (int i = 0; i < count; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int rand = Random.Range(0, 6);
                switch (rand)
                {
                    case 0: attack += 1; break;
                    case 1: attack += 2; break;
                    case 2: attack += 3; break;
                    case 3: attack += 4; break;
                    case 4: attack += 5; break;
                    case 5: attack += 6; break;
                }
            }
            yield return new WaitUntil(() => targetMonsterStats != null);

            yield return new WaitForSeconds(1f);
            attack = firstAttack;
            count = TurnManager.Instance.monsters.Count;
        }
    }

    public void Attack()
    {
        StartCoroutine(AttackCor());
    }

    IEnumerator AttackCor()
    {
        if (!targetMonsterStats.isDead && !player.isDead && TurnManager.Instance.turnend)
        {
            if (targetMonsterStats == null)
            {
                Debug.LogError("공격대상을 선택하세요");
                yield return new WaitUntil(() => targetMonsterStats != null);
            }

            targetMonsterStats.Hit(attack);
            Debug.Log("공격");
            turnend = true;
            TurnManager.Instance.turnend = false;
        }
    }
}
