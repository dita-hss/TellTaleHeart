using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Patrol : MonoBehaviour
{//GenericPropertyJSON:{"name":"patrolPoints","type":-1,"arraySize":2,"arrayType":"PPtr<$Transform>","children":[{"name":"Array","type":-1,"arraySize":2,"arrayType":"PPtr<$Transform>","children":[{"name":"size","type":12,"val":2},{"name":"data","type":5,"val":"UnityEditor.ObjectWrapperJSON:{\"guid\":\"\",\"localId\":0,\"type\":0,\"instanceID\":29056}"},{"name":"data","type":5,"val":"UnityEditor.ObjectWrapperJSON:{\"guid\":\"\",\"localId\":0,\"type\":0,\"instanceID\":29100}"}]}]}

    [SerializeField] private Transform[] patrolPoints;
    private int curPatrolPoint = 0;
    private Vector3 curDestination = Vector3.zero;
    private bool onPatrol = true; 


    NavMeshAgent navMeshAgent;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        curDestination = patrolPoints[curPatrolPoint].position;
    }

    public void PatrolStep()
    {

        if (!onPatrol)
        {
            navMeshAgent.SetDestination(patrolPoints[curPatrolPoint].position);
            curDestination = patrolPoints[curPatrolPoint].position;
            onPatrol = true; 
        }


        if (!navMeshAgent.destination.Equals(curDestination))
        {
            navMeshAgent.SetDestination(curDestination);
        }

        if (ReachedDestination())
        {
            print("Destination reached");
            curPatrolPoint = (curPatrolPoint + 1) % patrolPoints.Length;
            curDestination = patrolPoints[curPatrolPoint].position;
            navMeshAgent.SetDestination(curDestination);
        }
    }

    public void OverrideDestination(Vector3 newDest)
    {
        onPatrol = false; 
        navMeshAgent.SetDestination(newDest);
        curDestination = newDest;

    }

    public bool ReachedDestination()
    {
        return (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f));
    }
}
