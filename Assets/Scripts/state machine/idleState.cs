using UnityEngine;

public class idleState : StateMachineBehaviour
{
    float timer;
    Transform player;
    float chaseRange = 8f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;

        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            player = null;
            Debug.LogWarning("idleState: No GameObject with tag 'Player' found in the scene!");
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer += Time.deltaTime;

        if (timer > 2f)
            animator.SetBool("isPatrolling", true);

        
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, animator.transform.position);
            if (distance < chaseRange)
            {
                animator.SetBool("isChasing", true);
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        timer = 0;
    }
}