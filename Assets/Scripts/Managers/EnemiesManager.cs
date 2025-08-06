using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : Singleton<EnemiesManager>
{
    public List<Enemy> Enemies { get; set; } = new();
    
    protected override void Awake()
    {
        base.Awake();
    }

    public void EnemyDied(Enemy enemy)
    {
        Enemies.Remove(enemy);
        
        if (Enemies.Count == 0)
        {
            Debug.Log("All enemies defeated!");
            GameManager.Instance.CheckWaveResult();
        }
    }
}