using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockManager : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public int agentCount = 20;
    public List<FlockAgent> agents = new List<FlockAgent>();

    public float neighborRadius = 2f;
    public float cohesionWeight = 1f;
    public float alignmentWeight = 1f;
    public float separationWeight = 1.5f;


    void Start()
    {
        for (int i = 0; i < agentCount; i++)
        {
            var agent = Instantiate(agentPrefab, Random.insideUnitCircle * 5f, Quaternion.identity);
            agents.Add(agent);
        }
    }

    void Update()
    {
        foreach (var agent in agents)
        {
            Vector2 move = CalculateMove(agent);
            agent.Move(move);
        }
    }

    Vector2 CalculateMove(FlockAgent agent)
    {
        Vector2 cohesion = Vector2.zero;
        Vector2 alignment = Vector2.zero;
        Vector2 separation = Vector2.zero;
        int count = 0;

        foreach (var other in agents)
        {
            if (other == agent) continue;
            float dist = Vector2.Distance(agent.transform.position, other.transform.position);
            if (dist < neighborRadius)
            {
                cohesion += (Vector2)other.transform.position;
                alignment += other.velocity;
                separation += (Vector2)(agent.transform.position - other.transform.position) / dist;
                count++;
            }
        }

        if (count > 0)
        {
            cohesion = (cohesion / count - (Vector2)agent.transform.position) * cohesionWeight;
            alignment = (alignment / count).normalized * alignmentWeight;
            separation = separation * separationWeight;
        }

        return cohesion + alignment + separation;
    }
}
