using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateMachineBehaviour
{
    Transform player;
    PlayerHealth playerHealth;
    float attackCooldown = 1.0f; 
    float lastAttackTime;

    public float attackDamage = 10f; 

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
            playerHealth = playerObject.GetComponent<PlayerHealth>();
            Debug.Log("player found");
        }
         Debug.Log("player health changed");
        lastAttackTime = Time.time - attackCooldown;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (player == null || playerHealth == null) return;
    
      
        animator.transform.LookAt(player);

      
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 3f)
        {
            animator.SetBool("isAttacking", false);
            return;
        }

       
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            playerHealth.TakeDamage(attackDamage);
            lastAttackTime = Time.time; 
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Optionally, you can reset any state variables here
    }
}
