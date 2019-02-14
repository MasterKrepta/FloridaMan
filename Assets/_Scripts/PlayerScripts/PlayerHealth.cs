using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamaggable
{
public float CurrentHealth { get; set ; }
    public float MaxHealth { get; set; }
    public Transform DamaggableTransform { get; set; }

    [SerializeField] float maxHealthSetter = 50;

    private void OnEnable() {
        ResetHealthOnRespawn();
        GameEvents.OnPlayerDied += PlayerDeath;
        GameEvents.OnPlayerRespawn += ResetHealthOnRespawn;
        DamaggableTransform = this.transform;
        //GameEvents.OnGooseHit += TakeDamage;
        
    }
    public void Die(Unit unit) {
        
    }

    void PlayerDeath() {
        //this.gameObject.GetComponent<Renderer>().enabled = false;
        //GameObject player = FindObjectOfType<PlayerMovement>().gameObject;
        Renderer[] rends = this.GetComponentsInChildren<Renderer>();
      

        foreach (Renderer renderer in rends) {
            renderer.enabled = false;
        }
        MonoBehaviour[] allScripts = this.GetComponents<MonoBehaviour>();
        foreach (MonoBehaviour m in allScripts) {
            m.enabled = false;
        }
        //this.gameObject.SetActive(false);
    }

    public void TakeDamage(IDamaggable unit, float  dmg) {
        
        //TODO slow down time when we are hit
        //Debug.Log(this.name + " is hit " + CurrentHealth);
        unit.CurrentHealth -= dmg;
        GameEvents.OnPlayerHit(unit);
        if (CurrentHealth <= 0) {
            GameEvents.OnPlayerDied();
            GameEvents.OnPlayerRespawn();
        }


        
    }

    void ResetHealthOnRespawn() {
        CurrentHealth = maxHealthSetter;
    }


    private void OnDestroy() {
        //GameEvents.OnGooseHit -= TakeDamage;
        GameEvents.OnPlayerDied -= PlayerDeath;
        GameEvents.OnPlayerRespawn -= ResetHealthOnRespawn;


    }
}
