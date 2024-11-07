using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Attribute
{
    [SerializeField] private string tagAttribute;
    [SerializeField] private float baseValue;
    public float CurrentValue { get; set; }
    public string TagAttribute { get => tagAttribute; set => tagAttribute = value; }

    public void Initialize()
    {
        CurrentValue = baseValue;
    }
    public void AddModifier(float value)
    {
        CurrentValue += value;
    }
}
