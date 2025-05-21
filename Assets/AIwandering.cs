using UnityEngine;
using UnityEngine.AI;

public class Wander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float minWanderTime = 3f;
    public float maxWanderTime = 7f;
    private NavMeshAgent agent;
    private float wanderTimer;
    private float wanderTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    void Update()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderTime)
        {
            SetNewDestination();
        }

        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, wanderRadius, agent.areaMask))
        {
            agent.SetDestination(hit.position);
        }

        wanderTime = Random.Range(minWanderTime, maxWanderTime);
        wanderTimer = 0;
    }
}