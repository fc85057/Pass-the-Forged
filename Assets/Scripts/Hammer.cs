using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    public static event Action<Enemy> OnEnemyHit;

    private Collider2D coll;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    public void SetCollider(bool colliderOn)
    {
        coll.enabled = colliderOn;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy hitEnemy = collision.GetComponent<Enemy>();
            if (hitEnemy != null)
            {
                OnEnemyHit(hitEnemy);
            }
                
        }
    }

}
