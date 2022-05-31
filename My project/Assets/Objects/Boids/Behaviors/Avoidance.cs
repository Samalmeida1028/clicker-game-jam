using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class Avoidance : FilteredFlockBehavior
{
  public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count==0){
            return Vector2.zero;
        }
        else{
            Vector2 avoidanceMove = Vector2.zero;
            int nAvoid = 0;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent,context);
            foreach(Transform i in filteredContext){
                if(Vector2.SqrMagnitude(i.position-agent.transform.position)<flock.SquareAvoidanceRadius){
                    nAvoid++;
                    avoidanceMove += (Vector2)agent.transform.position - (Vector2)i.position;
                }
            }
            if(nAvoid>0){
            avoidanceMove /= nAvoid;
            }

            return avoidanceMove;
        }
    }
}
