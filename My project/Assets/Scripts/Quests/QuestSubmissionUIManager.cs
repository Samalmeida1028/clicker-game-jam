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
    public void enableUI()
    {
        questUI.SetActive(true);
    }

    public void disableUI()
    {
        questUI.SetActive(false);

    }

}
