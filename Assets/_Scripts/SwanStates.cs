using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanStates : MonoBehaviour
{
    Transform player;
    [SerializeField] float moveSpeed;
    public enum AnimalStates
    {
        DOCILE,
        HOSTILE
    }

    public AnimalStates currentState;
    float timeToChangeState = 0;
    [Header("HostileState")]
    [SerializeField] float timeToCalmDown = 5f;
    [SerializeField] float chargeRange = 10f;
    [SerializeField] float chargeSpeed = 1.2f;

    [Header("DocileState")]
    [SerializeField] bool canMove = false;
    [SerializeField] Vector3 dest = Vector3.zero;
    [SerializeField]float distToGo;
    float randomDistance;
    Vector3 velocity;
    [SerializeField] bool movingRight = true;
    Rigidbody rb;
    [Range(3,10)] 
    [SerializeField] float moveDistance = 3;
    [SerializeField] float waitTime = 5f;

    private void Start() {
        player = FindObjectOfType<PlayerMovement>().transform;
        rb = GetComponent<Rigidbody>();
        currentState = AnimalStates.DOCILE;
        GameEvents.OnGooseHit += this.BecomeHostile; //TODO this is sending the player as a unit which is wrong
    }

    void BecomeHostile(Unit unit) {
        //TODO see note above to fix this
        if (unit.GetComponent<SwanStates>() != null) {
            unit.GetComponent<SwanStates>().currentState = AnimalStates.HOSTILE;
            timeToChangeState = Time.deltaTime + timeToCalmDown;
            StartCoroutine(ResetState(timeToChangeState));
        }
    }

    private void Update() {
        if (currentState == AnimalStates.HOSTILE) {
            HostileState();
        }
        if (currentState == AnimalStates.DOCILE) {
            if (!canMove) {
                Debug.Log("Need new pos");
                GetNextPosition();
            }

            if (canMove) {
                MoveSwan();
            }        
                
        }
    }

    private void MoveSwan() {
        velocity = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        if (movingRight) {
            transform.eulerAngles = Vector3.zero;
            distToGo = Vector3.Distance(dest, this.transform.position);
            distToGo = Mathf.Abs(distToGo);
            
            transform.Translate(velocity);
            if (distToGo <= 1) {
                dest = this.transform.position;

                StartCoroutine(WaitForNextMove());
            }
        }
        else {
            transform.eulerAngles = new Vector3(0, 180, 0);
            distToGo = Vector3.Distance(dest, this.transform.position);
            distToGo = Mathf.Abs(distToGo);
            transform.Translate(-velocity);
            if (distToGo >= 1) {
                dest = this.transform.position;

                StartCoroutine(WaitForNextMove());
            }
        }
        
      
    }

    IEnumerator WaitForNextMove() {
        dest = Vector3.zero;
        canMove = false;
        yield return new WaitForSeconds(waitTime);
        canMove = true;
        GetNextPosition();
    }

    private void GetNextPosition() {
        movingRight = (UnityEngine.Random.value > 0.5f);
        randomDistance = UnityEngine.Random.Range(5, moveDistance);
        //dest = transform.position + transform.right * randomDistance;
        //dest = new Vector3(Mathf.Round( randX), 0, 0);
        
    }



    private void HostileState() {
        Debug.Log("Hostile");
        //TODO this is messing up the position of the gameobject in the Z axis
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
        if (dist <= chargeRange && !player.GetComponent<PlayerMovement>().sliding) {
            transform.position = Vector3.Slerp(transform.position, player.transform.position, chargeSpeed * Time.deltaTime);
        }
        else if (player.GetComponent<PlayerMovement>().sliding || !player.GetComponent<Jump>().isGrounded) { //TODO this is pretty hacky
            Vector3 dir = transform.rotation * Vector3.right;
            transform.position += dir * chargeSpeed * Time.deltaTime;
        }
    }

    IEnumerator ResetState(float timeToChangeState) {
        yield return new WaitForSeconds(timeToCalmDown);
            currentState = AnimalStates.DOCILE;
    }

    private void OnDestroy() {
        GameEvents.OnGooseHit -= this.BecomeHostile; 
    }
}
