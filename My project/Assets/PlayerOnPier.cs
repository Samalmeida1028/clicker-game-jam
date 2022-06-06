using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPier : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter()
    {
        Debug.Log("player entered pier");
    }

    // Update is called once per frame
    void OnTriggerExit()
    {
        Debug.Log("player entered pier");
    }
}
