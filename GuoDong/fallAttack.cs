using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallAttack : StateMachineBehaviour
{
    SpriteRenderer guoDongRend;
    BoxCollider2D guoDongColl;
    public GameObject fallGd;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        guoDongRend = animator.GetComponent<SpriteRenderer>();
        guoDongColl = animator.GetComponent<BoxCollider2D>();
        guoDongRend.enabled = false;
        guoDongColl.enabled = false;
        GameObject temp = Instantiate(fallGd, new Vector3(Random.Range(114f,125f),-22,0), fallGd.transform.rotation);
        Destroy(temp, 1.2f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        guoDongRend.enabled = true;
        guoDongColl.enabled = true;
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
