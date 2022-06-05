using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingScreen : MonoBehaviour
{
    public GameObject UI;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI quantityText;
    public TextMeshProUGUI currentFishText;
    public TextMeshProUGUI buildingPrice;

    public Button purchase;
    public Button collect;

    private GameObject player;
    private BuildingController controller;

    void Update() {
        //if ((player.transform.position - gameObject.transform.position).magnitude <= 2 ){ UI.SetActive(true); }
       // else { UI.SetActive(false); }

        titleText.SetText(controller.building.buildingName);
        quantityText.SetText("Owned: " + controller.buildingQuantity);
        currentFishText.SetText("Fish To Collect: " + controller.fishToCollect);
        buildingPrice.SetText("Price: " + controller.currentPrice + " Fish");
    }

    void Start() {
        UI.SetActive(true);
        controller = gameObject.GetComponent<BuildingController>();
        player = GameObject.FindWithTag("Player");

        purchase.onClick.AddListener(controller.purchase);
        collect.onClick.AddListener(controller.collect);
    }
}
