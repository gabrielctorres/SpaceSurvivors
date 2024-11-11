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
        AttributeUtils.InitializeAllAttributes(attributes);
    }
}

