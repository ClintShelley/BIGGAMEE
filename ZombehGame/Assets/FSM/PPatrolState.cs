using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;
using NPCCode;

[CreateAssetMenu(fileName = "PPatrolState", menuName = "Unity-FSM/States/PPatrol", order = 6)]
public class PPatrolState : AbstractFSMState
{

    NPCPatrolPoint[] _patrolPoints;
    int _patrolPointIndex;

    public override void OnEnable()
    {
        base.OnEnable();
        StateType = FSMStateType.PPATROL;
        _patrolPointIndex = -1;
    }

    public override bool EnterState()
    {
        EnteredState = false;
        if (base.EnterState())
        {
            _patrolPoints = _npc.PatrolPoints;

            if (_patrolPoints == null || _patrolPoints.Length == 0)
            {

            }
            else
            {
                if (_patrolPointIndex < 0)
                {
                    _patrolPointIndex = UnityEngine.Random.Range(0, _patrolPoints.Length);
                }
                else
                {
                    _patrolPointIndex = (_patrolPointIndex + 1) % _patrolPoints.Length;
                }

                SetDestination(_patrolPoints[_patrolPointIndex]);
                EnteredState = true;
            }
        }
        return EnteredState;
    }


    public override void UpdateState()
    {
        if (EnteredState)
        {
            if (Vector3.Distance(_navMeshAgent.transform.position, _patrolPoints[_patrolPointIndex].transform.position) >= 15f)
            {
                _fsm.EnterState(FSMStateType.IDLE);
            }
        }
    }

    private void SetDestination(NPCPatrolPoint destination)
    {
        if (_navMeshAgent != null && destination != null)
        {
            _navMeshAgent.SetDestination(destination.transform.position);
        }
    }
}
