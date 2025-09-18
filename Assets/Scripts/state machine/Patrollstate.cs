using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

public class Patrollstate : StateMachineBehaviour
{
    float timer;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 8f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;

        
        agent = animator.GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("Patrollstate: No NavMeshAgent found on " + animator.gameObject.name);
            return;
        }
        agent.speed = 1.5f;

        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            player = null;
            Debug.LogWarning("Patrollstate: No GameObject with tag 'Player' found!");
        }

       
        if (wayPoints.Count == 0)
        {
            GameObject go = GameObject.FindGameObjectWithTag("WayPoints");
            if (go != null)
            {
                foreach (Transform t in go.transform)
                {
                    wayPoints.Add(t);
                }
            }
            else
            {
                Debug.LogWarning("Patrollstate: No GameObject with tag 'WayPoints' found!");
            }
        }

       
        if (wayPoints.Count > 0)
        {
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        }
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent == null) return;

        
        if (agent.remainingDistance <= agent.stoppingDistance && wayPoints.Count > 0)
        {
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        }

     
        timer += Time.deltaTime;
        if (timer > 10f)
        {
            animator.SetBool("isPatrolling", false);
        }

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
        if (agent != null)
        {
            agent.ResetPath(); 
        }
    }
}
