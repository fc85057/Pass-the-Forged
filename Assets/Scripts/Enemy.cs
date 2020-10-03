using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField]
    private EnemyStats stats;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject impactEffect;

    private int currentHealth;
    private bool isDead;
    private float nextAttackTime;

    private Animator animator;
    private Transform target;

    private void Start()
    {
        currentHealth = stats.maxHealth;
        SetColors();

        animator = GetComponent<Animator>();
        target = FindObjectOfType<Viking>().transform;
    }

    private void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            StartCoroutine(Die());
        }
        else if (!isDead)
        {
            ChooseAction();
        }


    }

    private void ChooseAction()
    {
        bool vikingFound = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 2.5f);
        foreach (Collider2D hit in hits)
        {
            Viking viking = hit.GetComponent<Viking>();
            // if (viking != null && Time.time > nextAttackTime)
            if (viking == GameManager.Instance.CurrentViking && Time.time > nextAttackTime)
            {
                vikingFound = true;
                Attack();
                nextAttackTime = Time.time + 1f;
            }
        }

        if (vikingFound)
            return;

        hits = Physics2D.OverlapCircleAll(transform.position, 7f);
        foreach (Collider2D hit in hits)
        {
            Viking viking = hit.GetComponent<Viking>();
            // if (viking != null)
            if (viking == GameManager.Instance.CurrentViking)
            {
                vikingFound = true;
                Move();
            }
        }

        if (!vikingFound)
        {
            animator.SetFloat("Speed", 0f);
        }

    }

    private void Attack()
    {
        animator.SetTrigger("Melee");
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, 0.4f);
        foreach (Collider2D hit in hits)
        {
            Viking viking = hit.GetComponent<Viking>();
            // if (viking != null)
            if (viking == GameManager.Instance.CurrentViking)
            {
                viking.TakeDamage(stats.damage, stats.element);
                Instantiate(impactEffect, attackPoint.position, Quaternion.identity);
            }
        }
    }

    private void Move()
    {
        animator.SetFloat("Speed", 1f);
        target = GameManager.Instance.CurrentViking.transform;
        transform.position = Vector2.MoveTowards(transform.position, target.position, stats.speed * Time.deltaTime);
    }

    private IEnumerator Die()
    {
        animator.SetTrigger("Die");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void SetColors()
    {

        SpriteRenderer[] srs = GetComponentsInChildren<SpriteRenderer>();
        Color color;

        switch(stats.element)
        {
            case (Element.Fire):
                {
                    color = Color.red;
                    break;
                }
            case (Element.Ice):
                {
                    color = Color.blue;
                    break;
                }
            case (Element.Poison):
                {
                    color = Color.green;
                    break;
                }
            case (Element.Thunder):
                {
                    color = Color.yellow;
                    break;
                }
            default:
                {
                    return;
                }

        }

        foreach (SpriteRenderer sr in srs)
        {
            sr.color = color;
        }

    }

    public void TakeDamage(int damageAmount)
    {
        AudioManager.Instance.PlaySound("EnemyHit");
        currentHealth -= damageAmount;
        Debug.Log("After taking " + damageAmount + " damage, health is " + currentHealth);
    }

    public void SetStats(EnemyStats newStats)
    {
        stats = newStats;
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 2.5f);
        Gizmos.DrawWireSphere(transform.position, 7f);
        Gizmos.DrawWireSphere(attackPoint.position, .4f);
    }


}
