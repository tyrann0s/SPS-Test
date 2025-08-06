using System;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private string basicText;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        text.text = $"{basicText}\n({GameManager.Instance.GetUpgradePrice()} coins)";
    }
}
