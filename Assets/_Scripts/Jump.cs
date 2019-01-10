using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public bool isGrounded = false;
    [SerializeField]float distToGround;
    Rigidbody rb;
    Animator anim;
    [SerializeField] float fallMulti = 10f;
    [SerializeField] float lowJumpMulti = 2f;
    [SerializeField] float Jumpforce = 10f;
    [SerializeField] float initialJump = 20f;
    
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private void ActivateJump() {
        anim.Play("Jump");
        rb.velocity = Vector3.up * Jumpforce;
    }

    private void Update() {
        isGrounded = CheckGrounded();
        anim.SetBool("isGrounded", isGrounded);
        if (Input.GetButtonDown("Jump") && isGrounded) {
            ActivateJump();
        }
        ModifyVelocityWhenJumping();
    }
    private void ModifyVelocityWhenJumping() {
        if (rb.velocity.y < 0 ) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMulti - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump")) {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMulti - 1) * Time.deltaTime;
        }
    }
    bool CheckGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}