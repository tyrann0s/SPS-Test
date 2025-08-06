using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : Character
{
    protected Transform playerTransform;
    
    protected override void Start()
    {
        base.Start();
        playerTransform = FindAnyObjectByType<Player>().transform;
    }
    
    protected override void Died()
    {
        GameManager.Instance.IncreaseCoins();
        EnemiesManager.Instance.EnemyDied(this);
        base.Died();
    }
}
