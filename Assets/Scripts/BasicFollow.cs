using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BasicFollow : MonoBehaviour
{

    public GameObject follow;
    private NavMeshAgent agent; 
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(follow.transform.position);
    }
}
