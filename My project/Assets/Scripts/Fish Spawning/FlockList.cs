using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Spawning/Flock List")]
public class FlockList : ScriptableObject
{
    [Range(0,10)]
    public float spawnChance;
    public int valueMult;
    public int rarity;
    public int minAgentVal;
    public int maxAgentVal;
    public List<Flock> flockList;
}
