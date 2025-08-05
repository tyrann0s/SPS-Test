using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<Wave> waves;
    
    protected override void Awake()
    {
        base.Awake();
    }
}
