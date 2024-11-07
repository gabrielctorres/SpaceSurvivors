using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : Ability
{
    public ProjectileAbility(AbilityData data) : base(data) { }

    public override void Activate(Caster caster)
    {
        // Obter o número de projéteis e ângulo do cone a partir dos atributos
        var projectileCountAttr = abilityData.ReturnAttribute("Projectile");
        int projectileCount = projectileCountAttr != null ? Mathf.RoundToInt(projectileCountAttr.CurrentValue) : 1;

        var coneAngleAttr = abilityData.ReturnAttribute("Projectile_Angle");
        float coneAngle = coneAngleAttr != null ? coneAngleAttr.CurrentValue : 45f; // Ângulo do cone (por exemplo, 45 graus)

        // Ângulo entre cada projétil dentro do cone
        float angleStep = coneAngle / Mathf.Max(projectileCount - 1, 1);

        // Ângulo inicial para centralizar o cone na direção do caster
        float startAngle = -coneAngle / 2f;

        for (int i = 0; i < projectileCount; i++)
        {
            // Calcula o ângulo para este projétil
            float currentAngle = startAngle + angleStep * i;

            // Direção de movimento do projétil baseada no ângulo calculado
            Vector3 projectileDirection = Quaternion.Euler(0, 0, currentAngle) * caster.transform.up;

            // Posição inicial ligeiramente deslocada a partir do caster
            Vector3 spawnPosition = caster.transform.position + projectileDirection * 0.5f; // Ajuste 0.5f como deslocamento inicial

            // Instancia o projétil e define sua direção
            GameObject projectile = Instantiate(abilityData.abilityPrefab, spawnPosition, Quaternion.identity);
            projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, projectileDirection); // Alinha o projétil na direção calculada
            projectile.GetComponent<Rigidbody2D>().velocity = projectileDirection * (abilityData.ReturnAttribute("Projectile_Speed")?.CurrentValue ?? 10f);

            // Define o tempo de vida do projétil, se aplicável
            var lifetimeAttribute = abilityData.ReturnAttribute("Lifetime");
            if (lifetimeAttribute != null)
            {
                Destroy(projectile, lifetimeAttribute.CurrentValue);
            }
        }
    }
}