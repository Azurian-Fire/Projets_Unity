public enum InteractableType
{
    VerticalRectangle,
    HorizontalRectangle,
    ThinVerticalRectangle,
    BushRectangle,
    RectangleItem,
    RoundItem,
}
public struct InteractableColorKey
{
    public string interactableName;
    public string color;

    public InteractableColorKey(string shape, string color)
    {
        this.interactableName = shape;
        this.color = color;
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

public enum InteractionType
{
    Negative,
    Neutral,
    Positive
}