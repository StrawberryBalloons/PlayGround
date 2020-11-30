using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10;
    Transform target;
    NavMeshAgent agent;
    CharacterCombat combat;

    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                //face target
                FaceTarget();
                //attack target
                if (targetStats != null)
                {
                    combat.Attack(targetStats);
                }

            }
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized; //direction towards target
        Quaternion lookRotation = Quaternion.LookRotation(direction); //rotation to be facing target
        //smooth interpolation towards a direction
        float rotSpeed = 5f;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
