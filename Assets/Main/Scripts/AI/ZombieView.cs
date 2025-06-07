using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieView : MonoBehaviour
{
    [SerializeField] Animator _animator;

    public Action Walk;
    // Start is called before the first frame update
    void Start()
    {
        Walk += IsWalking;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IsWalking()
    {
        _animator.SetInteger("walking", 1);
    }
}
