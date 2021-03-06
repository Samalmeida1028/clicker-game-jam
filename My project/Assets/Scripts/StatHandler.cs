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

    int hookPower;
    public float HookPower {get{return hookPower;}}

    GameObject spawner;

    TextMeshProUGUI fishText;
    PlayerStatsController stats;
    QuestTracker quest;

    ProgressBar progressBar;
    

    void Awake(){
        progressBar = GameObject.FindWithTag("Progress Bar").GetComponent<ProgressBar>();
        spawner = GameObject.FindWithTag("Spawner");
        player = GameObject.FindWithTag("Player");
        player.GetComponent<PlayerMovementController>().canMove = false;
        stats = player.GetComponent<PlayerStatsController>();
        quest = player.GetComponent<QuestTracker>();
        fishText = GameObject.FindWithTag("FishUI").GetComponent<TMPro.TextMeshProUGUI>();
        UpdateStatsFromPlayer();
        fishText.text = currentFishMoney.ToString();
        Debug.Log(baitPower);
        spawner.GetComponent<Spawner>().baitPower = baitPower;
        UpdateProgressBar();
        spawner.GetComponent<Spawner>().MAXDEPTH = player.GetComponent<PlayerStatsController>().getMaxDepthLevel()-1;
    }

    public void UpdateStatsFromPlayer(){
        numFishMult = stats.getNumFishMulti();
        totalFishHooked = stats.getTotalFishHooked();
        totalClicks = stats.getTotalClicks();
        totalFishCaught = stats.getTotalFishCaught();
        currentFishMoney = stats.getCurrentFishMoney();


        reelPower = stats.getReelPower();
        baitPower = stats.getBaitLevel();


    }
    public void UpdateProgressBar(){
        if(quest.currQuest){
            progressBar.SetCurrentFill(quest.getQuestValue(),quest.getQuestMax(), quest.currQuest.units);
        }
        else{
            progressBar.gameObject.SetActive(false);
        }
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
        UpdateProgressBar();
    }
    public void addTotalCaught(){
        totalFishCaught++;
        stats.addToFishCount(1);
        UpdateProgressBar();
    }

    public void addTotalFishHooked(){
        totalFishHooked++;
        stats.addToFishHooked(1);
        UpdateProgressBar();
    }

}
