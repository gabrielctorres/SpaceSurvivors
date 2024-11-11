using System.Collections;
using System.Collections.Generic;
using SmitePackage.Core.Events;
using UnityEngine;

public class PlayerShip : Unit, IDamageable
{
    [SerializeField] private GameEvent OnUpdateLife;

    // Start is called before the first frame update
    void Start()
    {
        AttributeUtils.InitializeAllAttributes(attributes);
        OnUpdateLife.Raise(this, AttributeUtils.ReturnAttribute("Life", attributes).CurrentValue);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        Attribute life = AttributeUtils.ReturnAttribute("Life", attributes);
        if (life != null)
        {
            life.CurrentValue -= damage;
            if (life.CurrentValue <= 0)
            {
                Destroy(gameObject); // Destroi o asteroide atual
            }
            OnUpdateLife.Raise(this, AttributeUtils.ReturnAttribute("Life", attributes).CurrentValue);
        }
    }
}
