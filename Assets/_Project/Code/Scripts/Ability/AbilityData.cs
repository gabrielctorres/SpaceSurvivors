using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilityData", menuName = "Project/Abilitys/Data", order = 0)]
public class AbilityData : ScriptableObject
{
    [Header("UI Settings")]
    public Sprite sprite;
    public string abilityName;
    public string description;

    [Header("Gameplay Settings")]
    public GameObject abilityPrefab;
    public List<Attribute> attributes = new List<Attribute>();

    private void OnEnable()
    {
        // Inicializa todos os atributos ao carregar o ScriptableObject
        foreach (var attribute in attributes)
        {
            attribute.Initialize();
        }
    }


    public Attribute ReturnAttribute(string tag)
    {
        foreach (var attribute in attributes)
        {

            if (attribute.TagAttribute.ToLower().Contains(tag.ToLower()) || tag.ToLower().Contains(attribute.TagAttribute.ToLower()))
            {
                return attribute;
            }
        }
        return null;
    }

}

