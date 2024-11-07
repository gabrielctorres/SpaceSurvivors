using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected AbilityData abilityData;

    protected Ability(AbilityData data)
    {
        abilityData = data;
    }

    public abstract void Activate(Caster caster);
}
