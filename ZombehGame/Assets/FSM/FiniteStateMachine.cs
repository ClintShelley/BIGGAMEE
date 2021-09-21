using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NPCCode;

public class FiniteStateMachine : MonoBehaviour
{
    AbstractFSMState _currentState;

    [SerializeField]
    List<AbstractFSMState> _validStates;
    Dictionary<FSMStateType, AbstractFSMState> _fsmStates;
    //[SerializeField] GameObject player;
    

    public void Awake()
    {
        _currentState = null;
        _fsmStates = new Dictionary<FSMStateType, AbstractFSMState>();

        NavMeshAgent navMeshAgent = this.GetComponent<NavMeshAgent>();
        NPC npc = this.GetComponent<NPC>();

        foreach(AbstractFSMState state in _validStates)
        {
            state.SetExecutingFSM(this);
            state.SetExecutingNPC(npc);
            state.SetNavMeshAgent(navMeshAgent);
            _fsmStates.Add(state.StateType, state);
        }
    }

    public void Start()
    {
        EnterState(FSMStateType.IDLE);
    }

    public void Update()
    {
        if (_currentState != null)
        {
            _currentState.UpdateState();
        }
    }

    #region STATE MANAGEMENT

    public void EnterState(AbstractFSMState nextState)
    {
        if (nextState == null)
        {
            return;
        }

        if(_currentState != null)
        {
            _currentState.ExitState();
        }

        _currentState = nextState;
        _currentState.EnterState();
    }

    public void EnterState(FSMStateType stateType)
    {
        if (_fsmStates.ContainsKey(stateType))
        {
            AbstractFSMState nextState = _fsmStates[stateType];

            EnterState(nextState);
        }
    }

    #endregion
}