using System;
using UnityEngine;

public class AttackScript : MonoBehaviour
{
    private int attack = StatManager.Instance.attack;
    private HpScript _hpScript;
    void Awake()
    {
        _hpScript = gameObject.GetComponent<HpScript>();
    }

    private void Start()
    {
        _hpScript.attack = attack;
    }
}
