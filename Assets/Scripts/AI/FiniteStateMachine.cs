using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    private FSMState[] states;
    private FSMState currentState;
    private FSMState startingState;

    [SerializeField]
    private AIBlackboard blackboard;


    public List<FSMAction> UpdateStateMachine()
    {
        List<FSMAction> returnActions = new List<FSMAction>();

        FSMTransition triggered = null;

        


        foreach (FSMTransition transition in currentState.Transitions)
        {
            if (transition.IsTriggered(blackboard))
            {
                triggered = transition;
                break; 
            }
        }


        // Transition to a new state!
        if (triggered != null)
        {
            FSMState target = triggered.TargetState;

            returnActions.AddRange(currentState.ExitActions);
            returnActions.AddRange(triggered.Actions);
            returnActions.AddRange(target.EntryActions);

            currentState = target;
            return returnActions;
        }
        
        
        return currentState.Actions;

    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class HierarchicalStateMachine : MonoBehaviour
{

}

public class FSMAction
{

}



public class FSMState
{
    //private Dictionary<FSMState, FSMTransition> transitions = new Dictionary<FSMState, FSMTransition>();


    protected List<FSMTransition> transitions = new List<FSMTransition>();
    public ref List<FSMTransition> Transitions { get { return ref transitions; } }


    protected List<FSMAction> actions = new List<FSMAction>(); 
    public ref List<FSMAction> Actions { get { return ref actions; } }


    protected List<FSMAction> entryActions = new List<FSMAction>();
    public ref List<FSMAction> EntryActions { get { return ref entryActions; } }


    protected List<FSMAction> exitActions = new List<FSMAction>();
    public ref List<FSMAction> ExitActions { get { return ref exitActions; } }


}

public abstract class FSMTransition
{
    protected FSMState targetState;
    public ref FSMState TargetState { get { return ref targetState;  } }


    protected List<FSMAction> actions = new List<FSMAction>();
    public ref List<FSMAction> Actions { get { return ref actions; } }

    public abstract bool IsTriggered(AIBlackboard blackboard);
}

