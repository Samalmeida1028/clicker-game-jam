using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode()]

public class ProgressBar : MonoBehaviour
{

    public int max;
    public int cur;
    public Image mask;

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

    public void SetCurrentFill(int currentVal, int maxVal)
    {
        float fillAmount = (float)cur / (float)max;

        mask.fillAmount = fillAmount;
    }

}
