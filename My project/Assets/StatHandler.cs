using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatHandler : MonoBehaviour
{
    GameObject player;
    int totalFishVal;
    int totalClicks;
    int totalFishHooked;
    int totalFishCaught;

    void Start(){
        player = GameObject.FindWithTag("Player");
        UpdateStatsFromPlayer();
    }

    void UpdateStatsFromPlayer(){
        stats = player.GetComponent<PlayerStatsController>();
        stats.totalFishCaught = totalFishCaught;
        stats.totalFishHooked = total
        stats.totalClicks = total
        stats.currentFishCaught = total

    }

    void UpdatePlayerStats(){

    }

}
