using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "Unity-FSM/States/Idle", order = 1)]
public class IdleState : AbstractFSMState
{
    [SerializeField]
    float _idleDuration = 3f;

    float _totalDuration;


    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE;
    }

    public override bool EnterState()
    {
        EnteredState = base.EnterState();

        if (EnteredState)
        {
            Debug.Log("Entered Idle State");
            _totalDuration = 0f;
        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            _totalDuration += Time.deltaTime;
            Debug.Log("Entered Update State" +_totalDuration +" seconds");

            if(_totalDuration >= _idleDuration)
            {
                _fsm.EnterState(FSMStateType.PATROL);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Entered Exit State");
        return true;
    }
}
