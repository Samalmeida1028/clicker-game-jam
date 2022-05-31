using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCohesion")]
public class SteeredCohesion : FilteredFlockBehavior
{
    Vector2 currentVel;
    public float agentSmoothTime = .5f;
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count==0){
            return Vector2.zero;
        }
        else{
            Vector2 cohesionMove = Vector2.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent,context);
            foreach(Transform i in filteredContext){
                float randX = Random.Range(-.8f, .8f);
                float randY = Random.Range(-.8f, .8f);
                cohesionMove += (Vector2)i.position+ new Vector2(randX,randY);
            }
            cohesionMove /= filteredContext.Count;

            cohesionMove -= (Vector2)agent.transform.position;
            if (float.IsNaN(currentVel.x) || float.IsNaN(currentVel.y)) currentVel = Vector2.zero;
            cohesionMove = Vector2.SmoothDamp(agent.transform.up,cohesionMove, ref currentVel,agentSmoothTime); // this gets rid of jitteryness by smoothing the transition between
                                                                                                                                    //cohesion vectors
            return cohesionMove;
        }
    }
    

}
