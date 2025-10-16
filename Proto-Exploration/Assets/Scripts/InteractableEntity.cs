using UnityEngine;

public abstract class InteractableEntity : MonoBehaviour
{
    public int incrementCount;

    public int totalStressValue;
    public float interactDuration;
    private string entityType;

    [Header("Memory")] public InteractableType interactableType { get; private set; }
    public string colorName { get; private set; }
    protected Color fittingColor;

    private void Start()
    {
        fittingColor = GetFittingColor();
        SetMainColor(fittingColor);
    }

    public virtual string GetInteractMessage()
    {
        return $"Press E to interact with {entityType}";
    }

    public virtual void Interact(int succesfullIncrementCount, int stressChange = 0)
    {
    }

    protected Color GetFittingColor()
    {
        Color currentTreeColor = transform.GetComponent<Renderer>().material.color;
        return currentTreeColor;
    }

    protected void SetMainColor(Color color)
    {
        colorName = ColorToString(color);
    }

    private string ColorToString(Color color)
    {
        string mainColor = "Red";

        if (color.maxColorComponent == color.r)
        {
            return mainColor;
        }
        else if (color.maxColorComponent == color.g)
        {
            mainColor = "Green";
        }
        else
        {
            mainColor = "Blue";
        }

        return mainColor;
    }
}