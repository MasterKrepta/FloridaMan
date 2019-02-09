using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnims : MonoBehaviour
{
    Animator anim;

    private void Awake() {
        anim = GetComponent<Animator>();
    }

    public void ActivatePunch(string punchTrigger) {
        anim.SetTrigger(punchTrigger);
    }


    public void ActivateKick() {
        anim.SetTrigger(TagsAndLayers.KICKING);
    }
    public void ActivateStab() {
        anim.SetTrigger(TagsAndLayers.STABBING);
    }

    public void SetMoving() {
        anim.SetBool(TagsAndLayers.MOVING, true);
    }

    public void SetIdle() {
        anim.SetBool(TagsAndLayers.MOVING, false);
    }
}
