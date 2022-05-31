using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/Quests/BaitQuest")]
public class BaitQuest : Quest
{
    public BaitQuest(int questLevel){
        this.questLevel=questLevel;
        this.questType=questTypes.BaitQuest;
        this.levelModifier=.8F;
        this.conditionValue = calculateConditionValue(100);
    }   

    
}
