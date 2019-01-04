using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetButton("HighAttack") && canPunch) {
            RaycastHit hit;
            //Debug.Log("Launch High Attack");

            if (Physics.Raycast(PunchPoint.position, PunchPoint.right, out hit, punchRange)) {
                
                GameEvents.OnGooseHit(hit.collider.GetComponent<Unit>(), punchPower);
            }
            StartCoroutine(PunchCooldown());

        }

        if (Input.GetButton("LowAttack") && canKick) {
            RaycastHit hit;
            //Debug.Log("Launch Low Attack");

            //? Should Kick include a knockback that punch does not (maybe it should fire slower)
            if (Physics.Raycast(KickPoint.position, KickPoint.right, out hit, kickRange)) {
                Debug.Log(hit.collider.GetComponent<Unit>().name);
                GameEvents.OnGooseHit(hit.collider.GetComponent<Unit>(), kickPower);
            }
            StartCoroutine(KickCooldown());
        }
    }

    private void OnDrawGizmosSelected() {
        Color punchColor = Color.red;
        Color kickColor = Color.green;
        Debug.DrawLine(PunchPoint.position, PunchPoint.position + new Vector3(punchRange, 0, 0), punchColor, .01f);
        Debug.DrawLine(KickPoint.position, KickPoint.position + new Vector3(kickRange, 0, 0), kickColor, .01f);
    }

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

