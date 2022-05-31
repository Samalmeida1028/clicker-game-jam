using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{


    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
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
    public float count = 0;
    public float radius = 100f;

    public float respawnTime = 10;

    public int value = 0;

    public int minAgentVal;
    public int maxAgentVal;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public Vector2 center = new Vector2(0,0);
    public float SquareAvoidanceRadius {get{return squareAvoidanceRadius;}}
    public Camera mainCam;

    void Start()
    {
        squareMaxSpeed = maximumSpeed*maximumSpeed;
        squareNeighborRadius = neighbourRadius*neighbourRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidRangeMult * avoidRangeMult;
    }

    public void createByValue(int maxvalue){
        while(value < maxvalue){
            FlockAgent newagent = Instantiate(agentPrefab,Random.insideUnitCircle*startingCount*AgentDensity, Quaternion.Euler(Vector3.forward*Random.Range(0f,360f)),transform);
            newagent.Initialize(this);
            newagent.setValue(minAgentVal,maxAgentVal);
            newagent.name= "Agent " + value;
            agents.Add(newagent);
            value+= newagent.value;
        }
        startingCount = agents.Count;

    }

    void FixedUpdate()
    {
        maxNeighbors = maxNeighbors;
        maximumSpeed = maximumSpeed;
        neighbourRadius = neighbourRadius;
        avoidRangeMult = avoidRangeMult;
        driveFactor = driveFactor;
        count+=Time.fixedDeltaTime;
        int i = 0;
        if(!agents[i].isClicked){
        while(i < agents.Count){
            List<Transform> context = GetNearbyObjects(agents[i]);
            Vector3 view = mainCam.WorldToViewportPoint(agents[i].gameObject.transform.position);
            if(view.x < 1&&view.y < 1&&view.x > 0&&view.y > 0&&!agents[i].passed){
                agents[i].passed = true;
                agents[i].addPass();
            }
            else if(!(view.x < 1&&view.y < 1&&view.x > 0&&view.y > 0)){
                agents[i].passed = false;
            }
            Vector2 move = behavior.calculateMove(agents[i],context,this) + centerOffset(agents[i]);
            move *= driveFactor;
            if(move.sqrMagnitude>squareMaxSpeed){
                move = move.normalized*maximumSpeed;
            }
            agents[i].Move(move);
            if(agents[i].passnum > agents[i].maxPasses&&!agents[i].passed){
                agents[i].Desty();
                agents.RemoveAt(i);
            }
            i++;
        }
        }
            if(agents.Count < startingCount && count > respawnTime){
                count = 0;
                FlockAgent newagent = Instantiate(agentPrefab,Random.insideUnitCircle*startingCount*AgentDensity, Quaternion.Euler(Vector3.forward*Random.Range(0f,360f)),transform);
                newagent.Initialize(this);
                newagent.name= "Agent " + i + count;
                agents.Add(newagent);
                Debug.Log("hi");
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

    public Vector2 centerOffset(FlockAgent agent)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude/radius;
        if(t<=0.5){
            return Vector2.zero;
        }
        else{
            return centerOffset*t;
        }
    }

    public void setCenter(Vector2 center1){
        center = center1;
    }

    public void DestroyAll(){
        int i = 0;
        while(i<agents.Count){
            agents[0].Desty();
            agents.RemoveAt(0);
            i++;
        }
        GameObject.Destroy(gameObject);
    }
}
