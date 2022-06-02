using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateWithFishValue : MonoBehaviour
{
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        text = gameObject.GetComponent<TextMeshProUGUI>();
        text.text = "HELLO";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
