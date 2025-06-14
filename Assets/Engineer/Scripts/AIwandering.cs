using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
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
    [HideInInspector] public PlayerMovement _playerMovement;

    [HideInInspector] public Collider BlowrangeColl;

    [HideInInspector] public BlowingScript _blowScript;

    public MeshRenderer mesh;

    public bool BoostGiven;
    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        agent = GetComponent<NavMeshAgent>();
        wanderTime = Random.Range(minWanderTime, maxWanderTime);
    }

    public virtual void Update()
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
            _playerMovement = other.GetComponentInParent<PlayerMovement>();
            BlowrangeColl = other.GetComponentInChildren<BoxCollider>();
            _playerInput = other.GetComponent<PlayerInput>();
            _blowScript = other.GetComponentInChildren<BlowingScript>();
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

    public virtual void GiveBoost()
    {
        if (BoostGiven)
        {
            return;
        }
    }

}