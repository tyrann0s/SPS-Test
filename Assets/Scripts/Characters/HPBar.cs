using System;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject damageNumberPrefab;
    private Canvas canvas;

    private void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main;
    }

    public void Initialize(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    
    public void UpdateHealth(float damage)
    {
        slider.value -= damage;
        DamageNumber damageNumber = Instantiate(damageNumberPrefab, canvas.transform).GetComponent<DamageNumber>();
        damageNumber.Initialize(damage);
    }
    
    public void IncreaseHealth(float amount)
    {
        slider.value += amount;
    }
}
