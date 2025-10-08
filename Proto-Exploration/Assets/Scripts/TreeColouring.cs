using UnityEngine;
using System.Collections.Generic;

public class TreeColouring : MonoBehaviour
{
    [SerializeField] Color[] colors;
    public Material[] childMaterials;
    [SerializeField] Color chosenColor;

    public static Dictionary<Color, int> colorEffectDict;

    // if value = 1, fruit is good
    //if value = 2, fruit is bad

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        childMaterials = new Material[transform.childCount];
        colorEffectDict = new Dictionary<Color, int>();

        // Choose a random color for the tree
        chosenColor = colors[Random.Range(0, colors.Length)];

        // Apply the color to all children
        for (int i = 0; i < transform.childCount; i++)
        {
            childMaterials[i] = transform.GetChild(i).GetComponent<Renderer>().material;
            childMaterials[i].color = chosenColor;
        }

        // Assign random effects to each color
        for (int i = 0; i < colors.Length; i++)
        {
            colorEffectDict.Add(colors[i], Random.Range(1, 2));
            Debug.Log($"Color: {colors[i]}, Effect: {colorEffectDict[colors[i]]}");
        }
    }
}
