using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int damage = 15;

    private Transform target;

    private void Start()
    {
        target = GameManager.Instance.CurrentViking.transform;
    }

    private void FixedUpdate()
    {
        // Vector2 targetVector = new Vector2(target.position.x, transform.position.y);
        Vector2 targetVector = new Vector2(transform.position.x - 1, transform.position.y);
        transform.position = Vector2.MoveTowards(transform.position, targetVector, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Viking>().TakeDamage(damage);
            Destroy(gameObject, .1f);
        }
    }

}
