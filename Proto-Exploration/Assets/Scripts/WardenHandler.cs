using UnityEngine;

public class WardenHandler : MonoBehaviour
{
    public WardenSpawnData wardenData;
    bool isBushInhabited;
    [SerializeField] int remainingWardenCount;

    void Awake()
    {
        GetComponent<SphereCollider>().radius = wardenData.triggerSpawnRadius;
        IsThereAWarden();
    }

    // Update is called once per frame
    private void IsThereAWarden()
    {
        isBushInhabited = Random.value < wardenData.spawnChance;
        if (isBushInhabited)
        {
            remainingWardenCount = wardenData.wardenCount;
            // Debug.Log($"bush {transform.name} is inhabited");
        }
    }

    private void SpawnWarden()
    {
        Instantiate(wardenData.warden, transform.position, Quaternion.identity);
        remainingWardenCount--;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        if (!isBushInhabited)
        {
            return;
        }
        if (remainingWardenCount == 0)
        {
            return;
        }
        SpawnWarden();
    }
}