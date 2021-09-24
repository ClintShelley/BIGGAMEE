using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackState", menuName = "Unity-FSM/States/Attack", order = 3)]
public class AttackState : AbstractFSMState
{

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.ATTACK;
    }

    public override bool EnterState()
    {
        EnteredState = base.EnterState();

        if (EnteredState)
        {

        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 40f)
            {
                _navMeshAgent.SetDestination(player.transform.position);
            }
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 20f)
            {
                _fsm.EnterState(FSMStateType.PATROL);
            }
        }
    }

    public override bool ExitState()
    {
        base.ExitState();
        return true;
    }
}
