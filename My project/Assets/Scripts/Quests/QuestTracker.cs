using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTracker : MonoBehaviour
{

    //Abstract Class
    public Quest currQuest = null;
    int initialQuestVal;
    // Start is called before the first frame update
    bool questCompleted = false;
    void Start()
    {
        //could put a tutorial quest or something here
    }

    // Update is called once per frame
    void Update()
    {
        if(currQuest){
            if(questCompleted==false){
                this.currQuest.checkCondition(getCurrVal());
            }
        }

    }

    public void setCurrentQuest(Quest newQuest){
        this.currQuest=newQuest;
        this.questCompleted=false;
        this.initialQuestVal=getCurrVal();
    }

    public void removeCurrentQuest(){
        this.currQuest=null;
        this.questCompleted=false;
        this.initialQuestVal=0;
    }

    private int getCurrVal(){
        
        if(currQuest.Equals(null)){
            return 0;
        }else if(currQuest.questType.Equals(Quest.questTypes.BaitQuest)){
            //This is where the player's # of caught fish data will go
            return 0;
        }else if(currQuest.questType.Equals(Quest.questTypes.HookQuest)){
            //This is where the player's # of hooked fish data will go
            return 0;
        }else if(currQuest.questType.Equals(Quest.questTypes.ReelQuest)){
            //This is where the player's # of clicks data will go
            return 0;
        }else{
            return 0;
        }

    }
}
