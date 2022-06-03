using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public QuestUIManager questUI;
    public QuestTracker questTracker;
    public GameObject player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void interact(GameObject player)
    {
        this.player = player;
        questTracker = player.GetComponent<QuestTracker>();

        bool activeQuest = questTracker.hasActiveQuest();
        //should enable quest submission ui when active quest
        questUI.enableUI();
        if (activeQuest)
        {
            Debug.Log("YO ACTIVE QUEST");
            Debug.Log(questTracker.currQuest);
            Quest currQuest = questTracker.currQuest;
            if (questTracker.isCompleted())
            {
                Debug.Log("YO COMPLETED QUEST");
            }
        }
        else
        {
            // questUI.enableUI();
            foreach (Quest.questTypes questType in System.Enum.GetValues(typeof(Quest.questTypes)))
            {
                getQuest(player, questType);
            }


        }
    }

    //Attached to confirm button on quest ui
    public void chooseQuest()
    {
        Quest.questTypes newQuestType = questUI.disableUI();
        questUI.resetButtons();

        if (!newQuestType.Equals(Quest.questTypes.NullQuest))
        {
            Quest newQuest = getQuest(player, newQuestType);
            questTracker.setCurrentQuest(newQuest);
        }

    }

    public Quest getQuest(GameObject player, Quest.questTypes questType)
    {

        Quest quest = null;
        PlayerStatsController playerStats = player.GetComponent<PlayerStatsController>();

        switch (questType)
        {
            case Quest.questTypes.BaitQuest:
                quest = new BaitQuest(playerStats.getBaitLevel());
                break;
            case Quest.questTypes.HookQuest:
                quest = new HookQuest(playerStats.getHookLevel());
                break;
            case Quest.questTypes.ReelQuest:
                quest = new ReelQuest(playerStats.getReelLevel());
                break;
        }

        return quest;
    }
}
