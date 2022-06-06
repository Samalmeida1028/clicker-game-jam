using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DepthChangeButton : MonoBehaviour
{
    public Spawner spawner;
    public int depth= 0;
    public TextMeshProUGUI text;

    
    void Start(){
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();
        text = GameObject.FindWithTag("Depth").GetComponent<TextMeshProUGUI>();
        text.text = depth.ToString();
        checkMaxDepth();
    }

    public void DepthDecrease(){
        depth = spawner.Depth;
        if(depth != 0){
        spawner.refresh();
        }
        spawner.Depth-=1;
        depth = spawner.Depth;
        checkMinDepth();
        text.text = depth.ToString();
    }
    public void DepthIncrease(){
        depth = spawner.Depth;
        if(depth != spawner.MAXDEPTH){
        spawner.refresh();
        }
        spawner.Depth+=1;
        checkMaxDepth();
        depth = spawner.Depth;
        text.text = depth.ToString();
    }

    public void checkMaxDepth(){
        if(depth != spawner.MAXDEPTH){
        gameObject.GetComponent<ChangeSprite>().setSprite1();
        }
        else{
        gameObject.GetComponent<ChangeSprite>().setSprite2(); 
        }
    }

        public void checkMinDepth(){
        if(depth != 0){
        gameObject.GetComponent<ChangeSprite>().setSprite1();
        }
        else{
        gameObject.GetComponent<ChangeSprite>().setSprite2(); 
        }
    }


}
