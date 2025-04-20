using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState<T> : State<T>
{
    public IdleState() 
    {

    }
    public override void Enter()
    {
        base.Enter();
        Debug.Log("i'm idle");
    }
}
