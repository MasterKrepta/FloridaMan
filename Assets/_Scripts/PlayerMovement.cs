using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float distToGround;
    Vector3 movement;
    [SerializeField]bool isGrounded = false;
    Rigidbody rb;
    [SerializeField]float Jumpforce = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        distToGround = GetComponent<Collider>().bounds.extents.y;
        rb = GetComponent<Rigidbody>();
    }
    private void Update() {
        movement = GetInput();
        isGrounded = CheckGrounded();
        transform.Translate(movement * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.AddForce(Vector3.up * Jumpforce);
        }
    }

    private Vector3 GetInput() {
        return new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
    }

    bool CheckGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);
    }
}
