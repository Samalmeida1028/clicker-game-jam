using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    [SerializeField]public string FlockName;
    public FlockAgent agentPrefab;
    public List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;
    [Range(10,1000)]
    public int startingCount = 250;
    public float AgentDensity = 0.08f;
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
    public float radius = 10f;

    public float respawnTime = 10;

    public int flockvalue = 0;

    public int minAgentVal;
    public int maxAgentVal;
    public float threshold = .2f;

    float flockHasPassed = 0;


    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public Vector2 center = new Vector2(0,0);
    public float SquareAvoidanceRadius {get{return squareAvoidanceRadius;}}
    public Camera mainCam;
    public Vector2 target = new Vector2(0,0);
    Vector2 targetOffset = new Vector2(0,0);
    bool flockIsClose = true;

    void Start()
    {
        squareMaxSpeed = maximumSpeed*maximumSpeed;
        squareNeighborRadius = neighbourRadius*neighbourRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidRangeMult * avoidRangeMult;
    }

    public void createByValue(int maxvalue, int maxFlockSize){
        while(flockvalue < maxvalue&&agents.Count<maxFlockSize){
            FlockAgent newagent = Instantiate(agentPrefab,Random.insideUnitCircle*startingCount*AgentDensity, Quaternion.Euler(Vector3.forward*Random.Range(0f,360f)),transform);
            newagent.Initialize(this);
            newagent.setValue(minAgentVal,maxAgentVal);
            newagent.name= "Agent " + flockvalue;
            agents.Add(newagent);
            flockvalue+= newagent.value;
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
        while(i < agents.Count){
            Transform agent = agents[i].transform;
            if(!agents[i].isClicked){
                List<Transform> context = GetNearbyObjects(agents[i]);
                Vector3 view = mainCam.WorldToViewportPoint(agent.position);
                if(view.x < 1&&view.y < 1&&view.x > 0&&view.y > 0){
                    if(!agents[i].passed){
                    agents[i].passed = true;
                    agents[i].addPass();
                    }
                    flockHasPassed +=1;
                }
                else if(!(view.x < 1&&view.y < 1&&view.x > 0&&view.y > 0)){
                    agents[i].passed = false;
                }

                Vector2 move = (Vector2)agent.forward+behavior.calculateMove(agents[i],context,this) + targetOff(agents[i]);
                move *= driveFactor;
                if(move.sqrMagnitude>squareMaxSpeed){
                    move = move.normalized*maximumSpeed;
                }
                agents[i].Move(move);
                if(agents[i].passnum > agents[i].maxPasses&&!agents[i].passed){
                    agents[i].Desty();
                    agents.RemoveAt(i);
            }
            }
            else if (agents[i].isClicked){
                Vector2 move = new Vector2(0,0);
            }
            i++;
        }
        flockHasPassed/= agents.Count;
            if(agents.Count < startingCount && count > respawnTime){
                count = 0;
                FlockAgent newagent = Instantiate(agentPrefab,Random.insideUnitCircle*startingCount*10*AgentDensity, Quaternion.Euler(Vector3.forward*5*Random.Range(0f,360f)),transform);
                newagent.Initialize(this);
                newagent.setValue(minAgentVal,maxAgentVal);
                newagent.name= "Agent " + i + count;
                agents.Add(newagent);
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

    public Vector2 targetOff(FlockAgent agent)
    {
        Vector2 pos = (Vector2)agent.transform.position;
        targetOffset = target - pos;
        Debug.DrawLine(pos,target,Color.green,.1f,true);
        float t = targetOffset.magnitude/radius;

        if(t<=0.1){
        Debug.DrawLine(pos,target,Color.red,.1f,true);
        flockIsClose = true;
        return Vector2.zero; 
        }
        else{
            Vector2 currentVel = agent.transform.up;
            if (float.IsNaN(currentVel.x) || float.IsNaN(currentVel.y)) currentVel = Vector2.zero;
            return  Vector2.SmoothDamp(agent.transform.up,targetOffset*t, ref currentVel,.5f);
        }
    }

    public void setCenter(Vector2 center1){
        center = center1;
    }

    public void setTarget(Vector2 targetSet){
        target = targetSet;
    }

    public bool isClose(){
        if(flockIsClose){
            flockIsClose = false;
            return true;
        }
        else{
            return false;
        }
    }

    public bool wasInside(){
        if(flockHasPassed>.5){
            flockHasPassed = 0;
            return true;
        }
        else{
            return false;
        }
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
