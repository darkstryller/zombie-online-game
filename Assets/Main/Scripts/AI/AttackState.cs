using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState<T> : State<T>
{
    ZombieView _view;
    NavMeshAgent _agent;

    public AttackState(ZombieView view, NavMeshAgent agent)
    {
        _view = view;
        _agent = agent;
    }
    public override void Enter()
    {
        base.Enter();
        _view.Attack();
        
        _agent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        _agent.isStopped = false;
    }
}
