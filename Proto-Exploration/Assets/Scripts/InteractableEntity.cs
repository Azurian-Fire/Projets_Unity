using UnityEngine;

public abstract class InteractableEntity : MonoBehaviour
{
    public int incrementCount;

    public int totalStressValue;
    public float interactDuration;


    private string entityType;

    public virtual string GetInteractMessage()
    {
        return $"Press E to interact with {entityType}";
    }

    public virtual void Interact(int succesfullIncrementCount, int stressChange = 0)
    {

    }
}
