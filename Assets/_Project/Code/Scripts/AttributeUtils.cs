using System.Collections.Generic;
using UnityEngine;

public static class AttributeUtils
{
    /// <summary>
    /// Procura um atributo em uma lista de atributos com base em uma tag, ignorando maiúsculas e minúsculas.
    /// </summary>
    /// <param name="tag">Tag a ser procurada.</param>
    /// <param name="attributes">Lista de atributos onde será feita a busca.</param>
    /// <returns>O atributo encontrado ou null, se não existir.</returns>
    public static Attribute ReturnAttribute(string tag, List<Attribute> attributes)
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

    /// <summary>
    /// Inicializa todos os atributos em uma lista de atributos.
    /// </summary>
    /// <param name="attributes">Lista de atributos a ser inicializada.</param>
    public static void InitializeAllAttributes(List<Attribute> attributes)
    {
        foreach (var attribute in attributes)
        {
            attribute.Initialize();
        }
    }
}
