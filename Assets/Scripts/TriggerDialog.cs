using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialog : MonoBehaviour
{
    public Vector3 position;
    public float speed = 5;
    private Npc npc;
    private Rigidbody2D myRigidBody;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        npc = GetComponent<Npc>();
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("ParamY",-1f);
        animator.SetFloat("ParamX",0f);
    }

    void Move(){
        myRigidBody.MovePosition(
            transform.position + position * speed * Time.deltaTime
        );
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            // position = col.transform.position;
            // Move();
            npc.dialogManager.Conversation();
        }
    }
}
