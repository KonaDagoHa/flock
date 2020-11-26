using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    private Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    private SpriteRenderer agentRenderer;
    public SpriteRenderer AgentRenderer { get { return agentRenderer; } }

    private Color baseColor;
    public Color BaseColor { get { return baseColor; } }

    private Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        agentRenderer = GetComponent<SpriteRenderer>();
        baseColor = agentRenderer.color;
        agentFlock = GetComponentInParent<Flock>();
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
