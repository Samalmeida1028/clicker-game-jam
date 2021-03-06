using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour
{
    public int baseReelPower = 1;
    public int baseFishValue = 1;
    public int baseNumFish = 1;
    public int baitLevel = 1;
    public int hookLevel = 1;
    public int reelLevel = 1;
    //change to int * multiplier of 10s
    public int totalFishCaught;
    public int totalFishHooked;
    public int totalClicks;
    public int totalFishMoney;
    public int currentFishMoney;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getTotalFishMoney()
    {
        return this.totalFishMoney;
    }

    public int getCurrentFishMoney(){
        return this.currentFishMoney;
    }

    public int getTotalFishCaught()
    {
        return this.totalFishCaught;
    }

    public int getTotalFishHooked()
    {
        return this.totalFishHooked;
    }

    public int getTotalClicks()
    {
        return this.totalClicks;
    }

    public int getMaxDepthLevel()
    {
        //Unlock new depth level after upgrading each one to level 3
        return Mathf.Min(1 + (baitLevel + hookLevel + reelLevel) / 9, 3);
    }

    public float getReelPower()
    {
        return baseReelPower * reelLevel;
    }

    public float getFishValue()
    {
        return baseFishValue * hookLevel;
    }

    public float getNumFishMulti()
    {
        return baseNumFish * baitLevel;
    }

    public int addToFishCount(int fishCaught)
    {
        this.totalFishCaught += fishCaught;
        return this.totalFishCaught;
    }

    public int addToFishMoney(int fishValue)
    {
        this.totalFishMoney += fishValue;
        this.currentFishMoney += fishValue;
        return this.totalFishMoney;
    }

    public int subtractFromCurrentFishMoney(int fishSpent)
    {
        this.currentFishMoney -= fishSpent;

        return this.currentFishMoney;
    }

    public int addToTotalClicks(int numClicks)
    {
        this.totalClicks += numClicks;

        return this.totalClicks;
    }

    public int addToFishHooked(int numHooked)
    {
        this.totalFishHooked += numHooked;

        return this.totalFishHooked;
    }

    public int getStatByType(Quest.questTypes questType)
    {
        switch (questType)
        {
            case Quest.questTypes.BaitQuest:
                return getTotalFishCaught();
            case Quest.questTypes.HookQuest:
                return getTotalFishHooked();

            case Quest.questTypes.ReelQuest:
                return getTotalClicks();
            default:
                return -1;

        }
    }

    public int getReelLevel()
    {
        return this.reelLevel;
    }

    public int getHookLevel()
    {
        return this.hookLevel;
    }

    public int getBaitLevel()
    {
        return this.baitLevel;
    }

    public void upgradeLevel(Quest.questTypes questType)
    {
        if (questType.Equals(Quest.questTypes.BaitQuest))
        {
            this.baitLevel++;
        }
        else if (questType.Equals(Quest.questTypes.HookQuest))
        {
            //This is where the player's # of hooked fish data will go
            this.hookLevel++;
        }
        else if (questType.Equals(Quest.questTypes.ReelQuest))
        {
            //This is where the player's # of clicks data will go
            this.reelLevel++;
        }
    }
}
