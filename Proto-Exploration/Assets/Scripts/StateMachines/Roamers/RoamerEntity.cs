using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RoamerEntity : MonoBehaviour
{
    public Transform target;
    public RoamerEntityData entityData;
    [Header("References")]
    protected Rigidbody rb;
    protected RoamerStateMachine stateMachine;
    protected NavMeshAgent agent;
    protected SphereCollider aggroCollider;

    protected virtual void Awake()
    {
        stateMachine = GetComponent<RoamerStateMachine>();
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        aggroCollider = GetComponent<SphereCollider>();
        stateMachine.Initialize();
        SetMovementSpeed(entityData.roamingMovementSpeed);
        aggroCollider.radius = entityData.aggroRangeRadius;
    }

    protected virtual void Update()
    {
        stateMachine.currentState.Tick();
    }

    public void MoveTowards(Vector3 targetPosition)
    {
        agent.SetDestination(targetPosition);
        //rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    public void SetMovementSpeed(float movementSpeed)
    {
        agent.speed = movementSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        target = other.transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        target = null;
    }
}