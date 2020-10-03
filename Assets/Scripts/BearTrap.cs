using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{

    [SerializeField]
    private int damageAmount = 15;

    private BoxCollider2D coll;
    private Animator animator;

    private bool hasEntered;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !hasEntered)
        {
            hasEntered = true;
            animator.SetTrigger("Snap");
            AudioManager.Instance.PlaySound("BearTrap");
            collision.GetComponent<Viking>().TakeDamage(damageAmount);
        }
    }

}
