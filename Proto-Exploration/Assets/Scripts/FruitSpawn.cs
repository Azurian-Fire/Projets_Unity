using UnityEngine;

public class FruitSpawn : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject fruitPrefab;
    [SerializeField] FruitSpawnData spawnData;
    Color fittingColor;
    public int effect;

    public string interactMessage => message;
    [SerializeField] string message = "Press E to spawn fruit";

    public void Interact()
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
        fruit.GetComponent<FruitEffect>().stressEffect = effect;
        fruitRb.AddTorque(randomStartingPos.normalized * spawnData.randomThrowStrength, ForceMode.Impulse);
    }

    Color GetFittingColor()
    {
        Color currentTreeColor = transform.GetChild(1).GetComponent<Renderer>().material.color;
        return currentTreeColor;
    }
}
