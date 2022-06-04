using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSubmissionUIManager : MonoBehaviour
{

    public GameObject questUI;
    public Image hookButton;
    public Image reelButton;
    public Image baitButton;
    public GameObject progressBar;

    void Start()
    {
        questUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    //takes in list of quests
    //should also be changed to enable quest selection ui 
    public void enableUI(int max, int currentVal, Quest.questTypes questType)
    {
        questUI.SetActive(true);

        progressBar.GetComponent<ProgressBar>().SetCurrentFill(currentVal, max);

        bool hookButton = false;
        bool reelButton = false;
        bool baitButton = false;

        if (questType.Equals(Quest.questTypes.HookQuest))
        {
            hookButton = true;
        }
        else if (questType.Equals(Quest.questTypes.ReelQuest))
        {
            reelButton = true;
        }
        else if (questType.Equals(Quest.questTypes.BaitQuest))
        {
            baitButton = true;
        }
        else
        {

        }
    }

    public void disableUI()
    {
        questUI.SetActive(false);

    }

}
