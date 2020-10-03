using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{

    [SerializeField]
    private int healthAmount = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Player detected");
            collision.GetComponent<Viking>().Heal(healthAmount);
            Destroy(gameObject, 0.2f);
        }
    }

}
