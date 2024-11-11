using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public List<Attribute> attributes = new List<Attribute>();
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
