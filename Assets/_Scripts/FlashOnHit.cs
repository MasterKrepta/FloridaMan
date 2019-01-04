using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    private float flashTime = .25f;
    Color original;
    Renderer rend;
    private void OnEnable() {
        
        GameEvents.OnGooseHit += StartFlash;
        GameEvents.OnGooseDied += UnRegister;
        
    }

    void StartFlash(Unit unit) {
        if (unit == null) {
            return;
        }
        else{ 
            StartCoroutine(Flash(unit));
        }
        
    }
    IEnumerator Flash(Unit unit) {
        rend = unit.GetComponent<Renderer>();
        original = rend.material.color;
        rend.material.color = Color.red;

        yield return new WaitForSeconds(flashTime);
        if (rend != null) {
            rend.material.color = original;
        }
   
    }

    void UnRegister(Unit unit) {
        GameEvents.OnGooseHit -= StartFlash;
        GameEvents.OnGooseDied -= UnRegister;
    }

}
