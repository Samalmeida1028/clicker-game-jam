using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<FlockList> FlockPool;
    public List <Flock> activePool;
    public int minFlockVal, maxFlockVal;
    public int maxFlocks = 20;
    public int maxFlockSize = 70;
    public Camera mainCam;
    public int MAXDEPTH = 0;
    int depth = 0;
    public int Depth
    {
        get{return depth;}

        set{
            if(value>=0&&value<=MAXDEPTH){
                depth = value;
            }
        }
    }

    void Start()
    {
        mainCam = Camera.main;
        activePool = randomFlock(maxFlocks);
        
    }

    void Update()
    {
        int i = 0;
        while(i < activePool.Count){
            if(activePool[i].isClose()){    // Checks each flock to see if it reached it's target
            Vector2 target = createTarget(activePool[i].wasInside(), activePool[i].target); //created a new target using createTarget();
            activePool[i].setTarget(target); //sets that target to be the flocks new target
            }
            if(activePool[i].agents.Count == 0){ //checks to see if a flock is empty, and destroys it if it is
            removeFlock(i);
            }
            i++;
        }
        if(activePool.Count < maxFlocks){
            List<Flock> newFlocks = randomFlock(maxFlocks - activePool.Count);
            activePool.AddRange(newFlocks);

        }
        
    }

    public Vector2 createTarget(bool isInside, Vector2 prevTarget){
        Vector2 target;
        float screenAspect = (float)Screen.width/(float)Screen.height;
        float innerRadius = mainCam.orthographicSize;
        float outerRadius = mainCam.orthographicSize*screenAspect*2;
        Vector2 cameraPos = mainCam.transform.position;
        Vector2 cameraCenter = cameraPos + new Vector2(innerRadius,outerRadius/2);
        if(isInside){//this makes a random target outside of the range of the camera view
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

    public List<Flock> randomFlock(int numflocks){
        List <Flock> flocks = new List<Flock>();
        int b = 0;
        while(b < numflocks){
        int i = Random.Range(0,FlockPool.Count);
        Flock p;
        int value = Random.Range(minFlockVal, maxFlockVal);
        float pick = Random.Range(0.0f,1.0f) + depth;
        if(depth<FlockPool[i].spawnChance&&pick<FlockPool[i].spawnChance&&FlockPool[i].spawnChance<depth+1){
            b++;
            int j = Random.Range(0,FlockPool[i].flockList.Count);
            p = Instantiate(FlockPool[i].flockList[j]);
            p.createByValue(value*FlockPool[i].valueMult,maxFlockSize);
            flocks.Add(p);
        }
        }
        return flocks;
    }

    public void removeFlock(int index){
        activePool[index].DestroyAll(); //In case at the same time the flock creates an agent, destroys all agents in the flock and the flock itself, then removes the flock
        activePool.RemoveAt(index);

    }

    public void refresh(){
        int i = 0;
        while(i<activePool.Count){
            removeFlock(i);
        }

    }
}
