using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildingsController : MonoBehaviour
{
    [SerializeField]
    public Dictionary<string, PlayerBuilding> buildings = new Dictionary<string, PlayerBuilding>();

    private PlayerStatsController playerStats;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = gameObject.GetComponent<PlayerStatsController>();
    }

    // Update is called once per frame
    void Update() {
        foreach(KeyValuePair<string, PlayerBuilding> buildingEntry in buildings) {
            PlayerBuilding building = buildingEntry.Value;
            
            float lastTicked = building.getLastTicked();

            if (lastTicked != null && (Time.fixedTime - lastTicked) < building.getTickDelay()) { return; }

            building.addFishToCollect(building.getFishPerTick());
            building.setLastTicked(Time.fixedTime);
        }
    }

    public PlayerBuilding getBuilding(string buildingName) {
        if (!buildings.ContainsKey(buildingName)) { return null; }
        return buildings[buildingName];
    }

    public void collectBuilding(string buildingName) {
        PlayerBuilding building = getBuilding(buildingName);
        if (building == null) { return; }

        playerStats.addToFishMoney(building.getFishToCollect());
        building.clearFishToCollect();
    }

    public void purchaseBuilding(Building building) {
        // If the player doesn't own the building yet create a new one and set its quantity to one
        if (!buildings.ContainsKey(building.buildingName)) {
            PlayerBuilding newBuilding = new PlayerBuilding(building);
            newBuilding.addQuantity(1);
            buildings.Add(building.buildingName, newBuilding);
            return;
        }

        // If the player already owns the building, just increaes its quantity
        buildings[building.buildingName].addQuantity(1);
    }

}
