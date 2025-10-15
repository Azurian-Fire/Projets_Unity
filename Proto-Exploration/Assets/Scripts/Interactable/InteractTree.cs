using UnityEngine;

public class InteractTree : InteractableEntity
{
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] FruitSpawnData spawnData;
    Color fittingColor;
    public int effect;


    public override string GetInteractMessage()
    {
        //Todo: add timer, effect in 1 increment
        string message = base.GetInteractMessage();
        return message;
    }

    public override void Interact(int stressChange)
    {
        Spawn();
    }

    void Spawn()
    {
        Vector2 randomOffset = Random.insideUnitCircle * spawnData.randomSpawnRadius;
        Vector3 randomStartingPos = new Vector3(transform.position.x + randomOffset.x, transform.position.y + spawnData.verticalOffset, transform.position.z + randomOffset.y);
        GameObject fruit = Instantiate(fruitPrefab, randomStartingPos, Quaternion.identity);
        fruit.transform.GetChild(0).GetComponent<Renderer>().material.color = GetFittingColor();
        Rigidbody fruitRb = fruit.GetComponent<Rigidbody>();
        fruit.GetComponent<InteractFruit>().stressEffect = effect;
        fruitRb.AddTorque(randomStartingPos.normalized * spawnData.randomThrowStrength, ForceMode.Impulse);
    }

    Color GetFittingColor()
    {
        Color currentTreeColor = transform.GetComponent<Renderer>().material.color;
        return currentTreeColor;
    }
}
