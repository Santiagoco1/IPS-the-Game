using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasingBehaviour : StateMachineBehaviour
{
    public float speed;
    Transform player;
    Controller2D controller;
    Vector2 moveVector;
    float gravity=-20f;
    float dampAcceleration=.2f;
    float xDisplacementSmoothing;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player=GameObject.FindGameObjectWithTag("Player").transform;
        controller=animator.GetComponent<Controller2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float direction=Mathf.Sign(player.position.x-animator.transform.position.x);
        float xDisplacement= direction * speed;

        if(controller.collisions.above || controller.collisions.below){
            moveVector.y=0;
        }

        moveVector.x = Mathf.SmoothDamp(moveVector.x,xDisplacement,ref xDisplacementSmoothing,dampAcceleration);
        moveVector.y+=gravity*Time.deltaTime;
        controller.Move(moveVector);
    
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
