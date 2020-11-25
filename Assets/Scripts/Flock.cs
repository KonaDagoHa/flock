using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount = 250;
    [Range(1f, 100f)]
    public float driveFactor = 10;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    private List<FlockAgent> agents = new List<FlockAgent>();
    private const float agentDensity = 0.08f;

    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;

    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0, 360)),
                transform
            );
            newAgent.name = "Agent_" + i;
            agents.Add(newAgent);
        }
    }

    private void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            // calculate move velocity vector
            Vector2 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;

            // limit move speed to maxSpeed
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            // if collider does not belong to current agent
            if (c != agent.AgentCollider)
            {
                // add collider's transform to context
                context.Add(c.transform);
            }
        }
        return context;
    }
}
