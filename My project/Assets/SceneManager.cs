using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public GameObject Spawner;
    public GameObject SceneLight;
    public GameObject[] AmbientLights;
    public GameObject background;
    public GameObject waves;
    public GameObject Seaweed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(Spawner.gameObject.GetComponent<Spawner>().Depth){
            case 0:
            break;
            case 1:
            break;
            case 2:
            break;
        }
        
    }
}
