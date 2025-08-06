using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject upgradesPanel;
    [SerializeField] private GameObject endgamePanel;

    [SerializeField] private TextMeshProUGUI coinsText, endgameText, waveText;

    [SerializeField] private TextMeshProUGUI nextWaveAnnouncer;
    
    
    protected override void Awake()
    {
        base.Awake();
    }

    public void ShowUpgrades()
    {
        upgradesPanel.SetActive(true);
    }
    
    public void HideUpgrades()
    {
        upgradesPanel.SetActive(false);
    }

    public void ShowEndgamePanel(string text)
    {
        Debug.Log("ShowEndgamePanel");
        endgamePanel.SetActive(true);
        endgameText.text = text;
    }

    public void UpdateCons()
    {
        coinsText.text = GameManager.Instance.Coins.ToString();
    }
    
    public void UpdateWave(int currentWave, int maxWaves)
    {
        waveText.text = currentWave + "/" + maxWaves;
    }

    public void ShowNextWaveAnnouncer()
    {
        DOTween.Sequence()
            .Append(nextWaveAnnouncer.rectTransform.DOLocalMoveX(0, 1f).SetEase(Ease.OutExpo))
            .Append(nextWaveAnnouncer.rectTransform.DOLocalMoveX(2000, .5f).SetEase(Ease.OutExpo))
            .OnComplete(AnnouncerComplete);
    }

    private void AnnouncerComplete()
    {
        nextWaveAnnouncer.rectTransform.localPosition = new Vector3(-2000, 0, 0);
        GameManager.Instance.SpawnEnemies();
    }
}
