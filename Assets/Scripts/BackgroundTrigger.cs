using System;
using UnityEngine;

public class BackgroundTrigger : MonoBehaviour
{

    public static event Action OnPlayerEntered;
    public static event Action OnVikingTrigger;

    private BoxCollider2D coll;
    private bool playerEntered;
    public bool vikingTriggerEnabled;

    private void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !playerEntered)
        {
            playerEntered = true;
            if (vikingTriggerEnabled)
            {
                OnVikingTrigger();
                vikingTriggerEnabled = false;
            }
            OnPlayerEntered();
        }

    }

}
