using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] Transform m_Target;

    NavMeshAgent m_Agent;

    private void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        m_Agent.updateRotation = false;
        m_Agent.updateUpAxis = false;
    }

    private void Update()
    {
        m_Agent.SetDestination(m_Target.position);
    }
}
