using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boid : MonoBehaviour
{

    public List<GameObject> boidNeighbors = new List<GameObject>();
    public Vector3 groupCenter = new Vector3();
    public Vector3 groupDirection= new Vector3();
    public Vector3 awayDirection= new Vector3(0,0,0);
    public Vector3 moveDirection;
    public float centerStrength;
    public float alignStrength;
    public float minDistance = .5f;

    public float boundaryX, boundaryXm;
    
    public float boundaryY, boundaryYm;
    public int checkInterval =10;
    int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        //centerStrength *= Time.fixedDeltaTime;
        //alignStrength *= Time.fixedDeltaTime;
        moveDirection = new Vector3(Random.Range(-.5f,.5f),Random.Range(-.5f,.5f),0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        gameObject.GetComponent<Rigidbody2D>().velocity=moveDirection;
        int length = boidNeighbors.Count;

        if(length > 0){
         foreach(GameObject otherBoid in boidNeighbors){
            groupCenter += otherBoid.gameObject.transform.position;
            groupDirection += otherBoid.gameObject.GetComponent<Boid>().moveDirection;
            if(Vector3.Magnitude(otherBoid.gameObject.transform.position)<minDistance){
                awayDirection-=(otherBoid.gameObject.transform.position-gameObject.transform.position);
            }

        }
        groupCenter /= (float)length;
        groupDirection /=(float)length;
        }
        moveDirection = moveDirection.normalized + groupCenter.normalized*centerStrength + groupDirection.normalized*alignStrength + awayDirection.normalized;

        
    }

    void OnTriggerEnter2D(Collider2D oth){
        if(oth.gameObject.tag == "boid"){
            boidNeighbors.Add(oth.gameObject);

        }
    }
        void OnTriggerExit2D(Collider2D oth){
        if(oth.gameObject.tag == "boid"){
            boidNeighbors.Remove(oth.gameObject);

        }
    }
}
