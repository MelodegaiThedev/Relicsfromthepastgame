using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    Idle,
    Patroling,
    Chasing,
    Attacking,
    Dead
}

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float health;

    // Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;
    public int projectileForceStrength;
    public float verticalForceStrength;
    public float spawnOffsetDistance = 1f;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Current state
    public EnemyState currentState = EnemyState.Idle;

    // Animator
    private Animator animator;

    // Default rotation
    private Quaternion defaultRotation;

    private void Awake()
    {
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        defaultRotation = transform.rotation;
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
            SetState(EnemyState.Patroling);
        if (playerInSightRange && !playerInAttackRange)
            SetState(EnemyState.Chasing);
        if (playerInAttackRange && playerInSightRange)
            SetState(EnemyState.Attacking);

        UpdateState();

        // Reset X rotation
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, eulerRotation.y, eulerRotation.z);
    }

    private void SetState(EnemyState newState)
    {
        currentState = newState;
    }

    private void UpdateState()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                // Implement idle behavior here if needed
                break;
            case EnemyState.Patroling:
                Patroling();
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                animator.SetBool("isWalking", true);
                animator.SetBool("isAttacking", false);
                break;
            case EnemyState.Attacking:
                AttackPlayer();
                animator.SetBool("isWalking", false);
                animator.SetBool("isAttacking", true);
                break;
            case EnemyState.Dead:
                // Play death animation and destroy the enemy
                animator.SetBool("isDead", true);
                Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
                break;
            default:
                break;
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        // Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        // Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            // Calculate the spawn position slightly in front of the enemy
            Vector3 spawnPosition = transform.position + transform.forward * spawnOffsetDistance;

            // Attack code here
            Rigidbody rb = Instantiate(projectile, spawnPosition, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileForceStrength, ForceMode.Impulse);
            rb.AddForce(transform.up * verticalForceStrength, ForceMode.Impulse);
            // End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0) SetState(EnemyState.Dead);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
