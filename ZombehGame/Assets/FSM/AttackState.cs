using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PatrolState", menuName = "Unity-FSM/States/Attack", order = 3)]
public class AttackState : AbstractFSMState
{
   // GameObject player = GameObject.FindGameObjectWithTag("Player");

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
            Debug.Log("Entered ATTACK State");
        }
        return EnteredState;
    }

    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) < 10f)
            {
                _navMeshAgent.SetDestination(player.transform.position);
            }
            else if (Vector3.Distance(_navMeshAgent.transform.position, player.transform.position) > 10f)
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
