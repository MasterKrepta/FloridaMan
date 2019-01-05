using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float distToGround;
    Vector3 movement;
    
    //Jumping
    [SerializeField]bool isGrounded = false;
    Rigidbody rb;
    [SerializeField]float Jumpforce = 10f;
    bool facingRight = true;
    [SerializeField] float fallMulti = 2.5f;
    [SerializeField] float lowJumpMulti = 2f;
    [SerializeField] float slideForce = 7f;
    [SerializeField]bool sliding = false;
    private float slideDelay = .5f;


    // Start is called before the first frame update
    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        isGrounded = CheckGrounded();
        //?Movement is disabled while jumping.. This Will affect gameplay
        //TODO Is this still feel good to play
        if (isGrounded) {
            movement = GetInput();
        }
        
        //Remove all control if we are sliding
        if (!sliding) {
            if (movement.x < 0) {
                Flip();
            } else {
                if (movement != Vector3.zero) { // Prevent flipping if we stop while facing left
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

            }

            //transform.Translate(movement * speed * Time.deltaTime);
            if (movement.x != 0 ) {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
            }
            if (Input.GetButtonDown("Jump") && isGrounded) {
                Jump();
            }

            ModifyVelocityWhenJumping();
        }
        if (Input.GetKeyDown(KeyCode.C) && rb.velocity.x != 0 && sliding == false) {
            Slide();
            //TODO Draw Particle Effect
        }
    }

    private void Slide() {
        rb.velocity = transform.right * slideForce;
        StartCoroutine(ToggleSlide());
    }

    IEnumerator ToggleSlide() {
        sliding = true;
        yield return new WaitForSeconds(slideDelay);
        sliding = false;
        rb.velocity = Vector3.zero;
    }

    private void ModifyVelocityWhenJumping() {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
        }
    }

    private void Jump() {
        rb.velocity = Vector3.up * Jumpforce;
    }

    private void Flip() {
        transform.eulerAngles = new Vector3(0, 180, 0);
    }

    private Vector3 GetInput() {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
    }

    bool CheckGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }

}
