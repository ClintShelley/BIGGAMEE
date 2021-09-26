using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "IdleState2", menuName = "Unity-FSM/States/Idle2", order = 7)]
public class Idle2State : AbstractFSMState
{
    [SerializeField]
    float _idleDuration = 3f;

    float _totalDuration;


    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.IDLE2;
    }

    public override bool EnterState()
    {
        EnteredState = base.EnterState();

        if (EnteredState)
        {
            _totalDuration = 0f;
        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            _totalDuration += Time.deltaTime;

            if (_totalDuration >= _idleDuration)
            {
                _fsm.EnterState(FSMStateType.PPATROL);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}
