using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/SteeredCohesion")]
public class SteeredCohesionBehavior : FlockBehavior
{

    public float agentSmoothTime = 0.5f;
    private Vector2 currentVelocity;

    /// <summary>
    /// Find average position of all neighbors in agent's context and return velocity vector to move agent there
    /// </summary>
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, return Vector2.zero
        if (context.Count == 0) { return Vector2.zero; }

        // add all neighbor positions together and find average
        Vector2 cohesionMove = Vector2.zero;
        foreach (Transform item in context)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        // offset agent's position (turns cohesionMove into a velocity vector rather than just a point in space)
        cohesionMove -= (Vector2)agent.transform.position;
        // smooth out movement vector so that agents stop twitching
        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);

        return cohesionMove;
    }
}
