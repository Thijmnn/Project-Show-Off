using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Wander : MonoBehaviour
{
    public float wanderRadius = 10f;
    public float minWanderTime = 3f;
    public float maxWanderTime = 7f;
    private NavMeshAgent agent;
    private float wanderTimer;
    private float wanderTime;

    bool playerInRange;

    PlayerInput _playerInput;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        wanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    void Update()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderTime && !playerInRange)
        {
            SetNewDestination();
        }

        if (agent.remainingDistance <= agent.stoppingDistance && !playerInRange)
        {
            SetNewDestination();
        }

        if(playerInRange && _playerInput.actions["Interact"].triggered)
        {
            InteractWithPlayer();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>())
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerMovement>())
        {
            playerInRange = false;
        }
    }

    private void InteractWithPlayer()
    {
        //INTERACT
    }

}