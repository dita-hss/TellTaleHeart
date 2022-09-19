using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    private FSMState[] states;
    private FSMState currentState;
    private FSMState startingState; 

    

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



public class FSMState
{
    private Dictionary<FSMState, FSMTransition> transitions = new Dictionary<FSMState, FSMTransition>();



}

public class FSMTransition
{

}

