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
    [SerializeField] private EnemyRotationHandler rotationHandler;
    [SerializeField] private float attackCooldown = 2f; // время между атаками
    [SerializeField] private GameObject attackPrefab; // объект, который враг кидает
    [SerializeField] private Transform attackSpawn; // точка спауна атаки

    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private Transform playerTransform;

    [SerializeField] private Animator animator;
    [SerializeField] private ActorAudio actorAudio;
    [SerializeField] private AnimationEventListener eventListener;

    private EnemyHealth health;
    public Rigidbody Rb => rb;
    public float MoveSpeed => moveSpeed;
    public float Acceleration => acceleration;
    public float AttackRange => attackRange;
    public float ThrowForce => throwForce;
    public float ChaseRange => chaseRange;

    public float AttackCooldown => attackCooldown;
    public Animator Animator => animator;
    public EnemyRotationHandler RotationHandler => rotationHandler;
    public GameObject AttackPrefab => attackPrefab;
    public Transform AttackSpawn => attackSpawn;
    public ActorAudio ActorAudio => actorAudio;

    public Transform PlayerTransform => playerTransform;
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

    private void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();

        health = GetComponent<EnemyHealth>();
        animator = GetComponentInChildren<Animator>();
        eventListener = GetComponentInChildren<AnimationEventListener>();
        rotationHandler = GetComponent<EnemyRotationHandler>();
        actorAudio = GetComponent<ActorAudio>();
        playerTransform = PlayerManager.Instance.GetPlayerTransform;

        stateMachine = new EnemyStateMachine();

        patrolState = new EnemyPatrolState(this, stateMachine);
        attackState = new EnemyAttackState(this, stateMachine);
        chaseState = new EnemyChaseState(this, stateMachine);
        deathState = new EnemyDeathState(this, stateMachine);

        stateMachine.Initialize(patrolState);
        if (health != null)
            SubscribeToDeath();
        SubscribeToJump();
        float randomOffset = Random.Range(0f, 0.5f);
        animator.Play("Walk", 0, randomOffset);
    }

    private void SubscribeToJump()
    {
        eventListener.OnAnimationJump += actorAudio.PlayJump;
    }

    private void SubscribeToDeath()
    {
        health.OnDeath += HandleDeath;
    }

    public void SetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }
    private void HandleDeath(EnemyHealth enemy, int collisionsCount)
    {
        stateMachine.ChangeState(deathState);
    }

    private void Update() => stateMachine.CurrentState.Update();
    private void FixedUpdate() => stateMachine.CurrentState.FixedUpdate();
}
