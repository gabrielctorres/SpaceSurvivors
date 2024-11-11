using System.Collections.Generic;
using UnityEngine;

public class Caster : MonoBehaviour
{
    public List<CasterSlot> abilitySlots = new List<CasterSlot>();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            UpgradeAbilitiesByAttribute("Projectile", 1f);
        }


        foreach (var slot in abilitySlots)
        {
            slot.UpdateCooldown(Time.deltaTime);


            if (slot.IsReady)
            {
                CastAbility(slot);
                slot.ActivateCooldown();
            }
        }
    }

    private void CastAbility(CasterSlot slot)
    {
        if (slot.abilityData == null) return;

        InstantiateAbility(slot.abilityData);
    }

    private void InstantiateAbility(AbilityData abilityData)
    {
        // Verifica o tipo da habilidade e instancia de forma modular
        Ability ability = CreateAbilityInstance(abilityData);
        ability?.Activate(this);

    }

    private Ability CreateAbilityInstance(AbilityData abilityData)
    {
        var projectileAttribute = AttributeUtils.ReturnAttribute("Projectile", abilityData.attributes);
        if (projectileAttribute != null)
        {
            return new ProjectileAbility(abilityData);
        }
        // Adicione mais tipos de habilidades conforme necessário

        return null; // Retorna null se o tipo de habilidade não foi identificado
    }

    public void UpgradeAbilitiesByAttribute(string attributeTag, float upgradeValue)
    {
        foreach (var slot in abilitySlots)
        {
            if (slot.abilityData != null)
            {
                // Tenta obter o atributo com o tag específico
                var attribute = AttributeUtils.ReturnAttribute(attributeTag, slot.abilityData.attributes);
                if (attribute != null)
                {
                    attribute.AddModifier(upgradeValue);
                    Debug.Log($"Upgraded {slot.abilityData.name}'s {attributeTag} by {upgradeValue}. New Value: {attribute.CurrentValue}");
                }
            }
        }
    }
}

[System.Serializable]
public class CasterSlot
{
    public AbilityData abilityData; // Dados da habilidade
    public float cooldownTimer;     // Timer para o cooldown da habilidade

    public CasterSlot(AbilityData abilityData)
    {
        this.abilityData = abilityData;
        cooldownTimer = 0f;
    }

    public void UpdateCooldown(float deltaTime)
    {
        if (cooldownTimer > 0)
            cooldownTimer -= deltaTime;
    }

    public bool IsReady => cooldownTimer <= 0;

    public void ActivateCooldown()
    {
        // Verifica se existe um atributo de cooldown
        var cooldownAttribute = AttributeUtils.ReturnAttribute("Cooldown", abilityData.attributes);
        if (cooldownAttribute != null)
        {
            cooldownTimer = cooldownAttribute.CurrentValue;
        }
        else
        {
            // Sem cooldown: habilidade fica pronta imediatamente
            cooldownTimer = 0f;
        }
    }
}
