using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class ZombieController : MonoBehaviourPunCallbacks, IDamageable
{
    NavMeshAgent _agent;
    LineOfSightMono _los;
    HealthScript health;
    [SerializeField] EnemyStats _stats;
    [SerializeField] GameObject _target;

    FSM<StateEnum> _fsm;
    ITreeNode _root;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _los = GetComponent<LineOfSightMono>();
        health = GetComponent<HealthScript>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        health.maxHealth = _stats._health;
        
    }
    void Start()
    {
        _target = FindAnyObjectByType<PlayerMovement>().gameObject;
        _agent.speed = _stats._speed;
        InitializeFSM();
        InitializeTree();
    }
    
    void Update()
    {
        _fsm.OnExecute();
        _root.Execute();
        Debug.Log("player pos: " + _target.transform.position);
        if (health._currentHealth <= 0)
        {
            Debug.Log("AAAAAAAAAA");
        }
    }

    public void GetDamage(int damage)
    {
        health.TakeDamage(damage);
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StateEnum>();

        var idle = new IdleState<StateEnum>();
        var move = new MovementState<StateEnum>(transform, _target.transform, _agent);
        var dead = new deathState<StateEnum>(gameObject);

        idle.AddTransition(StateEnum.MOVE, move);
        idle.AddTransition(StateEnum.DEATH, dead);

        move.AddTransition(StateEnum.IDLE, idle);
        move.AddTransition(StateEnum.DEATH, dead);

        dead.AddTransition(StateEnum.IDLE, idle);
        dead.AddTransition(StateEnum.MOVE, move);

        _fsm.SetInit(idle);
    }
    void InitializeTree()
    {

        ITreeNode idle = new ActionNode(() => _fsm.Transition(StateEnum.IDLE));
        ITreeNode move = new ActionNode(() => _fsm.Transition(StateEnum.MOVE));
        ITreeNode dead = new ActionNode(() => _fsm.Transition(StateEnum.DEATH));

        ITreeNode qHasSeenFoe = new QuestionNode(questionHasSeenFoe, move, idle);
        ITreeNode qIsAlive = new QuestionNode(health.IsAlive, qHasSeenFoe, dead);

        _root = qIsAlive;
    }

    bool questionHasSeenFoe()
    {
        Debug.Log(_los.LOS(_target.transform));
        return _los.LOS(_target.transform);
        
    }
}
