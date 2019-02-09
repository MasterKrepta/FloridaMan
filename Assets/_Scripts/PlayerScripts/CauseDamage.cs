using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauseDamage : MonoBehaviour
{
    public LayerMask collLayer;
    public float Radius = 1f;
    public float Damage = 2f;
    public GameObject hitFX;

    private void Update() {
        DetectCollision();
    }

    private void DetectCollision() {
        Collider[] hit = Physics.OverlapSphere(transform.position, Radius, collLayer);

        if (hit.Length > 0) {
            Unit unitHit = hit[0].GetComponent<Unit>();
            Vector3 hitfxPos = hit[0].transform.position;
            hitfxPos.y += 1.3f;

            if (hit[0].transform.forward.x > 0) {
                hitfxPos.x += 0.3f;
            }
            else if (hit[0].transform.forward.x < 0) {
                hitfxPos.x -= 0.3f;
            }

            hit[0].GetComponent<Unit>().TakeDamage(unitHit, Damage);
      
            //Debug.Log("We hit " + hit[0].name + " with the attack point: -> " + this.name);

            gameObject.SetActive(false);
        }
    }
}
