using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FlockBehavior
{
    /// <summary>
    /// Find average vector of all vectors from agent to neighbors in agent's context within flock's avoidance radius and return that vector
    /// </summary>
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, return Vector2.zero
        if (context.Count == 0) { return Vector2.zero; }

        // add all neighbor positions together and find average
        Vector2 avoidanceMove = Vector2.zero;

        foreach (Transform item in context)
        {
            // if the distance between neighbor and agent is less than flock's SquareAvoidanceRadius
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                // move away from neighbor
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        avoidanceMove /= context.Count;

        return avoidanceMove;

    }
}
