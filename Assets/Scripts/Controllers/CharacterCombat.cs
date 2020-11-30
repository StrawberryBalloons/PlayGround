using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))]
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f; //have this affected by dex?
    private float attackCoolDown = 0f;
    public float attackDelay = .6f;

    float lastAttacktime;
    const float combatCooldown = 5;

    public bool InCombat {get; private set;}
    public event System.Action OnAttack;

    CharacterStats myStats;
    CharacterStats enemyStats;

    void Update()
    {
        attackCoolDown -= Time.deltaTime;
        if(Time.time - lastAttacktime > combatCooldown){
            InCombat = false;
        }
    }

    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCoolDown <= 0)
        {
            enemyStats = targetStats;
            //trigger coroutine if I feel like it
            //StartCoroutine(DoDamage(targetStats, attackDelay));
            //targetStats.TakeDamage(myStats.damage.GetValue(), 1); //need to get armour slot on hit

            if(OnAttack != null){
                OnAttack();
            }

            attackCoolDown = 1f / attackSpeed;
            InCombat = true;
            lastAttacktime = Time.time;
        }
    }
        public void AttackHit_AnimationEvent(){ //this only works with attack hit events, last part
        //of sebs tutorial, I plan on using collision for it though
            
            enemyStats.TakeDamage(myStats.damage.GetValue(), 1); //need to get armour slot on hit
if(enemyStats.currentHealth <= 0){
            InCombat = false;
        } 
    }

//     IEnumerator DoDamager(CharacterStats stats, float delay){ coroutine commented out to have hit event
//         yield return new WaitForSeconds(delay);
// if(stats.currentHealth <= 0){
//             InCombat = false;
//         }
//         //copy targetStats.TakeDamage into this if I feel the need to do so
     //}



}
