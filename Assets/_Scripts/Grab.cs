using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{

    public LayerMask collLayer;
    public float Radius = 1f;

    private void Update() {
        DetectCollision();
    }

    private void DetectCollision() {
        Collider[] hit = Physics.OverlapSphere(transform.position, Radius, collLayer);

        if (hit.Length > 0) {
            Unit unitHit = hit[0].GetComponent<Unit>();
            

            //gameObject.SetActive(false);
        }
    }
}
