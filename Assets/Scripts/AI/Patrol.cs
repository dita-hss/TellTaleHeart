using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/// <summary>
/// Patrol and movement behavior
/// Basically a patrol wrapper class for Unity's Nav Mesh Agents
/// Run PatrolStep() in update from any class for object to move between patrol points
/// OverrideDestination() for it to move where ever else 
/// </summary>

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;

    private int curPatrolPoint = 0;
    private Vector3 curDestination = Vector3.zero;
    private bool onPatrol = true; 
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        curDestination = patrolPoints[curPatrolPoint].position;
    }

    public void Stop()
    {
        onPatrol = false; 
        navMeshAgent.isStopped = true;
    }

    public void PatrolStep()
    {
        // Set the destination back to the current patrol point if it went off track
        if (!onPatrol)
        {
            onPatrol = true;
            navMeshAgent.isStopped = false;

            navMeshAgent.SetDestination(patrolPoints[curPatrolPoint].position);
            curDestination = patrolPoints[curPatrolPoint].position;
        }

        // Set the destination back if the nav mesh agent gets messed up
        if (!navMeshAgent.destination.Equals(curDestination))
        {
            navMeshAgent.SetDestination(curDestination);
        }

        // Once destination is reached, go to the next patrol point
        if (ReachedDestination())
        {
            print("Destination reached");
            curPatrolPoint = (curPatrolPoint + 1) % patrolPoints.Length;
            curDestination = patrolPoints[curPatrolPoint].position;
            navMeshAgent.SetDestination(curDestination);
        }
    }

    /// <summary>
    /// Override the patrol points
    /// </summary>
    /// <param name="newDest">New position to go to</param>
    public void OverrideDestination(Vector3 newDest)
    {
        onPatrol = false;
        navMeshAgent.isStopped = false; 
        navMeshAgent.SetDestination(newDest);
        curDestination = newDest;

    }

    // Thx https://answers.unity.com/questions/324589/how-can-i-tell-when-a-navmesh-has-reached-its-dest.html
    /// <summary>
    /// Checks if NavMeshAgent reached the destination
    /// </summary>
    /// <returns>Boolean, if true, its reached the destination, false if not</returns>
    public bool ReachedDestination()
    {
        return (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f));
    }
}
