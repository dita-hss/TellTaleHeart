using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Basic Patrolling AI that responds to sound and chases the player (or other specified targets)
/// </summary>
[RequireComponent(typeof(NavMeshAgent), typeof(Eyes), typeof(SoundListener))]
public class BasicEnemyAI : MonoBehaviour
{


    private GameObject curTarget;

    [SerializeField] private float timeBeforeAbandonSound = 2.0f; 


    // Necessary AI components
    private Eyes eyes;
    private SoundListener ears;
    private NavMeshAgent navMeshAgent;
    private Patrol patrol; 



    private void Awake()
    {
        eyes = GetComponent<Eyes>();
        ears = GetComponent<SoundListener>();
        patrol = GetComponent<Patrol>();
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (curState.Equals(States.PATROL))
        {
            patrol.PatrolStep();

        }
        else if (curState.Equals(States.HEARSOUND))
        {
            //patrol.OverrideDestination(curTarget.transform.position);

            if (patrol.ReachedDestination())
            {
                StartCoroutine(WaitOnHear());
            }
        }
        else if (curState.Equals(States.ATTACK_TARGET))
        {
            patrol.OverrideDestination(curTarget.transform.position);
        }



    }

    
    IEnumerator WaitOnHear()
    {
        yield return new WaitForSeconds(timeBeforeAbandonSound);
        StartPatrol();
        yield return null; 
    }

    


    private void StartAttackTarget(GameObject newTarget)
    {
        print("Attack target start");
        curState = States.ATTACK_TARGET;
        curTarget = newTarget;
        patrol.OverrideDestination(newTarget.transform.position);
    }

    private void StartHearSound(Vector3 soundSource)
    {
        curState = States.HEARSOUND;
        // curTarget = soundSource;
        patrol.OverrideDestination(soundSource);
    }

    private void StartPatrol()
    {
        curState = States.PATROL;
    }


}
