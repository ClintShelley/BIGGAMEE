using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(FiniteStateMachine))]

public class NPC : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    FiniteStateMachine _finiteStateMachine;

    public void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _finiteStateMachine = this.GetComponent<FiniteStateMachine>();
    }

    public void Start()
    {
        
    }

    public void Update()
    {
        
    }
}
