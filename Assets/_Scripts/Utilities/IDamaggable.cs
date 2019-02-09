using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaggable 
{
    float CurrentHealth { get; set; }
    float MaxHealth { get; set; }
    Transform DamaggableTransform { get; set; }
    void TakeDamage(IDamaggable unit, float dmg);
    void Die(Unit unit);

    
}
