using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField] GameObject hitMarker; //FOR TESTING
    [SerializeField] LayerMask hitMask;
    [SerializeField] Transform PunchPoint;
    [SerializeField] float punchRange;
    [SerializeField] float punchDelay;

    [SerializeField] Transform KickPoint;
    [SerializeField] float kickRange;
    [SerializeField] float kickDelay;

    [SerializeField] bool canPunch = true;
    [SerializeField] bool canKick = true;

    [SerializeField] float punchPower = 1;
    [SerializeField] float kickPower = 1.5f;

    Animator anim;
    
    string leftPunch = "LeftPunch";
    string rightPunch = "RightPunch";
    string nextPunch;

    private void Awake() {
        anim = GetComponentInChildren<Animator>();
        
        nextPunch = rightPunch;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetButton("HighAttack") && canPunch) {
            anim.Play(nextPunch);
            TogglePunch(nextPunch);
            RaycastHit hit;
            //Debug.Log("Launch High Attack");
            //Instantiate(hitMarker, PunchPoint.position + new Vector3(punchRange, 0, 0), Quaternion.identity); !TESTING
            if (Physics.Raycast(PunchPoint.position, PunchPoint.right, out hit, punchRange, hitMask)) {
                Unit unitHit = hit.transform.GetComponent<Unit>();

                //Instantiate(hitMarker, hit.point, Quaternion.identity);

                unitHit.TakeDamage(unitHit, punchPower, hit);
                //GameEvents.OnGooseHit(unitHit, punchPower);
            }
            StartCoroutine(PunchCooldown());

        }

        if (Input.GetButton("LowAttack") && canKick) {
            anim.Play("Kicking");
            RaycastHit hit;
            //Debug.Log("Launch Low Attack");
            //Instantiate(hitMarker, KickPoint.position + new Vector3(kickRange,0,0), Quaternion.identity);

            if (Physics.Raycast(KickPoint.position, KickPoint.right, out hit, kickRange, hitMask)) {
                Unit unitHit = hit.transform.GetComponent<Unit>();

                //Instantiate(hitMarker, hit.point, Quaternion.identity);

                unitHit.TakeDamage(unitHit, kickPower, hit);
                GameEvents.OnKnockBack(unitHit);
                //GameEvents.OnGooseHit(hit.collider.GetComponent<Unit>(), kickPower);

            }
            StartCoroutine(KickCooldown());
        }
    }

    private void TogglePunch(string lastPunch) {
        if (lastPunch == rightPunch) {
            nextPunch = leftPunch;
        }
        else {
            nextPunch = rightPunch;
        }
    }

    //private void OnDrawGizmosSelected() {
    //    Color punchColor = Color.red;
    //    Color kickColor = Color.green;
    //    Debug.DrawLine(PunchPoint.position, PunchPoint.position + new Vector3(punchRange, 0, 0), punchColor, .01f);
    //    Debug.DrawLine(KickPoint.eulerAngles, KickPoint.position + new Vector3(kickRange, 0, 0), kickColor, .01f);
    //}

    IEnumerator PunchCooldown() {
        canPunch = false;
        yield return new WaitForSeconds(punchDelay);
        canPunch = true;
    }

    IEnumerator KickCooldown() {
        canKick = false;
        yield return new WaitForSeconds(kickDelay);
        canKick = true;
    }
}

