using UnityEngine;

public class ChaseState : BasicRoamerState
{
    public override void Initialize(RoamerStateMachine sm)
    {
        base.Initialize(sm);
    }
    public override void Enter()
    {
        Debug.Log($"{roamerEntity.name} starts Chasing {roamerEntity.target?.name}");
        roamerEntity.SetMovementSpeed(roamerEntity.entityData.chaseMovementSpeed);
    }

    public override void Tick()
    {
        if (roamerEntity.target == null)
        {
            roamerStateMachine.ChangeState(GetComponent<IdleState>());
            return;
        }

        float distance = Vector2.Distance(roamerEntity.transform.position, roamerEntity.target.position);

        // Example: lose target if too far
        if (distance > 10f)
        {
            roamerEntity.target = null;
            roamerStateMachine.ChangeState(GetComponent<RoamingState>());
        }
    }
    public override void Exit()
    {
        Debug.Log($"{roamerEntity.name} stops Chasing");
    }
}