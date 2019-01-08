using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    private float flashTime = .2f;
    [SerializeField]Color original;
    [SerializeField] Color HitColor;
    Renderer rend;

    private void OnEnable() {
        original = GetComponentInChildren<Renderer>().material.color;
        GameEvents.OnGooseHit += StartFlash;
    }

    void StartFlash(Unit unit, RaycastHit hit) {
        StartCoroutine(Flash(unit));
        
    }
    IEnumerator Flash(Unit unit) {
        rend = unit.GetComponentInChildren<Renderer>();
        original = unit.GetComponent<FlashOnHit>().original;

        rend.material.color = HitColor;
        yield return new WaitForSeconds(flashTime);
        if (rend != null) {
            
            rend.material.color = original;
        }
    }

    void OnDestroy() {
        GameEvents.OnGooseHit -= this.StartFlash;
    }
}
