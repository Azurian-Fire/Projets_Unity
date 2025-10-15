using UnityEngine;
[CreateAssetMenu(fileName = "WardenSpawnData", menuName = "Scriptable Objects/WardenSpawnData")]
public class WardenSpawnData : ScriptableObject
{
    public GameObject warden;
    public float triggerSpawnRadius;
    public int wardenCount;
    public float spawnChance;
}
