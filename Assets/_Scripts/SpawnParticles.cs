using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnParticles : MonoBehaviour
{

    [SerializeField] GameObject particle;
    [SerializeField] float particleLifetime;
    // Start is called before the first frame update

    private void Start() {
        //!This should work for every goose hit and not need to be unsubed
        GameEvents.OnGooseHit += CreateParatileAtPoint;
        
    }

    void CreateParatileAtPoint(Unit unit) {
        GameObject go = Instantiate(particle, unit.transform.position, Quaternion.identity);
        Destroy(go, particleLifetime);
    }

    
}
