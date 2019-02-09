using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStates : MonoBehaviour
{
    [SerializeField] GameObject RightHand, LeftHand, RightLeg, LeftLeg, GrabPoint;

    SwanMove move = null;
    SwanAttack attack = null;
    BoxCollider swanCollider = null;
    Rigidbody rb;
    void RightHand_ON() {
        RightHand.SetActive(true);
    }
    void RightHand_OFF() {
        RightHand.SetActive(false);
    }
    void LeftHand_ON() {
        LeftHand.SetActive(true);
    }
    void LeftHand_OFF() {
        LeftHand.SetActive(false);
    }

    void RightLeg_ON() {
        RightLeg.SetActive(true);
    }
    void RightLeg_OFF() {
        RightLeg.SetActive(false);
    }

    void LeftLeg_ON() {
        LeftLeg.SetActive(true);
    }
    void LeftLeg_OFF() {
        LeftLeg.SetActive(false);
    }

    void Grab_ON() {
        GrabPoint.SetActive(true);
    }

    void Grab_OFF() {
        //TODO Get a grab buffer working
        float grabBuffer = 2f;
        
     
        Transform grabPoint = FindObjectOfType<Grab>().transform;
        Quaternion origRot;
        try {
            Transform child = grabPoint.GetChild(0);
            try {
                attack = child.GetComponent<SwanAttack>();
                move = child.GetComponent<SwanMove>();
                swanCollider = child.GetComponent<BoxCollider>();
                rb = child.GetComponent<Rigidbody>();
                attack.enabled = false;
                move.enabled = false;
                swanCollider.enabled = false;
                rb.velocity = Vector3.zero;
            }
            catch (System.Exception) {
                //todo  This wont work with any type that doesnt have the swan scripts on it
                //Consider changing for more enemy types
                Debug.LogWarning("ATTACK: "+ attack + " : OR MOVE " + move + " : IS NULL!! ARE YOU SURE YOU GRABBED A SWAN?");
                throw;
            }

            origRot = child.rotation;

            grabPoint.DetachChildren();

            child.rotation = Quaternion.identity;

            if (grabPoint.parent.forward.x > child.position.x) {
            
                grabBuffer *= -1;
            }
            child.rotation = origRot;
            child.transform.position = new Vector3(child.transform.position.x  /*+ grabBuffer*/, 0.77f, 0);
            StartCoroutine(ResetAttackAndMove());

        }
        catch (System.Exception) {

        }
     
        
        GrabPoint.SetActive(false);
    }

    IEnumerator ResetAttackAndMove() {
        yield return new WaitForSeconds(1);
        attack.enabled = true;
        move.enabled = true;
        swanCollider.enabled = true;
    }

}
