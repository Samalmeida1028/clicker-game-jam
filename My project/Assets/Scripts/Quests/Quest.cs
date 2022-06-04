using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    public int questLevel;
    //Might want to just have an enum or something to check type of currValue
    public questTypes questType;
    public int conditionValue;
    public int rewardValue;
    public int rewardType;
    public string units;
    protected float levelModifier;

    public bool checkCondition(int currValue)
    {
        Debug.Log(this.GetType() + ": " + currValue + " / " + conditionValue);
        return currValue >= conditionValue;
    }

    public double getPercentage(int currValue)
    {
        return (double)currValue / (double)conditionValue;
    }

    public enum questTypes
    {
        BaitQuest,
        HookQuest,
        ReelQuest,
        NullQuest
    }

    protected int calculateConditionValue(int value)
    {
        int n = (int)Mathf.Ceil(value * Mathf.Pow(1 + levelModifier, questLevel));
        if (n % 10 != 0)
            n = n + (10 - n % 10);
        return n;
    }
}
