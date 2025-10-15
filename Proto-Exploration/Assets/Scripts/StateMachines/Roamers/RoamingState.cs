using UnityEngine;

public class RoamingState : BasicRoamerState
{
    private Vector2 roamDirection;
    private float roamTimer;
    private float currentRoamingDuration;
    private float roamingDuration;
    private float roamingDurationDelta;

    public override void Initialize(RoamerStateMachine sm)
    {
        base.Initialize(sm);
        roamingDuration = roamerEntity.entityData.averageRoamingDuration;
        roamingDurationDelta = roamerEntity.entityData.roamingDurationDelta;
    }

    public override void Enter()
    {
        currentRoamingDuration = GetFittingRandomDuration(roamingDuration, roamingDurationDelta);
        roamerEntity.SetMovementSpeed(roamerEntity.entityData.roamingMovementSpeed);
        roamTimer = 0f;
        roamDirection = Random.insideUnitCircle.normalized;
        //Debug.Log($"{roamerEntity.name} starts Roaming");
        roamerEntity.MoveTowards(transform.position + new Vector3(roamDirection.x,0, roamDirection.y) * roamerEntity.entityData.roamingMovementSpeed * currentRoamingDuration);
    }

    public override void Tick()
    {
        roamTimer += Time.deltaTime;

        if (roamerEntity.target != null)
        {
            roamerStateMachine.ChangeState(GetComponent<ChaseState>());
            return;
        }

        if (roamTimer >= currentRoamingDuration)
        {
            roamerStateMachine.ChangeState(GetComponent<IdleState>());
        }

    }

    public override void Exit()
    {
        //Debug.Log($"{roamerEntity.name} stops Roaming");
    }
}