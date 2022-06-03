using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuestUIManager : MonoBehaviour
{

    public GameObject questUI;
    public GameObject hookButton;
    public GameObject reelButton;
    public GameObject baitButton;

    public bool hookActive = false;
    public bool baitActive = false;
    public bool reelActive = false;
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
    public void enableUI()
    {
        questUI.SetActive(true);
    }

    public Quest.questTypes disableUI()
    {
        questUI.SetActive(false);

        if (this.hookActive)
        {
            return Quest.questTypes.HookQuest;
        }
        else if (this.baitActive)
        {
            return Quest.questTypes.BaitQuest;
        }
        else if (this.reelActive)
        {
            return Quest.questTypes.ReelQuest;
        }
        else
        {
            return Quest.questTypes.NullQuest;
        }

    }

    public void setHookActive()
    {
        this.baitActive = false;
        this.reelActive = false;
        this.hookActive = true;
        hookButton.GetComponent<Button>().interactable = false;
        baitButton.GetComponent<Button>().interactable = true;
        reelButton.GetComponent<Button>().interactable = true;
    }

    public void setBaitActive()
    {
        this.baitActive = true;
        this.reelActive = false;
        this.hookActive = false;
        hookButton.GetComponent<Button>().interactable = true;
        baitButton.GetComponent<Button>().interactable = false;
        reelButton.GetComponent<Button>().interactable = true;
    }

    public void setReelActive()
    {
        this.baitActive = false;
        this.reelActive = true;
        this.hookActive = false;
        hookButton.GetComponent<Button>().interactable = true;
        baitButton.GetComponent<Button>().interactable = true;
        reelButton.GetComponent<Button>().interactable = false;
    }

    public void resetButtons()
    {
        this.baitActive = false;
        this.reelActive = false;
        this.hookActive = false;
        hookButton.GetComponent<Button>().interactable = true;
        baitButton.GetComponent<Button>().interactable = true;
        reelButton.GetComponent<Button>().interactable = true;
    }
}
