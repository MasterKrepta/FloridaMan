using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanMove : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed = 2;
    public Vector3 lookOffset = new Vector3(0, 0.75f, 0);
    Transform player;
    bool movingRight = true;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag(TagsAndLayers.Player).transform;
        rb = GetComponent<Rigidbody>();
        GameEvents.OnPlayerDied += DisableScripts;
        GameEvents.OnPlayerAlive += EnableScripts;
    }

    private void Update() {
        Rotation();
        if (player.position.y < 1) { // prevent us from looking in the air
            transform.LookAt(player.position + lookOffset);
        }
        
    }
    void FixedUpdate()
    {
        if (movingRight) {
            rb.velocity = new Vector3(Vector3.right.x * moveSpeed, rb.velocity.y, rb.velocity.z);
        }
        else {
            rb.velocity = new Vector3(-Vector3.right.x * moveSpeed, rb.velocity.y, rb.velocity.z);
        }
    }

    private void Rotation() {
        if (player.transform.position.x > transform.position.x) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            movingRight = true;
        }
        else if (player.transform.position.x < transform.position.x) {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
            movingRight = false;
        }
    }

    void DisableScripts() {
        GetComponent<SwanAttack>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.enabled = false;
    }

    void EnableScripts() {
        GetComponent<SwanAttack>().enabled = true;
        this.enabled = true;
    }

    private void OnDestroy() {
    GameEvents.OnPlayerDied -= DisableScripts;
    GameEvents.OnPlayerAlive -= EnableScripts;
    }
}