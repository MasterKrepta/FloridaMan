using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public LayerMask collLayer;
    public float Radius = 1f;
    Unit unitHit = null;

    private void Update() {
        DetectCollision();
    }

    private void DetectCollision() {
        Collider[] hit = Physics.OverlapSphere(transform.position, Radius, collLayer);

        if (hit.Length > 0) {
            GameEvents.OnEffectMode_ON();
            unitHit = hit[0].GetComponent<Unit>();
            unitHit.GetComponent<BoxCollider>().enabled = false;
            unitHit.transform.parent = this.transform;
            

            //gameObject.SetActive(false);
        }

       
    }



    
}
