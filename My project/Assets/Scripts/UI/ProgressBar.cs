using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]

public class ProgressBar : MonoBehaviour
{

    public int max;
    public int cur;
    public Image mask;

    public TextMeshProUGUI currText;
    public TextMeshProUGUI endText;
    public TextMeshProUGUI unitsText;

    void Start()
    {

    }

    // // Update is called once per frame
    // void Update()
    // {
    //     GetCurrentFill();
    // }

    // void GetCurrentFill()
    // {
    //     float fillAmount = (float)cur / (float)max;

    //     mask.fillAmount = fillAmount;
    // }

    public void SetCurrentFill(int currentVal, int maxVal, string units)
    {
        this.max = maxVal;
        this.cur = currentVal;

        float fillAmount = (float)cur / (float)max;

        currText.SetText("" + this.cur);
        endText.SetText("" + this.max);
        unitsText.SetText(units);

        mask.fillAmount = fillAmount;
    }

}
