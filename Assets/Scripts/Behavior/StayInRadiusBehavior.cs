using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        // t is close to 0 => agent is near center; t is close to radius => agent is far from center
        float t = centerOffset.magnitude / radius;

        // if agent is close to center
        if (t < 0.9f)
        {
            // don't adjust agent's movement
            return Vector2.zero;
        }

        // else move agent back towards center (higher t => higher magnitude of move vector)
        return centerOffset * t * t;
    }
}
