using UnityEngine;

public class FruitSpawn : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject fruitPrefab;

    public float effect;

    public string interactMessage => message;
    [SerializeField] string message = "Press E to spawn fruit";

    public void Interact()
    {
        Spawn();
    }

    void Spawn()
    {
        GameObject fruit = Instantiate(fruitPrefab, transform.position + Vector3.up, Quaternion.identity, transform);
    }
}
