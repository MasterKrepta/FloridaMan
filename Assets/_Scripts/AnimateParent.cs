using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateParent : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.applyRootMotion = true;
    }

    private void OnAnimatorMove() {
        if (anim) {
            Vector3 modelOffset = new Vector3(0, 1, 0);
            transform.parent.position = anim.rootPosition + modelOffset;
            //transform.parent.rotation = anim.rootRotation;
        }

    }
}
