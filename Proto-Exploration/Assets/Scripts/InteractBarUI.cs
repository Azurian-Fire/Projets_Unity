using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class InteractBarUI : MonoBehaviour
{
    [FormerlySerializedAs("interactBar")] [SerializeField]
    private RectTransform interact;

    [SerializeField] float width;
    [SerializeField] float height;
    bool isVisible;
    Image image;

    void Start()
    {
        image = transform.GetComponent<Image>();
        image.enabled = isVisible;
    }

    public void SetInteractUI(float newCompletion)
    {
        isVisible = newCompletion > 0;
        image.enabled = isVisible;
        float newWidth = newCompletion * width;
        interact.sizeDelta = new Vector2(newWidth, height);
    }
}