using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaggable 
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    void TakeDamage(Unit unit, float dmg, RaycastHit hit);
    void Die(Unit unit);
}
