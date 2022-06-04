using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    public void enableUI(Quest[] quests)
    {
        questUI.SetActive(true);

        TextMeshProUGUI text = null;
        hookButton.SetActive(false);
        reelButton.SetActive(false);
        baitButton.SetActive(false);

        foreach (Quest quest in quests)
        {
            if (quest.questType.Equals(Quest.questTypes.HookQuest))
            {
                hookButton.SetActive(true);
                text = hookButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            }
            else if (quest.questType.Equals(Quest.questTypes.ReelQuest))
            {
                reelButton.SetActive(true);
                text = reelButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            }
            else if (quest.questType.Equals(Quest.questTypes.BaitQuest))
            {
                baitButton.SetActive(true);
                text = baitButton.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            }

            text.SetText("Lvl " + quest.questLevel);
        }


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
