using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    /*[HideInInspector]*/ public float currentHealth;
    public float maxHealth = 100f;
    public bool isDead;

    public float ExpAmount = 10;

    [SerializeField] GameObject enemyDeathVfx;
    [SerializeField] Transform enemyDeathTransform;

    Animator animator;
    NavMeshAgent agent;

    [SerializeField] private Image enemyHealthBar;

    public static event Action<float> onDeath;
    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth > amount)
        {
            enemyHealthBar.fillAmount = currentHealth / maxHealth;
            animator.SetTrigger("Hit");
        }
        else
        {
            onDeath(ExpAmount);
            enemyHealthBar.fillAmount = 0f;
            isDead = true;
            animator.ResetTrigger("Hit");
            currentHealth = 0;            
            agent.enabled = false;
            animator.SetBool("Death", true);            
            Invoke("GetEnemyDeathEffect", 1f);
            // Enemy öldü.
        }

            
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            TakeDamage(10f);
        }
    }
    private void GetEnemyDeathEffect()
    {
        Instantiate(enemyDeathVfx, enemyDeathTransform.position, Quaternion.identity);
        Destroy(gameObject, 1f);
    }


    
}
