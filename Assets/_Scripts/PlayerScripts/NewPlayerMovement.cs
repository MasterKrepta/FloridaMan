using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Animator anim;
    public float walkSpeed = 2f;
    public float zSpeed = 1.5f;

    float rotY = -180f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotatePlayer();
        AnimateWalk();
    }

    private void FixedUpdate() {
        GetInput();
    }

    private void GetInput() {
        rb.velocity = new Vector3(Input.GetAxisRaw(TagsAndLayers.HORIZONTAL) * (walkSpeed),
                rb.velocity.y,
                Input.GetAxisRaw(TagsAndLayers.VERTICAL) * (zSpeed));
    }

    private void RotatePlayer() {
        if (Input.GetAxisRaw(TagsAndLayers.HORIZONTAL) > 0) {
            transform.rotation = Quaternion.Euler(0f, 0, 0f);
        }
        else if (Input.GetAxisRaw(TagsAndLayers.HORIZONTAL) < 0) {
            transform.rotation = Quaternion.Euler(0f, rotY, 0f);

        }
    }

    private void AnimateWalk() {
        if (Input.GetAxisRaw(TagsAndLayers.HORIZONTAL) != 0 || Input.GetAxisRaw(TagsAndLayers.VERTICAL) != 0) {
            anim.SetBool(TagsAndLayers.MOVING, true);
        }
        else {
            anim.SetBool(TagsAndLayers.MOVING, false);
        }
    }
}
