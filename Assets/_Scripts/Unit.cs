using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamaggable
{

    public float CurrentHealth { get; set ; }
    public float MaxHealth { get; set; }

    
    [SerializeField] float maxHealthSetter;

    private void OnEnable() {
        CurrentHealth = maxHealthSetter;
        GameEvents.OnGooseDied += this.Die;
        GameEvents.OnGooseHit += this.TakeDamage;
    }
    public void Die(Unit unit) {
        
        Destroy(unit.gameObject);
    }

    public void TakeDamage(Unit unit, float  dmg) {
        Debug.Log(unit.name);
        unit.CurrentHealth -= dmg;
        Debug.Log(dmg + " given to " + unit.name);
            Debug.Log(unit.name + " has taken damage: " + CurrentHealth + " Remaining");
            if (CurrentHealth <= 0) {
                GameEvents.OnGooseDied(this);
            }
    }



    private void OnDestroy() {
        GameEvents.OnGooseHit -= TakeDamage;
        GameEvents.OnGooseDied -= Die;
    }

    
}
