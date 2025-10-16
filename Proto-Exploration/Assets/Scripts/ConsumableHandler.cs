using System.Collections.Generic;
using UnityEngine;

public class ConsumableHandler : MonoBehaviour
{
    [SerializeField] Color[] colors;
    public Material[] childMaterials;
    [SerializeField] private GameObject[] possibleSpawnedPrefabs;
    
    public static Dictionary<Color, bool> colorEffectDict;

    [SerializeField] int fruitStressValue;

    void Awake()
    {
        InitializeColorEffectDictionary();
        AssignRandomAttributes();
    }

    private void AssignRandomAttributes()
    {
        // Assign color+fruit to all children
        for (int i = 0; i < transform.childCount; i++)
        {
            Color randomColor = colors[Random.Range(0, colors.Length)];
            GameObject randomSpawnedItem = possibleSpawnedPrefabs[Random.Range(0, possibleSpawnedPrefabs.Length)];

            childMaterials[i] = transform.GetChild(i).GetComponent<Renderer>().material;
            childMaterials[i].color = randomColor;

            // Apply the value effect to the child
            InteractTree interactTree = transform.GetChild(i).GetComponent<InteractTree>();
            interactTree.totalStressValue = (colorEffectDict[randomColor] ? -1 : 1) * fruitStressValue;
            interactTree.SetFruitPrefab(randomSpawnedItem);
        }
    }

    private void InitializeColorEffectDictionary()
    {
        childMaterials = new Material[transform.childCount];
        colorEffectDict = new Dictionary<Color, bool>();
        
        // Assign random value effect to each color
        for (int i = 0; i < colors.Length; i++)
        {
            bool effectValue = Random.value < 0.5f;
            colorEffectDict.Add(colors[i], effectValue);
        }
    }
}