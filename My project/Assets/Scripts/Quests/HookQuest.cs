using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scripts/Quests/HookQuest")]
public class HookQuest : Quest
{


    public HookQuest(int questLevel){
        this.questLevel=questLevel;
        this.questType=questTypes.HookQuest;
        this.levelModifier=.8F;
        this.conditionValue = calculateConditionValue(100);
    }
}
