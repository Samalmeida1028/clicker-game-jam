using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<Flock> CommonFlockPool;
    public List<Flock> UncommonFlockPool;
    public List<Flock> RareFlockPool;
    public List <Flock> activePool;
    public int minFlockVal, maxFlockVal;
    public int maxFlocks;
    public int maxFlockSize = 70;
    public Camera mainCam;
    public float commonChance, uncommonChance, rareChance;

    void Start()
    {
        int i = 0;
        while(i<maxFlocks){
        Flock p;
        int value = Random.Range(minFlockVal, maxFlockVal);
        float pick = Random.Range(0.0f,1.0f);
        if(0<pick&&pick<commonChance){
            int flock = Random.Range(0,CommonFlockPool.Count);
            p = Instantiate(CommonFlockPool[flock]);
            p.createByValue(value,maxFlockSize);            
        }
        else if(commonChance<pick&&pick<uncommonChance){
            int flock = Random.Range(0,UncommonFlockPool.Count);
            p = Instantiate(UncommonFlockPool[flock]);
            p.createByValue(value*2,maxFlockSize);            
        }
        else{
            int flock = Random.Range(0,RareFlockPool.Count);
            p = Instantiate(RareFlockPool[flock]);
            p.createByValue(value*5,maxFlockSize);            
        }
        mainCam = Camera.main;
        activePool.Add(p);
        p.mainCam = mainCam;
        i++;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        while(i < activePool.Count){
            if(activePool[i].isClose()){
            Vector2 target = createTarget(activePool[i].wasInside(), activePool[i].target);
            activePool[i].setTarget(target);
            }
            if(activePool[i].agents.Count == 0){
                activePool[i].DestroyAll();
                activePool.RemoveAt(i);
            }
            i++;
        }
        
    }

    public Vector2 createTarget(bool isInside, Vector2 prevTarget){
        Vector2 target;
        float screenAspect = (float)Screen.width/(float)Screen.height;
        float innerRadius = mainCam.orthographicSize;
        float outerRadius = mainCam.orthographicSize*screenAspect*2;
        Vector2 cameraPos = mainCam.transform.position;
        Vector2 cameraCenter = cameraPos + new Vector2(innerRadius,outerRadius/2);
        if(isInside){
            float x = Random.Range(innerRadius*2,outerRadius*2);
            target = prevTarget + new Vector2(x,x)*Random.insideUnitCircle;
            return target;
        }
        else{
            float x = Random.Range(0,innerRadius);
            target = new Vector2(x,x)*Random.insideUnitCircle;
            
            return target;

        }
    }
}
