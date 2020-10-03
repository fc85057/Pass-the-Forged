using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WinTrigger : MonoBehaviour
{

    public static event Action OnWinTriggerEntered;

    //public bool triggerEnabled;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Win trigger entered");
            OnWinTriggerEntered();
        }
    }

}
