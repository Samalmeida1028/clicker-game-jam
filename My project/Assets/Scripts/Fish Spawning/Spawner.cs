using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public List<Flock> FlockPool;
    public List <Flock> activePool;
    public Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
        foreach(Flock f in FlockPool){
            Flock p = Instantiate(f);
            activePool.Add(p);
            p.mainCam = mainCam;
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
            }
        }
        
    }
}
