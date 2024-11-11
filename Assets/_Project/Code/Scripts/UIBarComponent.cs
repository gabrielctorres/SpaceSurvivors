using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using SmitePackage.Core.Events;
public class UIBarComponent : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textValue;
    [SerializeField] private Image fillImage;


    public void UpdateValue(Component component, object data)
    {
        float value = (float)data;
        textValue.text = value.ToString();
        fillImage.fillAmount = value / 100f;
    }
}
