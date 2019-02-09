using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{
    [SerializeField] GameObject particle;
    [SerializeField] float particleLifetime = 0.2f;
    // Start is called before the first frame update

    private void Start() {
        //!This should work for every goose hit and not need to be unsubed
        GameEvents.OnGooseHit += CreateParatileAtPoint;
        
    }

    void CreateParatileAtPoint(IDamaggable unit) {
        Transform unitHit = unit.DamaggableTransform;
        GameObject go = Instantiate(particle, unitHit.position, Quaternion.identity);
        Destroy(go, particleLifetime);
    }
}