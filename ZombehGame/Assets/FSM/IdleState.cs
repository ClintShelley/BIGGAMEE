using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class IdleState : AbstractFSMState
{
    public override bool EnterState()
    {
        base.EnterState();
        Debug.Log("Entered Idle State");
        return true;
    }

    public override void UpdateState()
    {
        Debug.Log("Entered Update State");
    }

    public override bool ExitState()
    {
        base.ExitState();
        Debug.Log("Entered Exit State");
        return true;
    }
}
