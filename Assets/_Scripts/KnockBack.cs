using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float knockbackForce = 5;
        
    private void OnEnable() {
        GameEvents.OnKnockBack += Knockback;
    }

    void Knockback(Unit unit) {
        StartCoroutine(KnockbackMovement(unit, this.gameObject));
    }

    IEnumerator KnockbackMovement(Unit unit, GameObject hitDir) {
        unit.GetComponent<Rigidbody>().AddRelativeForce(hitDir.transform.right * knockbackForce);
        yield return new WaitForSeconds(.25f);
    }
    private void OnDestroy() {
        GameEvents.OnKnockBack -= Knockback;
    }

}
