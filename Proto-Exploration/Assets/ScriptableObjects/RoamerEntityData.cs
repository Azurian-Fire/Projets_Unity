using UnityEngine;

[CreateAssetMenu]
public class RoamerEntityData : ScriptableObject
{
    [Header("Stress Settings")]
    public float stressAuraDamage;
    public float stressHitDamage;
    [Header("Idle Settings")]
    public float averageIdleDuration;
    public float idleDurationDelta;    
    [Header("Roaming Settings")]
    public float roamingMovementSpeed;
    public float averageRoamingDuration;
    public float roamingDurationDelta;
    [Header("Aggro Settings")]
    public float chaseMovementSpeed;
    public float aggroRangeRadius;
    public float deaggroTime;
}