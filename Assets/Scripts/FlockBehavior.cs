using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehavior : ScriptableObject
{
    /// <summary>
    /// Calculate velocity vector of agent
    /// </summary>
    /// <param name="agent">Agent to move</param>
    /// <param name="context">List of transforms belonging to agent's neighboring agents</param>
    /// <param name="flock">Flock that agent belongs to</param>
    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
