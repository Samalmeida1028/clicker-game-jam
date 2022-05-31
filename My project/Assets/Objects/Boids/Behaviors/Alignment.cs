using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class Alignment : FilteredFlockBehavior
{
    // Start is called before the first frame update
  public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count==0){
            return agent.transform.up;
        }
        else{
            Vector2 alignmentMove = Vector2.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent,context);
            foreach(Transform i in filteredContext){
                alignmentMove += (Vector2)i.transform.up;
            }
            alignmentMove /= filteredContext.Count;

            return alignmentMove;
        }
    }
}
