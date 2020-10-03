using System;
using System.Collections;
using UnityEngine;

public class Viking : MonoBehaviour
{

    public static event Action<int, int> OnHealthChanged;
    public static event Action<int, int> OnStaminaChanged;

    [SerializeField]
    private VikingStats stats;
    [SerializeField]
    private Transform attackPoint;
    [SerializeField]
    private GameObject impactEffect;
    [SerializeField]
    private float attackRadius = .5f;
    [SerializeField]
    private int staminaCost = 10;

    private int currentHealth;
    private int currentStamina;

    private float currentMovement;
    Vector3 facingRight = new Vector3(0f, 0f, 0f);
    Vector3 facingLeft = new Vector3(0f, 180f, 0f);
    Vector2 newPosition;

    private Animator animator;
    private Rigidbody2D rb;

    private bool isGrounded;
    private bool isDead;
    private bool isCharging;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask ground;

    public VikingStats Stats { get { return stats; } }

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        attackPoint.parent.gameObject.SetActive(true);

        currentHealth = stats.maxHealth;
        currentStamina = stats.maxStamina;

        if (this == GameManager.Instance.CurrentViking)
        {
            OnHealthChanged?.Invoke(currentHealth, stats.maxHealth);
            OnStaminaChanged?.Invoke(currentStamina, stats.maxStamina);
        }
        //OnHealthChanged(currentHealth, stats.maxHealth);
        //OnStaminaChanged(currentStamina, stats.maxStamina);

        Hammer.OnEnemyHit += DealRangeDamage;
        WinTrigger.OnWinTriggerEntered += GameWin;
    }

    private void Update()
    {
        if (currentHealth < 0 && !isDead)
        {
            Die();
        }

        if (currentStamina < stats.maxStamina && !isCharging)
        {
            StartCoroutine(ChargeStamina());
            isCharging = true;
        }
    }
/*
    private void OnEnable()
    {
        if (this == GameManager.Instance.CurrentViking)
        {
            OnHealthChanged?.Invoke(currentHealth, stats.maxHealth);
            OnStaminaChanged?.Invoke(currentStamina, stats.maxStamina);
        }
        
    }
*/
    private void OnDisable()
    {
        StopAllCoroutines();
        attackPoint.parent.gameObject.SetActive(false);
        Hammer.OnEnemyHit -= DealRangeDamage;
    }

    private IEnumerator ChargeStamina()
    {
        while (currentStamina < stats.maxStamina)
        {
            currentStamina += 5;
            OnStaminaChanged(currentStamina, stats.maxStamina);
            yield return new WaitForSeconds(1f);
        }
        isCharging = false;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        Debug.Log("Game over.");
        this.enabled = false;
    }

    private void FixedUpdate()
    {

        currentMovement = Input.GetAxisRaw("Horizontal");
        if (currentMovement != 0)
        {
            Move();
        }
        else
        {
            animator.SetFloat("Speed", currentMovement);
        }

        if (Input.GetButtonDown("Fire1") && currentStamina >= staminaCost)
        {
            MeleeAttack();
        }

        if (Input.GetButtonDown("Fire2") && currentStamina >= staminaCost)
        {
            RangeAttack();
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentStamina >= staminaCost)
        {
            Jump();
        }

    }

    private void Move()
    {
        if (currentMovement > 0)
        {
            transform.eulerAngles = facingRight;
        }
        else
        {
            transform.eulerAngles = facingLeft;
        }

        animator.SetFloat("Speed", Math.Abs(currentMovement));
        newPosition = new Vector2(transform.position.x + currentMovement, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, newPosition, stats.movementSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.5f, ground);

        if (isGrounded)
        {
            animator.SetTrigger("Jump");
            AudioManager.Instance.PlaySound("Jump");
            rb.velocity = Vector2.up * stats.jumpForce;
            currentStamina -= staminaCost;
            OnStaminaChanged(currentStamina, stats.maxStamina);
        }


    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }

    private void MeleeAttack()
    {
        animator.SetTrigger("Melee");
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius);

        foreach (Collider2D hit in hits)
        {

            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(stats.meleeDamage);
                Instantiate(impactEffect, attackPoint.position, Quaternion.identity);
            }
        }
        currentStamina -= staminaCost;
        OnStaminaChanged(currentStamina, stats.maxStamina);
    }

    private void RangeAttack()
    {
        
        animator.SetTrigger("Range");
        currentStamina -= staminaCost;
        OnStaminaChanged(currentStamina, stats.maxStamina);
    }

    private void DealRangeDamage(Enemy enemy)
    {
        Instantiate(impactEffect, attackPoint.position, Quaternion.identity);
        enemy.TakeDamage(stats.rangeDamage);
    }

    public void Heal(int healAmount)
    {
        Debug.Log("Health before healing: " + currentHealth);
        currentHealth += (healAmount * stats.healing / 100);
        if (currentHealth > stats.maxHealth) currentHealth = stats.maxHealth;
        AudioManager.Instance.PlaySound("Potion");
        OnHealthChanged(currentHealth, stats.maxHealth);
        Debug.Log("Health after healing: " + currentHealth);
    }

    public void TakeDamage(int damageAmount)
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        AudioManager.Instance.PlaySound("Hit");
        currentHealth -= damageAmount;
        Debug.Log("Viking took " + damageAmount + " damage and have " + currentHealth + " health left");
        OnHealthChanged(currentHealth, stats.maxHealth);
        
    }

    public void TakeDamage(int damageAmount, Element element)
    {
        bool isImmune = false;
        foreach (Element immunity in stats.immunities)
        {
            if (immunity == element)
            {
                isImmune = true;
            }
        }
        if (isImmune)
        {
            currentHealth -= damageAmount;
        }
        else
        {
            currentHealth -= (int)(damageAmount * 1.5f);
        }
        AudioManager.Instance.PlaySound("Hit");
        Debug.Log("Viking took " + damageAmount + " damage and has " + currentHealth + " health left");
        OnHealthChanged(currentHealth, stats.maxHealth);
        
    }

    public void SetStats(VikingStats newStats)
    {
        stats = newStats;
    }

    private void GameWin()
    {
        this.enabled = false;
    }

}
