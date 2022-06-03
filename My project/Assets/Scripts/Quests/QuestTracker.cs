using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{

    //Abstract Class
    public Quest currQuest = null;
    public PlayerStatsController playerStats;
    int initialQuestVal;
    // Start is called before the first frame update
    public bool questCompleted = false;
    void Start()
    {
        //could put a tutorial quest or something here
        gameObject.GetComponent<PlayerStatsController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currQuest)
        {
            if (questCompleted == false)
            {
                questCompleted = this.currQuest.checkCondition(getCurrVal());
            }
        }

    }

    public void setCurrentQuest(Quest newQuest)
    {
        this.currQuest = newQuest;
        this.questCompleted = false;
        this.initialQuestVal = getCurrVal();
    }

    public void removeCurrentQuest()
    {
        this.currQuest = null;
        this.questCompleted = false;
        this.initialQuestVal = 0;
    }

    public bool hasActiveQuest()
    {
        return (bool)currQuest;
    }

    private int getCurrVal()
    {

        if (currQuest.Equals(null))
        {
            return 0;
        }
        else if (currQuest.questType.Equals(Quest.questTypes.BaitQuest))
        {
            return playerStats.getTotalFishCaught();
        }
        else if (currQuest.questType.Equals(Quest.questTypes.HookQuest))
        {
            //This is where the player's # of hooked fish data will go
            return playerStats.getTotalFishHooked();
        }
        else if (currQuest.questType.Equals(Quest.questTypes.ReelQuest))
        {
            //This is where the player's # of clicks data will go
            return playerStats.getTotalClicks();
        }
        else
        {
            return 0;
        }

    }

    public bool isCompleted()
    {
        return questCompleted;
    }
}
