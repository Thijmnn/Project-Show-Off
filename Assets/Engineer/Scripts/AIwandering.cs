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

    private void Awake()
    {
        
        agent = GetComponent<NavMeshAgent>();
        wanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    void Update()
    {
        if (!playerInRange){ Move(); }
        else
        {
            InteractWithPlayer();
        }
            
    }


    private void Move()
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
            _playerInput = other.GetComponent<PlayerInput>();
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
        if (playerInRange && _playerInput.actions["Interact"].triggered)
        {
            GiveBoost();
        }
        agent.SetDestination(transform.position);
        transform.LookAt(_playerInput.transform.position);
    }

    public static void GiveBoost()
    {

    }

}