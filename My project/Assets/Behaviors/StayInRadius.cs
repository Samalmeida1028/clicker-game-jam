using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/StayInRadius")]
public class StayInRadius : FlockBehavior
{
    public Vector2 center;
    public float radius = 500f;
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
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
}
