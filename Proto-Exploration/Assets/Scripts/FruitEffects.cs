using System.Collections.Generic;
using UnityEngine;

public class FruitEffects : MonoBehaviour
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

        // Assign random value effect to each color
        for (int i = 0; i < colors.Length; i++)
        {
            int effectValue = Random.Range(1, 3);
            colorEffectDict.Add(colors[i], effectValue);
            Debug.Log($"Color: {colors[i]}, Effect: {effectValue}");
        }

        // Apply the color to all children
        for (int i = 0; i < transform.childCount; i++)
        {
            // Choose a random color for the tree
            chosenColor = colors[Random.Range(0, colors.Length)];

            // Apply the color each child of the child
            //childMaterials[i] = transform.GetChild(i).GetChild(0).GetComponent<Renderer>().material;
            //childMaterials[i].color = chosenColor;

            childMaterials[i] = transform.GetChild(i).GetChild(1).GetComponent<Renderer>().material;
            childMaterials[i].color = chosenColor;

            // Apply the value effect to the child
            FruitSpawn fruitSpawn = transform.GetChild(i).GetComponent<FruitSpawn>();
            fruitSpawn.effect = colorEffectDict[chosenColor];

        }

    }
}
