using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState<T> : State<T>
{
    NavMeshAgent _agent;
    Action _action;
    Action _changeTarget;
    public IdleState(NavMeshAgent agent, Action coroutine, Action settarget) 
    {
        _agent = agent;
        _action = coroutine;
        _changeTarget = settarget;
    }
    public override void Enter()
    {
        base.Enter();
        _action.Invoke();
        Debug.Log("i'm idle");
    }
    public override void Execute()
    {
        base.Execute();
        _changeTarget.Invoke();
    }
}
