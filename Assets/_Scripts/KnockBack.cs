using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float knockbackForce = 5;
    // Start is called before the first frame update
    
    private void OnEnable() {
        GameEvents.OnKnockBack += Knockback;
    }

    void Knockback(Unit unit) {
        //?Trying to do this without RigidBodies
        StartCoroutine(KnockbackMovement(unit, this.gameObject));
        //TODO Smooth out this movement
    }

    IEnumerator KnockbackMovement(Unit unit, GameObject hitDir) {
        //unit.transform.eulerAngles = new Vector3(0,180,0);
        //Vector3 startPos = unit.transform.position;
        unit.GetComponent<Rigidbody>().AddRelativeForce(hitDir.transform.right * knockbackForce);
        //Vector3 knockbackPos = new Vector3(knockbackForce, 0, 0) + startPos;

        //unit.transform.position = Vector3.Slerp(startPos, knockbackPos, 0.25f);
        yield return new WaitForSeconds(.25f);
    }
    private void OnDestroy() {
        GameEvents.OnKnockBack -= Knockback;
    }

}
