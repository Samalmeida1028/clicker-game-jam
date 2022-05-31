using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/DooDoo")]
public class Cohesion : FilteredFlockBehavior
{
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count==0){
            return Vector2.zero;
        }
        else{
            Vector2 cohesionMove = Vector2.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent,context);
            foreach(Transform i in filteredContext){
                cohesionMove += (Vector2)i.position;
            }
            cohesionMove /= filteredContext.Count;

            cohesionMove -= (Vector2)agent.transform.position;
            return cohesionMove;
        }
    }

}
