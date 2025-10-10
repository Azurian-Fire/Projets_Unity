using UnityEngine;
using UnityEngine.AI;

public abstract class BasicRoamerState : MonoBehaviour
{
    protected NavMeshAgent roamerAgent;
    protected RoamerEntity roamerEntity;
    protected RoamerStateMachine roamerStateMachine;

    public virtual void Initialize(RoamerStateMachine stateMachine)
    {
        roamerAgent = GetComponent<NavMeshAgent>();
        roamerEntity = GetComponent<RoamerEntity>();
        this.roamerStateMachine = stateMachine;
    }

    public virtual void Enter() { }
    public virtual void Tick() { }
    public virtual void Exit() { }

    public virtual float GetFittingRandomDuration(float baseDuration, float delta)
    {
        return Random.Range(baseDuration - delta, baseDuration + delta);
    }
}