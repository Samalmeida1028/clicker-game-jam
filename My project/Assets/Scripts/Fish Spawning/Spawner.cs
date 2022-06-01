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
            activePool[i].setCenter(mainCam.transform.position);
            if(activePool[i].agents.Count == 0){
                activePool[i].DestroyAll();
                activePool.RemoveAt(i);
            }
            i++;
        }
        
    }
}
