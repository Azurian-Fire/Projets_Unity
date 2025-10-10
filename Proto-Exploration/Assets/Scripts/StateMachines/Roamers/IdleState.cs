using UnityEngine;

public class IdleState : BasicRoamerState
{
    private float idleTimer;
    private float currentIdleDuration;
    private float idleDuration;
    private float idleDurationDelta;

    public override void Initialize(RoamerStateMachine sm)
    {
        base.Initialize(sm);
        idleDuration = roamerEntity.entityData.averageIdleDuration;
        idleDurationDelta = roamerEntity.entityData.idleDurationDelta;
    }

    public override void Enter()
    {
        currentIdleDuration = GetFittingRandomDuration(idleDuration, idleDurationDelta);
        //roamerEntity.SetMovementSpeed(roamerEntity.entityData.roamingMovementSpeed);
        idleTimer = 0f;
        Debug.Log($"{roamerEntity.name} enters Idle with a duration of {currentIdleDuration}");
    }

    public override void Tick()
    {
        idleTimer += Time.deltaTime;

        if (roamerEntity.target != null)
        {
            roamerStateMachine.ChangeState(GetComponent<ChaseState>());
            return;
        }

        if (idleTimer >= currentIdleDuration)
        {
            roamerStateMachine.ChangeState(GetComponent<RoamingState>());
        }

        //TODO random rotates
    }

    public override void Exit()
    {
        Debug.Log($"{roamerEntity.name} exits Idle");
    }
}