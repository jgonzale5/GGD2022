using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ChasingScript : MonoBehaviour
{

    //This is the component that controls the enemy's movement
    private NavMeshAgent agent;

    //The animator controlling the enemy animations
    private Animator animator;

    public enum State { Idle = 0, Patrolling = 1, Attacking = 2, Searching = 3, Fleeing = 4, Help = 5 };
    //The current state of the enemy, which determines their behavior at that time
    private State currentState = State.Idle;

    [Header("Idle")]
    public float idleTime = 1;
    private float idleTimeCounter = 0;

    [Header("Patrol")]
    private Vector3 patrollingDestination;
    public float minDestinationDistance;
    public float patrolRange = 10;

    [Header("Searching")]
    private Vector3 searchingDestination;

    [Header("Combat")]
    //The transform this enemy is gonna chase
    public Transform target;
    //How many seconds should pass between each nav update
    public float updateFrequency = 0.1f;
    //The counter keeping track of time since the last nav update
    private float updateCounter = 0;
    //
    public float minCombatDistance = 1;

    [Header("Spawn")]
    public Transform particle;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        animator.SetInteger("State", (int)currentState);

        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Patrolling:
                Patrolling();
                break;
            case State.Attacking:
                Attacking();
                break;
            case State.Searching:
                Searching();
                break;
            case State.Fleeing:
            case State.Help:
            default:
                Debug.Log("Doing nothing");
                break;
        }       

    }

    void Idle()
    {
        if (idleTimeCounter >= idleTime)
        {
            currentState = State.Patrolling;
            patrollingDestination = this.transform.position +
            new Vector3(
                Random.Range(-patrolRange, patrolRange), 
                0, 
                Random.Range(-patrolRange, patrolRange));
            agent.SetDestination(patrollingDestination);
            idleTimeCounter = 0;
        }
        else
        {
            idleTimeCounter += Time.deltaTime;
        }

        if (Vector3.Distance(target.position, this.transform.position) <= minCombatDistance)
        {
            currentState = State.Attacking;
        }
    }

    void Patrolling()
    {
        if (Vector3.Distance(target.position, this.transform.position) 
            <= minCombatDistance)
        {
            currentState = State.Attacking;
        }
        else if (Vector3.Distance(patrollingDestination, this.transform.position) 
            <= minDestinationDistance)
        {
            currentState = State.Idle;
        }        
    }

    void Attacking()
    {
        if (updateCounter >= updateFrequency)
        {
            agent.SetDestination(target.position);
            updateCounter = 0;
        }
        else
        {
            updateCounter += Time.deltaTime;
        }
    }

    void Searching()
    {
        if (Vector3.Distance(target.position, this.transform.position) <= minCombatDistance)
        {
            currentState = State.Attacking;
        }
    }
}
