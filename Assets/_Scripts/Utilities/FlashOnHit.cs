using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashOnHit : MonoBehaviour
{
    private float flashTime = .1f;
    [SerializeField]Color original;
    [SerializeField] Color HitColor;


    private void OnEnable() {
        original = GetComponentInChildren<Renderer>().material.color;
        HitColor = Color.red;
        GameEvents.OnGooseHit += StartFlash;
        GameEvents.OnPlayerHit += StartFlash;
    }


    void StartFlash(IDamaggable unit) {
        StartCoroutine(Flash(unit));
    }
    IEnumerator Flash(IDamaggable unit) {
        Renderer[] renderers = unit.DamaggableTransform.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers) {
            r.material.color = HitColor;
        }
        original = unit.DamaggableTransform.GetComponent<FlashOnHit>().original;
        yield return new WaitForSeconds(flashTime);
        foreach (Renderer r in renderers) {
            if (r != null) {
                r.material.color = original;
            }
        }
    }

    void OnDestroy() {
        GameEvents.OnGooseHit -= this.StartFlash;
        GameEvents.OnPlayerHit -= this.StartFlash;
    }
}
