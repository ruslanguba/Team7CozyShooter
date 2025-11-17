using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody rb;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float acceleration = 6f;
    [SerializeField] private float attackRange = 5f; // дистанция на которой начинает атаковать
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private float chaseRange = 7f; // дистанция на которой начинает преследовать

    [SerializeField] private float attackCooldown = 2f; // время между атаками
    [SerializeField] private GameObject attackPrefab; // объект, который враг кидает
    [SerializeField] private Transform attackSpawn; // точка спауна атаки

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform target;

    private EnemyHealth health;
    public Rigidbody Rb => rb;
    public float MoveSpeed => moveSpeed;
    public float Acceleration => acceleration;
    public float AttackRange => attackRange;
    public float ThrowForce => throwForce;
    public float ChaseRange => chaseRange;

    public float AttackCooldown => attackCooldown;
    public GameObject AttackPrefab => attackPrefab;
    public Transform AttackSpawn => attackSpawn;

    public Transform Target => target;
    public Transform[] PatrolPoints => patrolPoints;
    public EnemyPatrolState PatrolState => patrolState;
    public EnemyAttackState AttackState => attackState;
    public EnemyChaseState ChaseState => chaseState;
    public EnemyDeathState DeathState => deathState;

    private EnemyStateMachine stateMachine;

    private EnemyPatrolState patrolState;
    private EnemyAttackState attackState;
    private EnemyChaseState chaseState;
    private EnemyDeathState deathState;

    private void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        health = GetComponent<EnemyHealth>();
        stateMachine = new EnemyStateMachine();

        patrolState = new EnemyPatrolState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);

        stateMachine.Initialize(patrolState);
        if(health != null)
            SubscribeToDeath(rb);
    }

    private void SubscribeToDeath(Rigidbody rb)
    {
        health.OnDeath += HandleDeath;
    }

    public void SetPlayerTransform(Transform transform)
    {
        target = transform;
    }
    private void HandleDeath(Rigidbody rb)
    {
        stateMachine.ChangeState(deathState);
    }

    private void Update() => stateMachine.CurrentState.Update();
    private void FixedUpdate() => stateMachine.CurrentState.FixedUpdate();
}
