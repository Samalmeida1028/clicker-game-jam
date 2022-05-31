using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{


    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    [Range(10,1000)]
    public int startingCount = 250;
    const float AgentDensity = 0.08f;
    [Range(1f,100f)]
    public float driveFactor = 10;
    [Range(1f,100f)]
    public float maximumSpeed = 5;
    [Range(1f,10f)]
    public float neighbourRadius = 10;
    [Range(0f,1f)]
    public float avoidRangeMult = 0.5f;
    [Range(1,1000)]
    public float maxNeighbors = 200;
    [Range(1,1000)]
    public int updateNeighbors;
    int count = 1;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius {get{return squareAvoidanceRadius;}}

    void Start()
    {
        squareMaxSpeed = maximumSpeed*maximumSpeed;
        squareNeighborRadius = neighbourRadius*neighbourRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidRangeMult * avoidRangeMult;
        for( int i = 0; i < startingCount; i++){
            FlockAgent newagent = Instantiate(agentPrefab,Random.insideUnitCircle*startingCount*AgentDensity, Quaternion.Euler(Vector3.forward*Random.Range(0f,360f)),transform);
            newagent.Initialize(this);
            newagent.name= "Agent " + i;
            agents.Add(newagent);
        }
    }

    void FixedUpdate()
    {
        maxNeighbors = maxNeighbors;
        maximumSpeed = maximumSpeed;
        neighbourRadius = neighbourRadius;
        avoidRangeMult = avoidRangeMult;
        driveFactor = driveFactor;
        count++;
        if(count>=updateNeighbors){
        foreach(FlockAgent agent in agents){
            List<Transform> context = GetNearbyObjects(agent);
            //agent.GetComponentInChildren<SpriteRenderer>().color= Color.Lerp(Color.white,Color.red,context.Count/maxNeighbors);
            Vector2 move = behavior.calculateMove(agent,context,this);
            move *= driveFactor;
            if(move.sqrMagnitude>squareMaxSpeed){
                move = move.normalized*maximumSpeed;
            }
            agent.Move(move);
            count = 0;  
        }
        }
        else{
            foreach(FlockAgent agent in agents){
                agent.Move(agent.transform.up*driveFactor);
            }
                
            }


    }

    List<Transform> GetNearbyObjects(FlockAgent agent){
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position,neighbourRadius);
        foreach(Collider2D collider in contextColliders){
            if(collider != agent.AgentCollider&&context.Count<maxNeighbors){
                context.Add(collider.transform);
            }
        }
        return context;
    }
}
