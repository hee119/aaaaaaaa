using System.Collections;
using JetBrains.Annotations;
using Unity.VisualScripting;
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
    
    private IObjectPool<StatManager> statPool;
    HpsliSlider hpSlider;
    
    AudioSource audio;
    
    void Start()
    {
        defenseIcon = transform.Find("defenseIcon")?.gameObject;
        statPool = PoolManager.Instance.statPool;
        hpSlider = GetComponentInChildren<HpsliSlider>();
        hp = maxHp;
        blood = transform.Find("Blood").gameObject;
        audio = GetComponent<AudioSource>();
    }
    public IEnumerator Hit(int attack)
    {
        if (defense < attack)
        {
            audio.Play();
            blood.SetActive(true);
            hp -= Mathf.Abs(attack - defense);
            hpSlider.HpBar(hp, maxHp);
            yield return new WaitForSeconds(0.7f);
            audio.Stop();
            if(defenseIcon != null)
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
            hpSlider.gameObject.SetActive(false);
            yield return new WaitForSeconds(5f);
            Die();
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
        statPool.Release(this);
        Debug.Log($"나주금{this}");
    }
    private void OnEnable()
    {
        isDead = false;
        hp = 100; // 또는 maxHp
    }

}