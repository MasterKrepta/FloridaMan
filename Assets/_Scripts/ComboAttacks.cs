
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttacks : MonoBehaviour
{
    CombatAnims anims;
    [SerializeField] float attackDelay = 1f;
    
    bool leftPunch = true;
    [SerializeField]bool canAttack = true;

    void Awake() {
        anims = GetComponentInChildren<CombatAnims>();
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetButton("HighAttack") && canAttack) {
            string p = GetPunch();
            anims.ActivatePunch(p);
            StartCoroutine(AttackCooldown());
        }
        //? Note that kicking cannot be held down
        if (Input.GetButtonDown("LowAttack") && canAttack) {
            StartCoroutine(AttackCooldown());
            anims.ActivateKick();
        }
        if (Input.GetButtonDown("Stabbing") && canAttack) {
            StartCoroutine(AttackCooldown());
            anims.ActivateStab();
        }
    }

    private string GetPunch() {
        if (leftPunch) {
            leftPunch = false;
            return TagsAndLayers.LEFTPUNCH;
        }
        else {
            leftPunch = true;
            return TagsAndLayers.RIGHTPUNCH;
        }
    }

    IEnumerator AttackCooldown() {
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }
}