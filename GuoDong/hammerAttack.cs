using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hammerAttack : StateMachineBehaviour
{
    public GameObject hammer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int i = Random.Range(0, 3);
        float x = Random.Range(118f, 122f);
        Vector3 pos = new Vector3(x, -20, 0);
        Vector3 posL = new Vector3(x - 7, -20, 0);
        Vector3 posR = new Vector3(x + 7, -20, 0);
        switch (i)
        {
            case 0:
                Instantiate(hammer, pos, hammer.transform.rotation);
                break;
            case 1:
                Instantiate(hammer, posL, hammer.transform.rotation);
                Instantiate(hammer, posR, hammer.transform.rotation);
                break;
            case 2:
                Instantiate(hammer, pos, hammer.transform.rotation);
                Instantiate(hammer, posL, hammer.transform.rotation);
                Instantiate(hammer, posR, hammer.transform.rotation);
                break;
        }
        
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("hammerAttack");
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
