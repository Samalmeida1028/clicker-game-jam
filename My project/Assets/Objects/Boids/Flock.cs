using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    [SerializeField]
    public string FlockName;

    public FlockAgent agentPrefab;

    public List<FlockAgent> agents = new List<FlockAgent>();

    public FlockBehavior behavior;

    [Range(10, 1000)]
    public int startingCount = 250;

    public float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10;

    [Range(1f, 100f)]
    public float maximumSpeed = 5;

    [Range(1f, 10f)]
    public float neighbourRadius = 10;

    [Range(0f, 1f)]
    public float avoidRangeMult = 0.5f;

    [Range(1, 1000)]
    public float maxNeighbors = 200;

    [Range(1, 1000)]
    public int updateNeighbors;

    public int rarity;



    public float count = 0;

    public float radius = 10f;

    public float respawnTime = 10;

    public int flockvalue = 0;

    public int minAgentVal;

    public int maxAgentVal;

    public float threshold = .2f;

    float flockHasonScreen = 0;

    float squareMaxSpeed;

    float squareNeighborRadius;

    float squareAvoidanceRadius;

    public Vector2 center = new Vector2(0, 0);

    public float SquareAvoidanceRadius
    {
        get
        {
            return squareAvoidanceRadius;
        }
    }

    public Camera mainCam;

    public Vector2 target = new Vector2(0, 0);

    Vector2 targetOffset = new Vector2(0, 0);

    bool flockIsClose = true;

    void Start()
    {
        squareMaxSpeed = maximumSpeed * maximumSpeed;
        squareNeighborRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius =
        squareNeighborRadius * avoidRangeMult * avoidRangeMult;
        mainCam = Camera.main;
    }

    public void createByValue(int minvalue, int maxvalue, int maxFlockSize)
    {
        maxAgentVal = maxvalue;
        while (flockvalue < maxvalue && agents.Count < maxFlockSize)
        {
            FlockAgent newagent =
                Instantiate(agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity * 10,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform);
            newagent.Initialize(this);
            newagent.setValue(minAgentVal, maxAgentVal*rarity);
            newagent.name = "Agent " + flockvalue;
            agents.Add (newagent);
            flockvalue += newagent.value;
        }
        startingCount = agents.Count;
    }

    void FixedUpdate()
    {
        count += Time.fixedDeltaTime;
        int i = 0;
        while (i < agents.Count&&!flockIsClose)
        {
            Transform agent = agents[i].transform;
            List<Transform> context = GetNearbyObjects(agents[i]);
            Vector3 view = mainCam.WorldToViewportPoint(agent.position);
            if (view.x < 1 && view.y < 1 && view.x > 0 && view.y > 0)
            {
                if (!agents[i].onScreen)
                {
                    agents[i].onScreen = true;
                    agents[i].addPass();
                }
                flockHasonScreen += 1;
            }
            else if (!(view.x < 1 && view.y < 1 && view.x > 0 && view.y > 0))
            {
                agents[i].onScreen = false;
            }

            Debug.DrawLine(agents[i].transform.position,target,Color.green);
            bool close = agentIsClose(agents[i], target, .1f);
            Vector2 offset;

            if (close)
            {
                Debug.DrawLine(agents[i].transform.position, target, Color.red);
                flockIsClose = true;
                offset = Vector2.zero;
            }
            else
            {
                offset = targetOff(agents[i], target);
            }
            Vector2 move;
            if (!agents[i].isHooked)
            {
                move =
                    (Vector2) agent.forward +
                    behavior.calculateMove(agents[i], context, this) +
                    offset;
            }
            else
            {
                //move = agents[i].Escape(target);
                move = Vector2.zero;
            }
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maximumSpeed;
            }

            agents[i].Move(move);

            if (
                agents[i].passnum > agents[i].maxPasses &&
                !agents[i].onScreen &&
                flockIsClose
            )
            {
                if (agents.Count == startingCount)
                {
                    count = 0;
                }
                agents[i].Desty();
                agents.RemoveAt (i);
            }
            i++;
        }

        flockHasonScreen /= agents.Count;
        if (agents.Count < startingCount && count > respawnTime)
        {
            count = 0;
            FlockAgent newagent =
                Instantiate(agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity * 10,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform);
            newagent.Initialize(this);
            newagent.setValue (minAgentVal, maxAgentVal);
            newagent.name = "Agent " + i + count;
            agents.Add (newagent);
        }
    }



    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders =
            Physics2D
                .OverlapCircleAll(agent.transform.position, neighbourRadius);
        foreach (Collider2D collider in contextColliders)
        {
            if (collider != agent.AgentCollider && context.Count < maxNeighbors)
            {
                context.Add(collider.transform);
            }
        }
        return context;
    }


    public Vector2 targetOff(FlockAgent agent, Vector2 targetPos)
    {
        Vector2 pos = (Vector2) agent.transform.position;
        Vector2 targetPosOffset = targetPos - pos;
        float t = targetPosOffset.magnitude / radius;

        if (
            agent.transform.rotation.eulerAngles.z > 0 &&
            agent.transform.rotation.eulerAngles.z < 180
        )
        {
            agent.GetComponentInChildren<SpriteRenderer>().flipX = true;
            agent.isFlipped = true;
        }
        else
        {
            agent.GetComponentInChildren<SpriteRenderer>().flipX = false;
            agent.isFlipped = false;
        }
        Vector2 currentVel = agent.transform.up;
        if (float.IsNaN(currentVel.x) || float.IsNaN(currentVel.y))
            currentVel = Vector2.zero;
        return Vector2
            .SmoothDamp(agent.transform.up,
            targetPosOffset * t,
            ref currentVel,
            .5f);
    }

    public bool agentIsClose(FlockAgent agent, Vector2 targetPos, float threshold)
    {
        Vector2 pos = (Vector2) agent.transform.position;
        Vector2 targetPosOffset = targetPos - pos;
        float t = targetPosOffset.magnitude / radius;
        if (t <= threshold)
        {
            return true;
        }

        return false;
    }



    public void setCenter(Vector2 center1)
    {
        center = center1;
    }


    public void setTarget(Vector2 targetSet)
    {
        target = targetSet;
    }


    public bool isClose()
    {
        if (flockIsClose)
        {
            flockIsClose = false;
            return true;
        }
        else
        {
            return false;
        }
    }


    public bool wasInside()
    {
        if (flockHasonScreen > .5)
        {
            flockHasonScreen = 0;
            return true;
        }
        else
        {
            return false;
        }
    }


    public void DestroyAll()
    {
        int i = 0;
        while (i < agents.Count)
        {
            agents[0].Desty();
            agents.RemoveAt(0);
            i++;
        }
        GameObject.Destroy (gameObject);
    }


    public void removeAgent(FlockAgent agent)
    {
        agents.Remove(agent);
    }
}
