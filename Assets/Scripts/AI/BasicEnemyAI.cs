using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent), typeof(Eyes), typeof(SoundListener))]
public class BasicEnemyAI : MonoBehaviour
{

    [SerializeField] private Transform[] patrolPoints;
    private int curPatrolPoint = 0;
    private Transform curDestination = null;
    private GameObject curTarget;

    [SerializeField] private float timeBeforeAbandonSound = 2.0f; 



    private Eyes eyes;
    private SoundListener ears;
    private NavMeshAgent navMeshAgent;



    private void Awake()
    {
        eyes = GetComponent<Eyes>();
        ears = GetComponent<SoundListener>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private States curState = States.PATROL;

    private Vector3 lastSoundLocation;

    enum States
    {
        PATROL = 0,
        HEARSOUND = 1,
        ATTACK_TARGET = 2,
    }

    // Start is called before the first frame update
    void Start()
    {
        eyes.onSeeTarget.AddListener(StartAttackTarget);
        ears.AddOnSoundHeardAction(StartHearSound);
        curDestination = patrolPoints[curPatrolPoint];
    }

    // Update is called once per frame
    void Update()
    {
        
        if (curState.Equals(States.PATROL))
        {
            if (!navMeshAgent.destination.Equals(curDestination.position))
            {
                navMeshAgent.SetDestination(curDestination.position);
            }

            
            
            if (ReachedDestination())
            {
                print("Destination reached");
                curPatrolPoint = (curPatrolPoint + 1) % patrolPoints.Length;
                curDestination = patrolPoints[curPatrolPoint];
                navMeshAgent.SetDestination(curDestination.position);
            }

        }
        else if (curState.Equals(States.HEARSOUND))
        {
            navMeshAgent.SetDestination(curTarget.transform.position);
            curDestination = curTarget.transform;
        }
        else if (curState.Equals(States.ATTACK_TARGET))
        {
            navMeshAgent.SetDestination(curTarget.transform.position);
            curDestination = curTarget.transform;
        }



    }


    public bool ReachedDestination()
    {
        return (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance && (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f));
    }


    private void StartAttackTarget(GameObject newTarget)
    {
        print("Attack target start");
        curState = States.ATTACK_TARGET;
        curTarget = newTarget;
        navMeshAgent.SetDestination(newTarget.transform.position);
        curDestination = newTarget.transform;
    }

    private void StartHearSound(GameObject soundSource)
    {
        curState = States.HEARSOUND;
        curTarget = soundSource;
        navMeshAgent.SetDestination(soundSource.transform.position);
        curDestination = soundSource.transform;
    }

    private void StartPatrol(GameObject soundSource)
    {
        curState = States.PATROL;
        navMeshAgent.SetDestination(patrolPoints[curPatrolPoint].position);
    }


}
