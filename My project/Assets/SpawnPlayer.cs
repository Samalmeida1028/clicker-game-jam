using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        player.transform.position = gameObject.transform.position;
        player.GetComponent<PlayerMovementController>().canMove = true;
        
    }
}
