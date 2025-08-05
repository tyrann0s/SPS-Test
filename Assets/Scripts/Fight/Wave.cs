using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Enemy Wave", menuName = "Enemy Wave", order = 1)]
public class Wave : ScriptableObject
{
    public List<GameObject> enemies;
}
