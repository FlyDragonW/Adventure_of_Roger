using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack003 : StateMachineBehaviour
{
    public GameObject obstacle,self;
    public float timer,offset;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        self = animator.gameObject;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;
        if(timer >= 0.7f)
        {
            for(int i = 0; i < Random.Range(2, 4); i++)
            {
                timer = 0;
                Vector3 selfPos = self.transform.position;
                Vector3 tempPos = new Vector3(selfPos.x + offset, selfPos.y + 1.3f*Random.Range(-2,3), selfPos.z);
                Instantiate(obstacle, tempPos, obstacle.transform.rotation);
            }
            
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
