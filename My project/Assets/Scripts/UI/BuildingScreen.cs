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

    public Button purchase1;
    public Button purchase10;
    public Button purchase100;

    public Button collect;

    private GameObject player;
    private BuildingController controller;

    void Update() {
        if ((player.transform.position - gameObject.transform.position).magnitude <= 2 ){ UI.SetActive(true); }
        else { UI.SetActive(false); }

        titleText.SetText(controller.building.buildingName);
        quantityText.SetText("Owned: " + controller.buildingQuantity);
        currentFishText.SetText("Fish To Collect: " + controller.fishToCollect);
        buildingPrice.SetText("Price: " + controller.currentPrice + " Fish");
    }

    void Start() {
        UI.SetActive(false);
        controller = gameObject.GetComponent<BuildingController>();
        player = GameObject.FindWithTag("Player");
        //purchase1 = GameObject.FindWithTag("Purchase1").GetComponent<Button>(); //FIX FOR ETHAN??? IDK IT FIXES THE NULL REFERENCE BUT STILL DOESNT WORK

        purchase1.onClick.AddListener(purchaseOne);

        collect.onClick.AddListener(controller.collect);
    }

    void purchaseOne() {
        controller.purchase(1);
    }

    void purchaseTen() {
        controller.purchase(10);
    }

    void purchaseHundead() {
        controller.purchase(100);
    }
}
