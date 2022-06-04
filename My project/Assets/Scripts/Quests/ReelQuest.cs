using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/Quests/ReelQuest")]
public class ReelQuest : Quest
{
    public ReelQuest(int questLevel)
    {
        this.questLevel = questLevel;
        this.questType = questTypes.ReelQuest;
        this.levelModifier = 2F;
        this.conditionValue = calculateConditionValue(25);
        this.units = "Clicks";
    }
}
