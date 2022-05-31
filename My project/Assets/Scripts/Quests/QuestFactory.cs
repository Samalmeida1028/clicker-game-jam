using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFactory : MonoBehaviour
{
    public bool activeQuest;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Quest getActiveQuest(){
        //should probably check if there is an active quest in the QuestUI, not the factory
        if(activeQuest){
            //get active quest from the player
            return null;
        //this should never actually trigger will remove once the QuestUIManager is made
        }else{
            return null;
        }
    }

    public Quest getQuest(int questLevel, Quest.questTypes questType){

        Quest quest = null;

        switch (questType)
        {
            case Quest.questTypes.BaitQuest:
            quest = new BaitQuest(questLevel);
            
                break;
            case Quest.questTypes.HookQuest:
            quest = new HookQuest(questLevel);
                break;
            case Quest.questTypes.ReelQuest:
            quest = new ReelQuest(questLevel);
                break;
        }

        return quest;
    }
}
