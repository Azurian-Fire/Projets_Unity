using UnityEngine;
using UnityEngine.ProBuilder;
public class InteractTree : InteractableEntity
{
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] FruitSpawnData spawnData;
    [SerializeField] private float randomSpawnCircleRadius;
    [SerializeField] private float verticalSpawnOffset;

    public override string GetInteractMessage()
    {
        string message = base.GetInteractMessage();
        return message;
    }

    public override void Interact(int succesfullIncrementCount, int stressChange)
    {
        for (int i = 0; i < succesfullIncrementCount; i++)
        {
            RandomFruitSpawn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0,verticalSpawnOffset,0), randomSpawnCircleRadius);
    }

    void RandomFruitSpawn()
    {
        Vector2 randomOffset = Random.insideUnitCircle.normalized * randomSpawnCircleRadius;
        Vector3 randomStartingPos = new Vector3(transform.position.x + randomOffset.x,
            transform.position.y + verticalSpawnOffset, transform.position.z + randomOffset.y);

        GameObject fruit = Instantiate(fruitPrefab, randomStartingPos, Quaternion.identity);
        fruit.transform.GetComponent<Renderer>().material.color = fittingColor;
        fruit.GetComponent<InteractFruit>().totalStressValue = totalStressValue;
        Rigidbody fruitRb = fruit.GetComponent<Rigidbody>();
        fruitRb.AddTorque(randomStartingPos * spawnData.randomThrowStrength/5, ForceMode.Impulse);
    }


    public void SetFruitPrefab(GameObject newPrefab)
    {
        fruitPrefab = newPrefab;
    }
}