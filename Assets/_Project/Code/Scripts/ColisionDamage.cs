using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionDamage : MonoBehaviour
{
    public float Damage { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().TakeDamage(Damage);
        }
        Destroy(this.gameObject);
    }
}
