using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedBehaviour : StateMachineBehaviour
{
    public Vector2 knockback;
    float dampAcceleration=.2f;
    Controller2D controller;
    float xDisplacementSmoothing;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        controller=animator.GetComponent<Controller2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float xDisplacement=animator.transform.position.x+knockback.x;

        knockback.x = Mathf.SmoothDamp(knockback.x,xDisplacement,ref xDisplacementSmoothing,dampAcceleration);
        controller.Move(knockback);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
