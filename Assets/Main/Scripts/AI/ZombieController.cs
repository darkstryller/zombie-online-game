using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    NavMeshAgent _agent;
    LineOfSightMono _los;
    HealthScript _health;
    [SerializeField] EnemyStats _stats;
    [SerializeField] Transform _target;

    FSM<StateEnum> _fsm;
    ITreeNode _root;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _los = GetComponent<LineOfSightMono>();
        _health = GetComponent<HealthScript>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        Debug.Log("default speed es: " + _agent.speed);
        
    }
    void Start()
    {
        _agent.speed = _stats._speed;
        _health.SetHealth(_stats._health);
        InitializeFSM();
        InitializeTree();
    }
    void Update()
    {
        _fsm.OnExecute();
        _root.Execute();
    }

    void InitializeFSM()
    {
        _fsm = new FSM<StateEnum>();

        var idle = new IdleState<StateEnum>();
        var move = new MovementState<StateEnum>(transform, _target, _agent);
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
        ITreeNode qIsAlive = new QuestionNode(_health.IsAlive, qHasSeenFoe, dead);

        _root = qIsAlive;
    }

    bool questionHasSeenFoe()
    {

        return _los.LOS(_target);
    }
}
