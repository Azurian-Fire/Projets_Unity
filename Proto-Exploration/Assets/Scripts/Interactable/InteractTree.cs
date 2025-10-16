using UnityEngine;

public class InteractTree : InteractableEntity
{
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] FruitSpawnData spawnData;
    Color fittingColor;

    private void Start()
    {
        fittingColor = GetFittingColor();
    }
    
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

    void RandomFruitSpawn()
    {
        Vector2 randomOffset = Random.insideUnitCircle * spawnData.randomSpawnRadius;
        Vector3 randomStartingPos = new Vector3(transform.position.x + randomOffset.x,
            transform.position.y + spawnData.verticalOffset, transform.position.z + randomOffset.y);
        
        GameObject fruit = Instantiate(fruitPrefab, randomStartingPos, Quaternion.identity);
        fruit.transform.GetChild(0).GetComponent<Renderer>().material.color = fittingColor;
        fruit.GetComponent<InteractFruit>().totalStressValue = totalStressValue;
        Rigidbody fruitRb = fruit.GetComponent<Rigidbody>();
        fruitRb.AddTorque(randomStartingPos.normalized * spawnData.randomThrowStrength, ForceMode.Impulse);
    }

    Color GetFittingColor()
    {
        Color currentTreeColor = transform.GetComponent<Renderer>().material.color;
        return currentTreeColor;
    }
}