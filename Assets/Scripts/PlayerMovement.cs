using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public bool talking = false;
    public float speed = 5;
    public event Action OnEncounter;
    public LayerMask battleLayer;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void HandleUpdate(){
        if(!talking){
            change = Vector3.zero;
            change.x = Input.GetAxisRaw("Horizontal");
            change.y = Input.GetAxisRaw("Vertical");
            if(change != Vector3.zero){
                MoveCharacter();
                animator.SetFloat("moveX",change.x);
                animator.SetFloat("moveY",change.y);
                animator.SetBool("moving",true);
            }
            else{
                animator.SetBool("moving",false);
            }
            // CheckEncounter();
        }
    }

    void MoveCharacter(){
        myRigidBody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );
    }

    void OnTriggerStay2D(Collider2D col){
        if(col.gameObject.tag == "BattleTag"){
            if(change != Vector3.zero){
                change = Vector3.zero;
                animator.SetBool("moving",false);
                if(UnityEngine.Random.Range(0, 1001) < 10){
                    OnEncounter();
                }
            }
        }
    }
}
