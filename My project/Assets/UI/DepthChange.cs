using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthChange : MonoBehaviour
{
    public Spawner spawner;
    int depth= 0;
    public TextMeshProUGUI text;

    
    void Start(){
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        text = GameObject.FindWithTag("Depth").GetComponent<TextMeshProUGUI>();
        text.text = depth.ToString();
    }

    public void DepthIncrease(){
        depth = spawner.Depth;
        spawner.Depth+=1;
        if(depth != spawner.Depth){
        spawner.refresh();
        }
        depth = spawner.Depth;
        text.text = depth.ToString();
    }
        public void DepthDecrease(){
        depth = spawner.Depth;
        spawner.Depth-=1;
        if(depth != spawner.Depth){
        spawner.refresh();
        }
        depth = spawner.Depth;
        text.text = depth.ToString();
    }

    public void checkDepth(){
        if(depth != spawner.Depth){
        gameObject.GetComponent<ChangeSprite>().setSprite1();
        }
        else{
        gameObject.GetComponent<ChangeSprite>().setSprite2(); 
        }

    }
}
