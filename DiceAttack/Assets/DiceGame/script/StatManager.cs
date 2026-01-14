using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Pool;

public class StatManager : MonoBehaviour
{
    public int maxHp;
    public int hp;
    public int attack;
    public bool isDead;
    public int defense;
    public Animator animator;
    private GameObject blood;
    [CanBeNull] private GameObject defenseIcon;
    private GameObject player;

    private IObjectPool<StatManager> statPool;
    private HpsliSlider hpSlider; // private으로 관리 (자식에서 찾음)

    AudioSource audio;
    public DynamicTextData critTextData;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Start()
    {
        defenseIcon = transform.Find("defenseIcon")?.gameObject;
        statPool = PoolManager.Instance.statPool;
        hpSlider = GetComponentInChildren<HpsliSlider>();

        blood = transform.Find("Blood").gameObject;
        audio = GetComponent<AudioSource>();
        hp = maxHp;
        if (hpSlider != null) hpSlider.HpBar(hp, maxHp);
    }

    public IEnumerator Hit(int attack)
    {
        if (defense < attack)
        {
            DynamicTextManager.CreateText2D(
                transform.position + Vector3.up,
                $"{attack}",
                critTextData
            );
            audio.Play();
            blood.SetActive(true);
            hp -= Mathf.Abs(attack - defense);
            if (hpSlider != null) hpSlider.HpBar(hp, maxHp);

            yield return new WaitForSeconds(0.7f);
            audio.Stop();
            if (defenseIcon != null)
                defenseIcon.SetActive(false);
            
            blood.SetActive(false);
        }
        else
        {
            Debug.Log("완전방어에 성공했습니다.");
            if (defenseIcon != null)
                defenseIcon.SetActive(false);
        }

        if (hp <= 0 && !isDead)
        {
            isDead = true;
            if (gameObject.transform.GetChild(0).gameObject.activeSelf)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
            animator.SetTrigger("Die");
            if (hpSlider != null) hpSlider.gameObject.SetActive(false);
            Die();
            gameObject.transform.GetChild(2).gameObject.SetActive(false);
            yield return new WaitForSeconds(5f);
            statPool.Release(this); 
        }
    }

    public void Defense(int defense)
    {
        this.defense += defense;
        Debug.Log("방어중입니다.");
    }

    public void SetStatPool(IObjectPool<StatManager> pool)
    {
        statPool = pool;
    }

    void Die()
    { 
        TurnManager.Instance.monsters.Remove(gameObject);
        TurnImage.Instance.turnImage.Remove(gameObject);
        TurnImage.Instance.turnImage.Remove(player);
        TurnImage.Instance.Turn();
        Debug.Log($"나주금{this}");
    }

    private void OnEnable()
    {
        isDead = false;
        hp = maxHp; // 100 대신 설정된 maxHp로 초기화
        gameObject.transform.GetChild(2).gameObject.SetActive(true);
        // [핵심] 오브젝트 풀에서 다시 나올 때 슬라이더 상태 복구
        if (hpSlider != null)
        {
            hpSlider.gameObject.SetActive(true); // 꺼졌던 슬라이더 켜기
            hpSlider.HpBar(hp, maxHp);           // 바 수치 꽉 채우기
        }
    }
}