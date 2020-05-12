using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Controller2D))]
[RequireComponent (typeof(MeleeCombat))]
public class PlayerInput : MonoBehaviour
{
    public float moveSpeed=6;
    public float jumpHeight=10;
    public float timeToJumpApex=.4f;
    public float Damage=1;
    public float maxTimeBtwAttack=0.5f;

    float timeBtwAttack;
    float dampAccelerationGrounded=.1f;
    float dampAccelerationAirborne=.2f;
    float xDisplacementSmoothing;
    float gravity;
    float jumpDisplacement;

    Vector2 moveVector;

    Controller2D controller;
    MeleeCombat meleeCombat;
    Animator anim;


    void Start()
    {
        controller=GetComponent<Controller2D>();
        meleeCombat=GetComponent<MeleeCombat>();
        anim=GetComponent<Animator>();
        timeBtwAttack=maxTimeBtwAttack;
    }

    void Update()
    {
        gravity=-(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
        jumpDisplacement=Mathf.Abs(gravity)*timeToJumpApex;

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));

        if(controller.collisions.above || controller.collisions.below){
            moveVector.y=0;
        }
        if(Input.GetButtonDown("Jump") && controller.collisions.below){
            moveVector.y = jumpDisplacement;
        }

        anim.SetBool("grounded",controller.collisions.below);

        anim.SetBool("isPunching",false);   

        if(Input.GetKeyDown(KeyCode.C) && timeBtwAttack<=0){
            anim.SetBool("isPunching",true);
            meleeCombat.Attack();
            timeBtwAttack=maxTimeBtwAttack;
        }else if(timeBtwAttack>0){
            timeBtwAttack-=Time.deltaTime; 
        }else if(!Input.GetKeyDown(KeyCode.C)){
            
            
        }

        
        float xDisplacement = input.x * moveSpeed;
        float dampAcceleration = (controller.collisions.below) ? dampAccelerationGrounded : dampAccelerationAirborne;

        moveVector.x = Mathf.SmoothDamp(moveVector.x,xDisplacement,ref xDisplacementSmoothing,dampAcceleration);
        moveVector.y += gravity*Time.deltaTime;

        anim.SetBool("isRunning",input.x!=0);

        controller.Move(moveVector*Time.deltaTime);
    }
}
