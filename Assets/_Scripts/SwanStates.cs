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
        FEEDING,
        HOSTILE
    }

    public AnimalStates currentState;
    float timeToChangeState = 0;
    [Header("HostileState")]
    [SerializeField] float timeToCalmDown = 5f;
    [SerializeField] float chargeRange = 10f;
    [SerializeField] float chargeSpeed = 1.2f;

    [Header("FeedingState")]
    Vector3 dest;
    float dist;
    [SerializeField]bool moving = false;
    [SerializeField] float timeToFeed = 3f;
    [Range(3,10)] 
    [SerializeField] float feedingDistance = 3;


    private void Start() {
        player = FindObjectOfType<PlayerMovement>().transform;
        currentState = AnimalStates.DOCILE;
        GameEvents.OnGooseHit += this.BecomeHostile; //TODO this is sending the player as a unit which is wrong
    }

    void BecomeHostile(Unit unit, RaycastHit hit) {
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
        if (currentState == AnimalStates.DOCILE && !moving) {
            StartCoroutine(TimeToNextFeeding());
        }
        if (currentState == AnimalStates.FEEDING) {
            Debug.Log("We are feeding");
            moving = false;
            //GetNextFeedingPos();
        }
        if (moving) {
            MoveSwan(dest);
        }

    }

    private void MoveSwan(Vector3 dest) {
        dist = Vector3.Distance(dest, this.transform.position);
        transform.Translate(dest *moveSpeed * Time.deltaTime);
        
        if (dist <= 1) {
            Debug.Log("Done moving");
            currentState = AnimalStates.DOCILE;
            moving = false;
        }
    }

    private void GetNextFeedingPos() {
        int dir = UnityEngine.Random.Range(-1, 1);
        if (dir == 0) {
            dir = 1;
        }
        Debug.Log("New feedingpos");
        float randX = UnityEngine.Random.Range(5, feedingDistance);
        dest = new Vector3(randX * dir, 0, 0).normalized;
        
    }

    IEnumerator TimeToNextFeeding() {
        Debug.Log("Docile");
        currentState = AnimalStates.DOCILE;
        yield return new WaitForSeconds(timeToFeed);
        GetNextFeedingPos();
        moving = true;
        
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
