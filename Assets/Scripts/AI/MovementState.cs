using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementState<T> : State<T>
{
    Transform _self;
    Transform _target;
    NavMeshAgent _agent;
    public MovementState( Transform self,Transform target, NavMeshAgent agent)
    {
        _self = self;
        _target = target;
        _agent = agent;
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
