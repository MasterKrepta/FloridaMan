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
        GameEvents.OnGooseDied += Die;
        //GameEvents.OnGooseHit += TakeDamage;
    }
    public void Die(Unit unit) {
        
        Destroy(unit.gameObject);
    }

    public void TakeDamage(Unit unit, float  dmg) {
        //TODO slow down time when we are hit
        unit.CurrentHealth -= dmg;
        GameEvents.OnGooseHit(this);
        if (CurrentHealth <= 0) {
            GameEvents.OnGooseDied(this);
        }


        
    }



    private void OnDestroy() {
        //GameEvents.OnGooseHit -= TakeDamage;
        GameEvents.OnGooseDied -= Die;


    }

    
}
