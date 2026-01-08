using System;
using UnityEngine;
using UnityEngine.UI;
public class HpsliSlider : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    public void HpBar(float hp, float maxHp)
    {
        slider.value = hp / maxHp;
    }
    
}
