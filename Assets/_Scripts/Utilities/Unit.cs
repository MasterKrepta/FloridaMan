﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, IDamaggable
{

    public float CurrentHealth { get; set ; }
    public float MaxHealth { get; set; }
    public int PointsToGive { get; set; }
    public Transform DamaggableTransform { get; set; }

    [SerializeField] float maxHealthSetter = 10;

    private void OnEnable() {
        PointsToGive = 1;
        CurrentHealth = maxHealthSetter;
        GameEvents.OnGooseDied += Die;
        DamaggableTransform = this.transform;
        //GameEvents.OnGooseHit += TakeDamage;
    }
    public void Die(Unit unit) {
        //TODO we will need to seperate the code out since player death will call more activities
        Destroy(unit.gameObject);
    }

    public void TakeDamage(IDamaggable unit, float  dmg) {
        //? should i make a different player health script so we dont have to do so many checks in other code
        //TODO slow down time when we are hit
        //Debug.Log(this.name + " is hit " + CurrentHealth);
        unit.CurrentHealth -= dmg;
        GameEvents.OnGooseHit(unit);
        if (CurrentHealth <= 0) {
            GameEvents.OnGooseDied(this);
        }


        
    }



    private void OnDestroy() {
        //GameEvents.OnGooseHit -= TakeDamage;
        GameEvents.OnGooseDied -= Die;


    }

    
}
