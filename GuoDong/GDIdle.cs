using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GDIdle : StateMachineBehaviour
{
    public float swCD;
    float timer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
        swCD = Random.Range(0.8f, 1.2f);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Dialog.isDiaPausing) return;

        timer += Time.deltaTime;
        if(timer >= swCD)
        {
            int mode = Random.Range(0, 3);
            if (mode == 0) animator.SetTrigger("Lwall");
            else if (mode == 1) animator.SetTrigger("Rwall");
            else animator.SetTrigger("hammerAttack");
            timer = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
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
