using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementState<T> : State<T>
{
    Transform _self;
    Transform _target;
    NavMeshAgent _agent;
    ZombieView _view;
    public MovementState( Transform self,Transform target, NavMeshAgent agent, ZombieView view)
    {
        _self = self;
        _target = target;
        _agent = agent;
        _view = view;
    }
    public override void Enter()
    {
        base.Enter();
        _view.Walk(1);
    }
    public override void Execute()
    {
        base.Execute();
        if (_agent != null) 
        {
            if( _target.position != _self.position)
            {
                _agent.SetDestination(_target.position);
            }
        }

    }
}
