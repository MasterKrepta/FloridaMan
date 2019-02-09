using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float distToGround;
    Vector3 movement;
    Animator anim;
    //Jumping
    Jump jump;
    Rigidbody rb;
    BoxCollider collider;
    BoxCollider slideCollider;

    
    [SerializeField] float slideForce = 7f;
    public bool sliding = false;
    private float slideDelay = .75f;
    
    void Start()
    {
        jump = GetComponent<Jump>();
        collider = GetComponent<BoxCollider>();
        
        anim = GetComponentInChildren<Animator>();
        slideCollider = anim.gameObject.GetComponent<BoxCollider>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        Rotation();
    }
    private void FixedUpdate() {
        movement = GetInput();
        
        //Remove all control if we are sliding
        if (!sliding) {
            //if (movement.x < 0) {
            //    Flip();
            //} else {
            //    if (movement != Vector3.zero) { // Prevent flipping if we stop while facing left
            //        transform.eulerAngles = new Vector3(0, 0, 0);
            //    }
            //}

            if (movement.x != 0) {
                anim.SetBool(TagsAndLayers.MOVING, true);
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            else {
                anim.SetBool(TagsAndLayers.MOVING, false);
            }

            
        }
        if (jump.isGrounded && Input.GetKeyDown(KeyCode.C) && /* rb.velocity.x != 0 && */ sliding == false) {
            Slide();
            //TODO Draw Particle Effect
        }
    }

    private void Slide() {
        StartCoroutine(ToggleSlide());
    }

    IEnumerator ToggleSlide() {
        ToggleCollider();
        sliding = true;
        anim.Play("Slide");
        yield return new WaitForSeconds(slideDelay);
        sliding = false;
        ToggleCollider();
    }

    private void ToggleCollider() {
        collider.isTrigger = !collider.isTrigger;
        slideCollider.enabled = !slideCollider.enabled;
        //rb.useGravity = !rb.useGravity;
    }

    private void Rotation() {
        if (Input.GetAxisRaw("Horizontal") > 0) {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) {
            transform.rotation = Quaternion.Euler(0, -180f, 0);
        }
    }

    private Vector3 GetInput() {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
    }  
}