using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Variables")]
    [SerializeField] private GameObject attackArea;
    private bool isAttacking;
    [SerializeField] public float timeToAttack;
    [SerializeField] public float timeBetweenAttacks;

    private float timerAttack = 0;
    private float timerBetweenAttacks = 0;

    void Update()
    {
        BasicAttackTimer();
    }

    private void BasicAttack()
    {
        isAttacking = true;
        attackArea.SetActive(true);
    }

    private void BasicAttackTimer()
    {
        if (isAttacking)
        {
            timerAttack += Time.deltaTime;

            if (timerAttack >= timeToAttack)
            {
                timerAttack = 0;
                isAttacking = false;
                attackArea.SetActive(false);
            }
        }
        else if (!isAttacking)
        {
            timerBetweenAttacks += Time.deltaTime;

            if (timerBetweenAttacks >= timeBetweenAttacks)
            {
                timerBetweenAttacks = 0;
                BasicAttack();
            }
        }
    }
}
