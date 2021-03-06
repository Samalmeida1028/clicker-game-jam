using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSubmissionUIManager : MonoBehaviour
{

    public GameObject questUI;
    public GameObject hookButton;
    public GameObject reelButton;
    public GameObject baitButton;
    public GameObject progressBar;
    public GameObject submitButton;

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
    public void enableUI(int max, int currentVal, Quest currQuest, bool submit)
    {

        progressBar.GetComponent<ProgressBar>().SetCurrentFill(currentVal, max, currQuest.units);

        TextMeshProUGUI text = null;
        hookButton.SetActive(false);
        reelButton.SetActive(false);
        baitButton.SetActive(false);

        if (currQuest.questType.Equals(Quest.questTypes.HookQuest))
        {
            hookButton.SetActive(true);
            text = hookButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        else if (currQuest.questType.Equals(Quest.questTypes.ReelQuest))
        {
            reelButton.SetActive(true);
            text = reelButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        }
        else if (currQuest.questType.Equals(Quest.questTypes.BaitQuest))
        {
            baitButton.SetActive(true);
            text = baitButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        }

        text.SetText("Lvl " + currQuest.questLevel);

        questUI.SetActive(true);

        if (!submit)
        {
            submitButton.GetComponent<Button>().interactable = (false);
        }
        else
        {
            submitButton.GetComponent<Button>().interactable = (true);
        }

    }

    public void disableUI()
    {
        questUI.SetActive(false);

    }

}
