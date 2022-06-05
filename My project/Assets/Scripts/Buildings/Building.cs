using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Buildings/Building")]
public class Building : ScriptableObject
{
    public int baseFishPerTick;
    public float tickDelay;
    public string buildingName;
    public int basePrice;

}
