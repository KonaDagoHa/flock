using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior
{
    /// <summary>
    /// Find average of all alignments of neighbors in agent's context and return direction vector to align the agent
    /// </summary>
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // if no neighbors, maintain current alignment
        if (context.Count == 0) { return agent.transform.up; }

        // add all neighbor alignments together and find average
        Vector2 alignmentMove = Vector2.zero;

        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        alignmentMove /= context.Count;

        return alignmentMove;
    }
}
