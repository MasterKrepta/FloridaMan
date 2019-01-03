using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamaggable 
{
    float CurrentHealth { get; set; }
    float MyProperty { get; set; }
    void TakeDamage(float dmg);
    void Die();
}
