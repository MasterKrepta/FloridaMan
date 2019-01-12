using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    private float flashTime = .2f;
    [SerializeField]Color original;
    [SerializeField] Color HitColor;


    private void OnEnable() {
        original = GetComponentInChildren<Renderer>().material.color;
        GameEvents.OnGooseHit += StartFlash;
    }


    void StartFlash(Unit unit, RaycastHit hit) {
        StartCoroutine(Flash(unit));
    }
    IEnumerator Flash(Unit unit) {
        Renderer[] renderers = unit.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.material.color = HitColor;
        }
        original = unit.GetComponent<FlashOnHit>().original;
        yield return new WaitForSeconds(flashTime);
        foreach (Renderer r in renderers) {
            if (r != null) {
                r.material.color = original;
            }
        }
    }

    void OnDestroy() {
        GameEvents.OnGooseHit -= this.StartFlash;
    }
}
