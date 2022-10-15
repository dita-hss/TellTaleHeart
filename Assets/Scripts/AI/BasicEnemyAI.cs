using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Basic Patrolling AI that responds to sound and chases the player (or other specified targets)
/// </summary>
[RequireComponent(typeof(NavMeshAgent), typeof(Eyes), typeof(SoundListener))]
[RequireComponent(typeof(Attack))]
public class BasicEnemyAI : MonoBehaviour
{


    private GameObject curTarget;

    [SerializeField] private float timeBeforeAbandonSound = 2.0f;
    [SerializeField] private float attackStartTime = 0.5f;
    [SerializeField] private float postAttackTime = 0.25f;

    private bool attacking = false; 


    // Necessary AI components
    private Eyes eyes;
    private SoundListener ears;
    private NavMeshAgent navMeshAgent;
    private Patrol patrol;
    private Attack attack; 



    private void Awake()
    {
        eyes = GetComponent<Eyes>();
        ears = GetComponent<SoundListener>();
        patrol = GetComponent<Patrol>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        attack = GetComponent<Attack>();
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
            if (!attacking)
            {
                patrol.OverrideDestination(curTarget.transform.position);

                if (attack.ObjectInRange(curTarget))
                {
                    StartCoroutine(AttackTarget());
                }
            }
        }



    }

    
    IEnumerator WaitOnHear()
    {
        yield return new WaitForSeconds(timeBeforeAbandonSound);
        StartPatrol();
        yield return null; 
    }


    IEnumerator AttackTarget()
    {
        attacking = true; 
        patrol.Stop();
        yield return new WaitForSeconds(attackStartTime);
        attack.TryAttack(curTarget);
        yield return new WaitForSeconds(postAttackTime);
        attacking = false;
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
        if (curState != States.ATTACK_TARGET)
        {
            curState = States.HEARSOUND;
            // curTarget = soundSource;
            patrol.OverrideDestination(soundSource);
        }
    }

    private void StartPatrol()
    {
        if (curState != States.ATTACK_TARGET)
        {
            curState = States.PATROL;
        }
    }


}
