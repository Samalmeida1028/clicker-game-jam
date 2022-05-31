using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public GameObject boid;
    public int count;
    public int max;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(boid);
        count = 0;
        for(int i = 0; i<max;i++){
            GameObject b = Instantiate(boid);
            b.transform.position = new Vector3(Random.Range(-5f,5f),Random.Range(-5f,5f),0);
            b.GetComponent<Boid>().boundaryX = 5;
            b.GetComponent<Boid>().boundaryY = 5;
            b.GetComponent<Boid>().boundaryXm = -5;
            b.GetComponent<Boid>().boundaryYm = -5;
        }
        
    }
}
