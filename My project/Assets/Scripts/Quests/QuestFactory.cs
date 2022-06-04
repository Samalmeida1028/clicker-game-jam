using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFactory : MonoBehaviour
{
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }



    public Quest getQuest(int questLevel, Quest.questTypes questType)
    {

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
