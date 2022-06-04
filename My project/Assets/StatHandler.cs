using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatHandler : MonoBehaviour
{
    GameObject player;

    int currentFishMoney;
    public int CurrentFishMoney{get{return currentFishMoney;}}

    int totalClicks;
    int TotalClicks {get{return totalClicks;}}

    int totalFishHooked;
    public int TotalFishHooked  {get{return totalFishHooked;}}

    int totalFishCaught;
    public int TotalFishCaught  {get{return totalFishCaught;}}

    float numFishMult;
    public float NumFishMult {get{return numFishMult;}}

    float reelPower;
    public float ReelPower {get{return reelPower;}}

    int baitPower;
    public float BaitPower {get{return baitPower;}}

    GameObject spawner;

    public TextMeshProUGUI fishText;
    PlayerStatsController stats;
    

    void Awake(){
        spawner = GameObject.FindWithTag("Spawner");
        player = GameObject.FindWithTag("Player");
        fishText = GameObject.FindWithTag("FishUI").GetComponent<TMPro.TextMeshProUGUI>();
        UpdateStatsFromPlayer();
        fishText.text = currentFishMoney.ToString();
        spawner.GetComponent<Spawner>().baitPower = baitPower;
    }

    public void UpdateStatsFromPlayer(){
        stats = player.GetComponent<PlayerStatsController>();
        numFishMult = stats.getNumFishMulti();
        reelPower = stats.getReelPower();
        totalFishHooked = stats.getTotalFishHooked();
        totalClicks = stats.getTotalClicks();
        totalFishCaught = stats.getTotalFishCaught();
        currentFishMoney = stats.getCurrentFishMoney();
        baitPower = stats.getBaitLevel();

    }

    // void UpdatePlayerStats(){
    //     stats = player.GetComponent<PlayerStatsController>();
    //     stats.setTotalFishCaught(totalFishCaught);
    //     stats.totalFishHooked = totalFishHooked;
    //     stats.totalClicks = totalClicks;
    // }

    public void addTotalMoney(int money){
        int scaledMoney = (int)Mathf.Round(money*stats.getFishValue());
        currentFishMoney += scaledMoney;
        stats.addToFishMoney(scaledMoney);
        fishText.text = currentFishMoney.ToString();
    }

    public void addTotalClicks(){
        totalClicks++;
        stats.addToTotalClicks(1);
    }
    public void addTotalCaught(){
        totalFishCaught++;
        stats.addToFishCount(1);
    }

    public void addTotalFishHooked(){
        totalFishHooked++;
        stats.addToFishHooked(1);
    }

}
