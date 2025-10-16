using System;
using UnityEngine;

public abstract class InteractableEntity : MonoBehaviour
{
    public int incrementCount;

    public int totalStressValue;
    public float interactDuration;
    private string entityType;
    public static event Action<InteractableEntity> GlobalInteratableMemoryCheck;

    [Header("Memory")] public InteractableType interactableType;
    public string colorName { get; private set; }
    private string DEBUGEFFECT;
    protected Color fittingColor;

    private void Start()
    {
        fittingColor = GetFittingColor();
        SetMainColor(fittingColor);
    }

    protected virtual void OnEnable()
    {
        GlobalInteratableMemoryCheck += OnShapeInteracted;
    }

    protected virtual void OnDisable()
    {
        GlobalInteratableMemoryCheck -= OnShapeInteracted;
    }

    protected virtual void OnShapeInteracted(InteractableEntity interactedEntity)
    {
        bool sameType = interactableType.Equals(interactedEntity.interactableType);
        bool sameColor = colorName == interactedEntity.colorName;
        if (sameType && sameColor && this != interactedEntity)
        {
            Debug.Log($"{name} is known to be {MemoryManager.GetMemory(colorName+interactableType.ToString())}");
            UpdateInteractableMemory();

        }
    }

    public virtual string GetInteractMessage()
    {
        return $"Press E to interact with {entityType}";
    }

    public virtual void Interact(int succesfullIncrementCount, int stressChange = 0)
    {
        GlobalInteratableMemoryCheck?.Invoke(this);
    }

    protected void SpawnParticuleSystem()
    {
        if (DEBUGEFFECT == null) return;
        if (DEBUGEFFECT == "Negative")
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }        
        if (DEBUGEFFECT == "Positive")
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    
    protected void UpdateInteractableMemory()
    {
        DEBUGEFFECT = MemoryManager.GetMemory(GetInteractableColorKey()).ToString();
        SpawnParticuleSystem();
    }

    protected string GetInteractableColorKey()
    {
        return new InteractableColorKey(interactableType.ToString(), colorName, this).GetInteractableName();
    }

    protected InteractableColorKey GetInteractableColorKeyFRFR()
    {
        return new InteractableColorKey(interactableType.ToString(), colorName, this);
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