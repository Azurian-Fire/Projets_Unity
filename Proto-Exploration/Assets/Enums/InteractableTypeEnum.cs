public enum InteractableType
{
    VerticalRectangle,
    HorizontalRectangle,
    ThinVerticalRectangle,
    BushRectangle,
    RectangleItem,
    RoundItem,
}
public enum InteractionType
{
    Negative,
    Neutral,
    Positive
}

public struct InteractableColorKey
{
    public string interactableName;
    public string color;
    public InteractableEntity interactableEntity;

    public InteractableColorKey(string shape, string color, InteractableEntity interactableEntity)
    {
        this.interactableName = shape;
        this.color = color;
        this.interactableEntity = interactableEntity;
    }

    public string GetInteractableName()
    {
        return color+interactableName;
    }

    public override bool Equals(object obj)
    {
        if (!(obj is InteractableColorKey other)) return false;
        return interactableName == other.interactableName && color == other.color;
    }

    public override int GetHashCode()
    {
        return (color+interactableName).GetHashCode();
    }
}

