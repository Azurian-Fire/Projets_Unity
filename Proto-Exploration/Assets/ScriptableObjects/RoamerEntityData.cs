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
    [Header("Chase Settings")]
    public float chaseMovementSpeed;
    public float chaseScanIntervalTimer;
    public float chaseOffsetRadius;
    [Header("Aggro Settings")]
    public float aggroRangeRadius;
    public float deaggroTime;
}