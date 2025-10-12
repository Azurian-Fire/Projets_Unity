using UnityEngine;

public class ChaseState : BasicRoamerState
{
    public float chaseTimer;
    private float chaseDuration;
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
    }
    public override void Enter()
    {
        Debug.Log($"{roamerEntity.name} starts Chasing {roamerEntity.target?.name}");
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
    }

    public override void Exit()
    {
        Debug.Log($"{roamerEntity.name} stops Chasing");
    }

    private void UpdateLastKnownTargetPosition()
    {
        if (roamerEntity.target != null)
        {
            chaseTimer = 0f;
            lastKnownTargetPosition = roamerEntity.target.position;
            Debug.Log($"SCANNING AND FOUND AT {lastKnownTargetPosition}");
            Vector3 offsetPosition = GetRandomOffsetPosition(lastKnownTargetPosition);
            roamerEntity.MoveTowards(offsetPosition);
        }
    }
    private Vector3 GetRandomOffsetPosition(Vector3 targetPosition)
    {
        Vector2 randomOffset = Random.insideUnitCircle * offsetRadius;
        return targetPosition + new Vector3(randomOffset.x, 0, randomOffset.y);
    }

}