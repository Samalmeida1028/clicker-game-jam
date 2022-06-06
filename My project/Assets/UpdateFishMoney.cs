using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateFishMoney : MonoBehaviour
{
    PlayerStatsController Player;
    public TextMeshProUGUI fishText;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerStatsController>();
    }

    // Update is called once per frame
    void Update()
    {
        fishText.text = Player.getCurrentFishMoney().ToString();
    }
}
