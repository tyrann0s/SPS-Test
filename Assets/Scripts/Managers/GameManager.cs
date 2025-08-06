using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private Player player;
    public Player Player => player;
    
    [SerializeField] private List<Wave> waves;
    
    [SerializeField] private Transform rangeSpawnPoint;
    [SerializeField] private Transform meleeSpawnPoint;
    
    public float Coins { get; private set; } = 0;

    [Header("Upgrade Prices")] 
    [SerializeField] private float basicPrice = 3f;
    [SerializeField] private float priceMod = 1.5f;
    
    
    private int currentWave;
    private int currentEnemy;
    private int maxEnemies;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        currentEnemy = 0;

        if (maxEnemies < waves[currentWave].enemies.Count)
        {
            UIManager.Instance.UpdateWave(currentWave + 1, waves.Count);
            UIManager.Instance.ShowNextWaveAnnouncer();
        }
    }

    public void SpawnEnemies()
    {
        int waveSize = 2;
        if (waves[currentWave].enemies.Count < 2) waveSize = 1;
                
        for (int currentEnemy = 0; currentEnemy < waveSize; currentEnemy++)
        {
            GameObject enemyPrefab = waves[currentWave].enemies[maxEnemies];
            GameObject spawnedEnemy;
            
            if (enemyPrefab.GetComponent<EnemyRanged>())
            {
                spawnedEnemy = Instantiate(enemyPrefab, rangeSpawnPoint.position, Quaternion.identity);
                EnemiesManager.Instance.Enemies.Add(spawnedEnemy.GetComponent<EnemyRanged>());
            }
            else if (enemyPrefab.GetComponent<EnemyMelee>())
            {
                spawnedEnemy = Instantiate(enemyPrefab, meleeSpawnPoint.position, Quaternion.identity);
                EnemiesManager.Instance.Enemies.Add(spawnedEnemy.GetComponent<EnemyMelee>());
            }
                
            maxEnemies++;
        }
        
        maxEnemies = 0;
        currentWave++;
    }

    public void CheckWaveResult()
    {
        if (currentWave >= waves.Count)
        {
            UIManager.Instance.ShowEndgamePanel("You Win!");
        } else UIManager.Instance.ShowUpgrades();
    }

    public void IncreasePlayerDamage()
    {
        if (Coins >= GetUpgradePrice())
        {
            DecreaseCoins(GetUpgradePrice());
            player.IncreaseDamage(5f);
            UIManager.Instance.HideUpgrades();
            StartWave();
            
            basicPrice *= priceMod;
        }
    }
    
    public void IncreasePlayerAttackSpeed()
    {
        if (Coins >= GetUpgradePrice())
        {
            DecreaseCoins(GetUpgradePrice());
            player.IncreaseAttackSpeed(.5f);
            UIManager.Instance.HideUpgrades();
            StartWave();
            
            basicPrice *= priceMod;
        }
    }

    public void IncreasePlayerHealth()
    {
        player.IncreaseHealth(25f);
        UIManager.Instance.HideUpgrades();
        StartWave();
    }
    
    public void IncreaseCoins()
    {
        Coins += 2;
        UIManager.Instance.UpdateCons();
    }
    
    private void DecreaseCoins(float value)
    {
        Coins -= value;
        UIManager.Instance.UpdateCons();   
    }

    public float GetUpgradePrice()
    {
        return basicPrice;
    }
}