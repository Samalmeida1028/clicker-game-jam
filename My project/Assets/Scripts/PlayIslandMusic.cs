using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIslandMusic : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<FishSoundManager>().playMusic(gameObject.GetComponent<FishSoundManager>().backgroundMusic[3],3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
