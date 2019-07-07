using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwanAttack : MonoBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float attackPower = 1f;
    [SerializeField] float attackRate = 1.5f;
    
    float nextAttack;
    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag(TagsAndLayers.Player).transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        nextAttack = Time.time + attackRate;
    }

    // Update is called once per frame
    void Update() {
        float dist = Vector3.Distance(player.transform.position, this.transform.position);
            if (dist <= attackRange && Time.time > nextAttack) {
                StartAttack();
            }
    }

    private void StartAttack() {
        RaycastHit hit;
        nextAttack = Time.time + attackRate;
        if (Physics.Raycast(transform.position, transform.right, out hit, attackRange)) {
            playerHealth.TakeDamage(playerHealth, attackPower);
        }
    }
}