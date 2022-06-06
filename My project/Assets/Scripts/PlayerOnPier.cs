using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerOnPier : MonoBehaviour
{
    private GameObject fishingButton;
    private Image image;
    private Button button;
    private TMP_Text text;
    // Start is called before the first frame update

    private void Start()
    {
        fishingButton = GameObject.Find("FishingButton");
        image = fishingButton.GetComponent<Image>();
        button = fishingButton.GetComponent<Button>();
        text = fishingButton.GetComponentInChildren<TMP_Text>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        image.enabled = true;

        button.enabled = true;

        text.enabled = true;
    }

    // Update is called once per frame
    void OnTriggerExit2D(Collider2D other)
    {
        image.enabled = false;

        button.enabled = false;

        text.enabled = false;
    }
}
