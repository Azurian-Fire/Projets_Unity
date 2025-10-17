using UnityEngine;

public class ChaseState : BasicRoamerState
{
    public float chaseTimer;
    private float chaseDuration;
    [SerializeField] float currentAuraTimer;
    [SerializeField] float currentAttackTimer;

    private float stressAuraDmg;
    private float stressHitDmg;

    private float currentScanIntervalTimer;
    private float scanIntervalTimer;
    private float offsetRadius;
    private Vector3 lastKnownTargetPosition;

    public override void Initialize(RoamerStateMachine sm)
    {
        base.Initialize(sm);
        scanIntervalTimer = roamerEntity.entityData.chaseScanIntervalTimer;

        chaseDuration = roamerEntity.entityData.deaggroTime;
        offsetRadius = roamerEntity.entityData.chaseOffsetRadius;
        stressAuraDmg = roamerEntity.entityData.stressAuraDamage;
        stressHitDmg = roamerEntity.entityData.stressHitDamage;
    }

    public override void Enter()
    {
        //Debug.Log($"{roamerEntity.name} starts Chasing {roamerEntity.target?.name}");
        chaseTimer = 0f;
        currentScanIntervalTimer = 0f;
        roamerEntity.SetMovementSpeed(roamerEntity.entityData.chaseMovementSpeed);
    }

    public override void Tick()
    {
        currentScanIntervalTimer += Time.deltaTime;

        if (currentScanIntervalTimer >= scanIntervalTimer)
        {
            UpdateLastKnownTargetPosition();
            currentScanIntervalTimer = 0f;
        }

        if (roamerEntity.target == null)
        {
            chaseTimer += Time.deltaTime;
            roamerEntity.MoveTowards(lastKnownTargetPosition);
            if (chaseTimer > chaseDuration)
            {
                roamerStateMachine.ChangeState(GetComponent<IdleState>());
            }
        }
        else
        {
            HandleAttack();
        }
    }

    public override void Exit()
    {
        //Debug.Log($"{roamerEntity.name} stops Chasing");
    }

    private void HandleAttack()
    {
        if (Vector3.Distance(roamerEntity.target.transform.position, transform.position) <
            roamerEntity.entityData.attackRange)
        {
            currentAttackTimer += Time.deltaTime;
            if (currentAttackTimer >= roamerEntity.entityData.attackCooldown)
            {
                roamerEntity.target.GetComponent<Player>().ChangeHealth(stressHitDmg);
                currentAttackTimer = 0f;
            }
        }
        else
        {
            currentAttackTimer = 0f;
        }
    }

    private void UpdateLastKnownTargetPosition()
    {
        if (roamerEntity.target != null)
        {
            chaseTimer = 0f;
            lastKnownTargetPosition = roamerEntity.target.position;
            //Debug.Log($"SCANNING AND FOUND AT {lastKnownTargetPosition}");
            Vector3 offsetPosition = GetRandomOffsetPosition(lastKnownTargetPosition);
            roamerEntity.MoveTowards(offsetPosition);
        }
    }

    private Vector3 GetRandomOffsetPosition(Vector3 targetPosition)
    {
        Vector2 randomOffset = Random.insideUnitCircle * offsetRadius;
        return targetPosition + new Vector3(randomOffset.x, 0, randomOffset.y);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        currentAuraTimer = 0f;
    }

    void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        currentAuraTimer += Time.deltaTime;
        if (currentAuraTimer >= roamerEntity.entityData.stressAuraTimer)
        {
            other.GetComponent<Player>().ChangeHealth(stressAuraDmg);
            currentAuraTimer = 0f;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        currentAuraTimer = 0f;
    }
}