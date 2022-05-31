using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/CompositeBehavior")]
public class CompositeBehavior : FlockBehavior
{
    public FlockBehavior[] behaviors;
    public float[] weights;
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(weights.Length!=behaviors.Length){
            Debug.Log("Error, data mismatch in " + name, this);
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;

        for (int i = 0; i < behaviors.Length; i++)
        {
            Vector2 partialMove = behaviors[i].calculateMove(agent,context,flock)*weights[i];

            if(partialMove != Vector2.zero){
                if(partialMove.sqrMagnitude>weights[i]*weights[i]){
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                move += partialMove;
            }
        }
        return move;
    }
}
