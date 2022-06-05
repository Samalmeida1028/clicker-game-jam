using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/Quests/BaitQuest")]
public class BaitQuest : Quest
{
    public BaitQuest(int questLevel)
    {
        this.questLevel = questLevel;
        this.questType = questTypes.BaitQuest;
        this.levelModifier = 1F;
        this.conditionValue = calculateConditionValue(10);
        this.units = "Fish Caught";
    }


}
