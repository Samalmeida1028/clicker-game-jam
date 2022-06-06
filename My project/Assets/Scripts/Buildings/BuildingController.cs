using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingController : MonoBehaviour
{
    public Building building;
    public int fishToCollect = 0;
    public int buildingQuantity = 0;
    public float currentPrice = 0;
    private GameObject player;
    private PlayerBuildingsController playerBuldingsController;
    private SpriteRenderer renderer;
    private float priceIncrease = 1.15f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerBuldingsController = player.GetComponent<PlayerBuildingsController>();
        renderer = gameObject.GetComponent<SpriteRenderer>();

        currentPrice = (float)building.basePrice;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerBuilding playerBuilding = playerBuldingsController.getBuilding(building.buildingName);

        if (playerBuilding == null) { return; }
        
        fishToCollect = playerBuilding.getFishToCollect();
        buildingQuantity = playerBuilding.getQuantity();

        currentPrice = Mathf.Round(building.basePrice * Mathf.Pow(priceIncrease, buildingQuantity));

        updateLook();
    }

    public void updateLook() {
        if (buildingQuantity >= 5) {
            renderer.color = Color.white;
        };

        if (buildingQuantity >= 10) {
            renderer.color = Color.red;
        }

        if (buildingQuantity >= 15) {
            renderer.color = Color.blue;
        }
    }

    public void purchase() {
        PlayerStatsController playerStats = player.GetComponent<PlayerStatsController>();
        
        if (playerStats.getCurrentFishMoney() < (int)currentPrice) { Debug.Log("Cannot Afford"); return; }

        playerStats.subtractFromCurrentFishMoney((int)currentPrice);
        player.GetComponent<PlayerBuildingsController>().purchaseBuilding(building);
    }

    public void collect() {
        playerBuldingsController.collectBuilding(building.buildingName);
    }
}
