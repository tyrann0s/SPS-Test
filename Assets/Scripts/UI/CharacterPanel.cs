using System;
using TMPro;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI attackSpeedText;
    
    private void Start()
    {
        GameManager.Instance.Player.OnHPChanged += UpdatePlayerHP;
        GameManager.Instance.Player.OnDamageChanged += UpdatePlayerDamage;
        GameManager.Instance.Player.OnAttackSpeedChanged += UpdatePlayerAttackSpeed;
    }
    
    public void UpdatePlayerHP(float health)
    {
        hpText.text = health.ToString();
    }
    
    public void UpdatePlayerDamage(float damage)
    {
        damageText.text = damage.ToString();
    }
    
    public void UpdatePlayerAttackSpeed(float attackSpeed)
    {
        float realSpeed = 1 / attackSpeed;
        realSpeed = Mathf.Round(realSpeed * 100) / 100;
        attackSpeedText.text = realSpeed + " per second";
    }
}
